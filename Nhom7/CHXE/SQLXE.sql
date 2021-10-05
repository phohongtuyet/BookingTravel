USE CHXE;
GO

CREATE TABLE NhanVien
(
	ID INT NOT NULL IDENTITY(1,1),
	HovaTen NVARCHAR(255) NULL,
	SDT NVARCHAR(10) NULL,
	DiaChi NVARCHAR(255) NULL,
	TenDangNhap nvarchar(50) NULL,
	MatKhau nvarchar(225) NULL,
	Quyen bit NULL,
	PRIMARY KEY (ID)
);
CREATE TABLE KhachHang
(
	ID INT NOT NULL IDENTITY(1,1),
	HoVaTen NVARCHAR(255) NULL,
	SDT NVARCHAR(10) NULL,
	DiaChi NVARCHAR(255) NULL,
	TenDangNhap nvarchar(50) NULL,
	MatKhau nvarchar(225) NULL,
	PRIMARY KEY (ID)
);
CREATE TABLE ThuongHieu
(
	ID INT NOT NULL IDENTITY(1,1),
	TenThuongHieu NVARCHAR(255) NULL,
	PRIMARY KEY (ID)
);
CREATE TABLE NoiSanXuat
(
	ID INT NOT NULL IDENTITY(1,1),
	TenNoiSX NVARCHAR(255) NULL,
	PRIMARY KEY (ID)
);

CREATE TABLE LoaiXe
(
	ID INT NOT NULL IDENTITY(1,1),
	TenLoai NVARCHAR(255) NULL,
	PRIMARY KEY (ID)
);

CREATE TABLE Xe
(
	ID INT NOT NULL IDENTITY(1,1),
	ThuongHieu_ID INT NULL,
	NoiSanXuat_ID INT NULL,
	LoaiXe_ID INT NULL,
	TenXe NVARCHAR(255) NULL,
	MauXe NVARCHAR(255) NULL,
	SoLuong INT NULL,
	DonGia bigint NULL,
	ThoiHanBaoHanh INT NULL,
	HinhAnhBia nvarchar(255) NULL,
	MoTa ntext null,
	PRIMARY KEY (ID),
    FOREIGN KEY (ThuongHieu_ID) REFERENCES ThuongHieu(ID),
	FOREIGN KEY (NoiSanXuat_ID) REFERENCES NoiSanXuat(ID),
    FOREIGN KEY (LoaiXe_ID) REFERENCES LoaiXe(ID)
);

CREATE TABLE DonHang
(
	ID INT NOT NULL IDENTITY(1,1),
	NhanVien_ID INT NULL,
	KhachHang_ID INT NULL,
	SDTGiaoHang nvarchar(10) NULL,
	NgayDatHang datetime NULL,
	DiaChiGiaoHang NVARCHAR(255) NULL,
	TinhTrang smallint null,
	PRIMARY KEY (ID),
    FOREIGN KEY (NhanVien_ID) REFERENCES NhanVien(ID),
    FOREIGN KEY (KhachHang_ID) REFERENCES KhachHang(ID) ON DELETE CASCADE,
);
CREATE TABLE ChiTietDonHang
(
	ID INT NOT NULL IDENTITY(1,1),
	DonHang_ID INT NULL,
	Xe_ID INT NULL,
	SoLuong smallint  NULL,
	DonGia bigint null
	PRIMARY KEY (ID),
    FOREIGN KEY (DonHang_ID) REFERENCES DonHang(ID) ON DELETE CASCADE,
	FOREIGN KEY (Xe_ID) REFERENCES Xe(ID)
);
SET IDENTITY_INSERT ThuongHieu ON 
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (1,N'BMW');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (2,N'Mazda');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (3,N'Lexus');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (4,N'Mercedes-Benz');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (5,N'Audi');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (6,N'Vinfast');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (7,N'Toyota');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (8,N'Volkswagen');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (9,N'Hyundai');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (10,N'Ford');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (11,N'Honda');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (12,N'Peugeot');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (13,N'Ferrari');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (14,N'Lamborghini');
INSERT ThuongHieu (ID,TenThuongHieu) VALUES (15,N'Volvo');
SET IDENTITY_INSERT ThuongHieu OFF
select * from ThuongHieu;


SET IDENTITY_INSERT LoaiXe ON 
INSERT LoaiXe(ID,TenLoai) VALUES (1,N'Hatchback');
INSERT LoaiXe(ID,TenLoai) VALUES (2,N'Sedan');
INSERT LoaiXe(ID,TenLoai) VALUES (3,N'Sport Car');
INSERT LoaiXe(ID,TenLoai) VALUES (4,N'Crossover');
INSERT LoaiXe(ID,TenLoai) VALUES (5,N'Minivan');
SET IDENTITY_INSERT LoaiXe OFF
select * from LoaiXe;

SET IDENTITY_INSERT NoiSanXuat ON 
INSERT NoiSanXuat(ID,TenNoiSX) VALUES (1,N'Đức');
INSERT NoiSanXuat(ID,TenNoiSX) VALUES (2,N'Nhật Bản');
INSERT NoiSanXuat(ID,TenNoiSX) VALUES (3,N'Pháp');
INSERT NoiSanXuat(ID,TenNoiSX) VALUES (4,N'Hàn Quốc');
INSERT NoiSanXuat(ID,TenNoiSX) VALUES (5,N'Hoa Kỳ');
INSERT NoiSanXuat(ID,TenNoiSX) VALUES (6,N'Ý');
INSERT NoiSanXuat(ID,TenNoiSX) VALUES (7,N'Việt Nam');
SET IDENTITY_INSERT NoiSanXuat OFF

SET IDENTITY_INSERT NhanVien ON 
INSERT NhanVien(ID,HovaTen,SDT,DiaChi,TenDangNhap,MatKhau,Quyen) VALUES (1,N'Dương Văn Khang','0328789376',N'Long Xuyên',N'khang', N'7c4a8d09ca3762af61e59520943dc26494f8941b',0);
INSERT NhanVien(ID,HovaTen,SDT,DiaChi,TenDangNhap,MatKhau,Quyen) VALUES (2,N'Phạm Thanh Trâm','0328789376',N'Long Xuyên',N'tram', N'7c4a8d09ca3762af61e59520943dc26494f8941b',1);
SET IDENTITY_INSERT NhanVien OFF

SET IDENTITY_INSERT KhachHang ON 
INSERT KhachHang(ID,HoVaTen,SDT,DiaChi,TenDangNhap,MatKhau) VALUES (1,N'Phạm Thanh Trang','0123659862',N'Long Xuyên',N'trang', N'7c4a8d09ca3762af61e59520943dc26494f8941b');
SET IDENTITY_INSERT KhachHang OFF
