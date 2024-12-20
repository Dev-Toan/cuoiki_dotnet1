use master
go
create database QLHocSinh
go
use QLHocSinh

create table HocSinh(
 MaHS char(4) primary key,
 HoTen nvarchar(50),
 NgaySinh date,
 GioiTinh nvarchar(10),
 ConTBLS nvarchar(10),
 MaLop nvarchar(10)
)

insert into HocSinh
values('HS01','Tran Ngoc Lan','2010-12-30','Nu','Co','C01')
insert into HocSinh
values('HS02','Tran Van Nam','2011-06-19','Nam','Khong','C02')


create table Lop(
 MaLop nvarchar(10),
 TenTop Nvarchar(30)
)

insert into Lop
values('C01','Chuyen Hoa')
insert into Lop
values('C02','Chuyen Toan')
insert into Lop
values('C03','Chuyen Ly')
insert into Lop
values('C04','Chuyen Van')
insert into Lop
values('C05','Chuyen Anh')

select * from HocSinh
select * from Lop
