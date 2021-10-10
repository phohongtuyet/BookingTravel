USE BookingTravel;
go

CREATE TABLE KhachSan
(
	ID 	  		int NOT NULL IDENTITY(1,1),
	TenKhachSan nvarchar(255) null,
	DiaChi 		nvarchar(255) null,
	primary key (ID)
)
GO

CREATE TABLE PhuongTien
(
	ID 	  			int NOT NULL IDENTITY(1,1),
	TenPhuongTien 	nvarchar(255) null,
	LoaiPhuongTien 	smallint null, /*1 may bay 2 tau hoa, 3 tàu thủy 4 xe khách*/
	SoCho			int null,
	primary key (ID)
)
GO

CREATE TABLE KhachHang
(
	ID 			INT IDENTITY(1,1) NOT NULL,
	HoVaten 	nvarchar(100) null,
	DienThoai 	nvarchar(20) null,
	DiaChi 		nvarchar(255) null,
	TenDangNhap nvarchar(50) null,
	MatKhau 	nvarchar(255) null,
	primary key (ID)
)
GO

CREATE TABLE NhanVien
(
	ID 			INT IDENTITY(1,1) NOT NULL,
	HoVaTen 	nvarchar(100) NULL,
	DienThoai 	nvarchar(20) NULL,
	DiaChi 		nvarchar(255) NULL,
	TenDangNhap nvarchar(50) NULL,
	MatKhau 	nvarchar(255) NULL,
	Quyen 		bit NULL,/*0 admin 1 hướng dẫn viên*/
	Khoa		tinyint NULL,/*0 bị khóa, 1 không bị khóa*/
	primary key (ID)
)
GO

CREATE TABLE DiaDiemThamQuan
(
	ID 	  		int NOT NULL IDENTITY(1,1),
	TenDiaDanh 	nvarchar(255) null,
	Tinh 		smallint null,
	KhachSan_ID	int NULL foreign key(KhachSan_ID) references KhachSan(ID) ON UPDATE CASCADE,
	primary key (ID)
)
GO

CREATE TABLE BaiViet
(
	ID 					int IDENTITY(1,1) NOT NULL,
	NhanVien_ID 		int NULL foreign key(NhanVien_ID) references NhanVien(ID) ON UPDATE CASCADE,
	TieuDe 				nvarchar(255)NULL,
	TomTat 				ntext NULL,
	NoiDung 			ntext NULL,
	NgayDang 			datetime NULL,
	LuotXem 			int NULL,
	KiemDuyet 			tinyint NULL, /*0 chưa kiểm duyệt 1 đã kiểm duyệt*/
	TrangThaiBinhLuan 	tinyint NULL,/*0 không cho phép bình luận 1 cho phép bình luận*/
	PRIMARY KEY (ID)
) 
GO

CREATE TABLE BinhLuan
(
	ID 				int IDENTITY(1,1) NOT NULL,
	BaiViet_ID 		int NULL foreign key(BaiViet_ID) references BaiViet(ID),	
	KhachHang_ID 	int NULL foreign key(KhachHang_ID) references KhachHang(ID)ON UPDATE CASCADE,
	NoiDung 		ntext NULL,
	NgayDang 		datetime NULL,
	KiemDuyet 		tinyint NULL,/*0 chưa kiểm duyệt 1 đã kiểm duyệt*/
	PRIMARY KEY (ID)
) 
GO

CREATE TABLE Tour
(
	ID 					int IDENTITY(1,1) NOT NULL,
	TenTour				nvarchar(255) NULL,
	LoaiTour			smallint NULL,
	NoiKhoiHanh			smallint NULL,	
	NgayBD				date null,
	NgayKT				date null,
	DonGia				int NULL,
	SoLuong 			int null,
	TrangThai 			smallint null,
	HinhAnh				nvarchar(255) NULL,
	MoTa				ntext NULL,/*0 chưa hoạt động,1 đã hoạt động*/
	primary key (ID)			
)
GO

CREATE TABLE DatTour
(
	ID 					INT IDENTITY(1,1) NOT NULL,
	NhanVien_ID 		int null foreign key(NhanVien_ID) references NhanVien(ID)ON UPDATE CASCADE,
	KhachHang_ID 		int null foreign key(KhachHang_ID) references KhachHang(ID)ON UPDATE CASCADE,
	DienThoaiDatTour	nvarchar (20) null,
	HoVaTen				nvarchar(255) null,
	NgayDatHang 		datetime null,
	TinhTrang 			smallint null,
	primary key (ID),	
	
)
GO

CREATE TABLE ChiTietDiaDiemThamQuan
(
	ID 					INT IDENTITY(1,1) NOT NULL,
	Tour_ID 			int null foreign key(Tour_ID) references Tour(ID)ON DELETE CASCADE,
	DiaDiemThamQuan_ID	int null foreign key(DiaDiemThamQuan_ID) references DiaDiemThamQuan(ID) ON DELETE CASCADE,
	primary key (ID)
)
GO

CREATE TABLE ChiTietPhuongTien
(
	ID 				INT IDENTITY(1,1) NOT NULL,
	Tour_ID 		int null foreign key(Tour_ID) references Tour(ID)ON DELETE CASCADE,
	PhuongTien_ID	int null foreign key(PhuongTien_ID) references PhuongTien(ID) ON DELETE CASCADE,
	primary key (ID)
)
GO

CREATE TABLE DatTour_ChiTiet
(
	ID 			INT IDENTITY(1,1) NOT NULL,
	DatTour_ID	int null foreign key(DatTour_ID) references DatTour(ID),
	Tour_ID		int null foreign key(Tour_ID)  references  Tour(ID),
	SoLuong		smallint null,
	DonGia		int null,
	primary key (ID)
)
GO