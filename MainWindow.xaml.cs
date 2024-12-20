using QLHS.models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLHS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private QlhocSinhContext context = new QlhocSinhContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var data = context.HocSinhs.Select(hs => new
            {
                hs.MaHs,
                hs.HoTen,
                hs.NgaySinh,
                hs.GioiTinh,
                hs.ConTbls,
                TenLop = hs.MaLopNavigation.TenLop,

                Tuoi = DateTime.Now.Year - hs.NgaySinh.Value.ToDateTime(TimeOnly.MinValue).Year,
            }).ToList();

            dgQLSH.ItemsSource = data;
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            int tuoi = DateTime.Now.Year - dpNgaySinh.SelectedDate.Value.Year;
            if (tuoi < 10 || tuoi > 15)
            {
                MessageBox.Show("tuoi tu 10 den 15");
                return;
            }

            using (var context = new QlhocSinhContext())
            {
                var hocsinh = new HocSinh
                {
                    MaHs = txtMaHS.Text,
                    HoTen = txtHoTen.Text,
                    NgaySinh = DateOnly.FromDateTime(dpNgaySinh.SelectedDate.Value),
                    GioiTinh = rbNam.IsChecked == true ? "nam" : "nu",
                    ConTbls = cbCon.IsChecked == true ? "co" : "khong",
                    MaLop = (cbLop.SelectedItem as ComboBoxItem)?.Tag.ToString(),
                };

                context.HocSinhs.Add(hocsinh);
                context.SaveChanges();
            }
                LoadData();
        }

        private void btThongKe_Click(object sender, RoutedEventArgs e)
        {
            
            var thongke = context.Lops.Select(tk => new
            {
                tk.MaLop,
                tk.TenLop,
                SoHSNu = tk.HocSinhs.Count(h => h.GioiTinh == "Nu")
            }).ToList();

            Window1 window1 = new Window1(thongke);
            window1.Show();
        }

        private void btSua_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new QlhocSinhContext())
            {
                var hs = context.HocSinhs.Find(txtMaHS.Text);
                if(hs != null)
                {
                    hs.HoTen=txtHoTen.Text;
                    hs.NgaySinh = DateOnly.FromDateTime(dpNgaySinh.SelectedDate.Value);
                    hs.GioiTinh = rbNam.IsChecked == true ? "nam" : "nu";
                    hs.ConTbls = cbCon.IsChecked == true ? "co" : "khong";
                    hs.MaLop =(cbLop.SelectedItem as ComboBoxItem)?.Tag.ToString();

                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("khong tim thay ma sinh vien");
                }
            }
        }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new QlhocSinhContext())
            {
                var hs = context.HocSinhs.Find(txtMaHS.Text);
                if(hs != null)
                {
                    context.HocSinhs.Remove(hs);
                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("khong tim thay hs can xoa");
                }
            }
        }
    }
}