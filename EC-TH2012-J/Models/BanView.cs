using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNhaHangOnline.Models
{
    public class BanView
    {
        public string MaBan { get; set; }
        public string TenPhong { get; set; }
        public string MaLoai { get; set; }
        public int? DienTich { get; set; }
        public int? GiaThue { get; set; }
        public string TenLoai { get; set; }
        public string DuongDanAnh { get; set; }
    }
}