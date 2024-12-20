using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLHS.models;

public partial class QlhocSinhContext : DbContext
{
    public QlhocSinhContext()
    {
    }

    public QlhocSinhContext(DbContextOptions<QlhocSinhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HocSinh> HocSinhs { get; set; }

    public virtual DbSet<Lop> Lops { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NGUYEN-DUC-TOAN;Initial Catalog=QLHocSinh;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HocSinh>(entity =>
        {
            entity.HasKey(e => e.MaHs).HasName("PK__HocSinh__2725A6EF42C1BE9A");

            entity.ToTable("HocSinh");

            entity.Property(e => e.MaHs)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaHS");
            entity.Property(e => e.ConTbls)
                .HasMaxLength(10)
                .HasColumnName("ConTBLS");
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaLop).HasMaxLength(10);

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.HocSinhs)
                .HasForeignKey(d => d.MaLop)
                .HasConstraintName("FK__HocSinh__MaLop__6477ECF3");
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.MaLop).HasName("PK__Lop__3B98D27369008556");

            entity.ToTable("Lop");

            entity.Property(e => e.MaLop).HasMaxLength(10);
            entity.Property(e => e.TenLop).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
