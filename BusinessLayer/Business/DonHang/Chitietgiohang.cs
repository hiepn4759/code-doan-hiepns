using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Business.DonHang
{
    public class Chitietgiohang
    {
        public WebNhaHangOnline.Models.SanPham sanPham { get; set; }
        public int Soluong { get; set; }
        private double thanhtien;

        public double Thanhtien
        {
            get { return (double)sanPham.GiaTien * Soluong; }
            set { thanhtien = value; }
        }
        public WebNhaHangOnline.Models.DonHangKH Donhangkh { get; set; }
       
        public void Tinhtien()
        {
            Thanhtien = (double)sanPham.GiaTien * Soluong;
        }
    }
}