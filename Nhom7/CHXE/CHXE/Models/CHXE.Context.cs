﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CHXE.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CHXEEntities : DbContext
    {
        public CHXEEntities()
            : base("name=CHXEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<LoaiXe> LoaiXe { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<NoiSanXuat> NoiSanXuat { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieu { get; set; }
        public virtual DbSet<Xe> Xe { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
    }
}
