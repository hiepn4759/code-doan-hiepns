using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebNhaHangOnline.Models;

namespace BusinessLayer.Business.SanPham
{
    public class SanPhamModel
    {
        WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();
        public IQueryable<WebNhaHangOnline.Models.SanPham> SearchByName(string term)
        {
            IQueryable<WebNhaHangOnline.Models.SanPham> lst;
            lst = db.SanPhams.Where(u => u.TenSP.Contains(term));
            return lst ;
        }

        public IQueryable<WebNhaHangOnline.Models.SanPham> AdvancedSearch(string term, string loai, string hangsx, int? minprice, int? maxprice)
        {
            IQueryable<WebNhaHangOnline.Models.SanPham> lst = db.SanPhams;
            if(!string.IsNullOrEmpty(term))
                lst = SearchByName(term);
            if(!string.IsNullOrEmpty(loai))
                lst = from p in lst where p.LoaiSP.Equals(loai) select p;
            if (!string.IsNullOrEmpty(hangsx))
                lst = from p in lst where p.HangSX.Equals(hangsx) select p;
            if (minprice != null)
                lst = from p in lst where p.GiaTien >= minprice select p;
            if (maxprice != null)
                lst = from p in lst where p.GiaTien <= maxprice select p;
            return lst;
        }
        public IQueryable<WebNhaHangOnline.Models.SanPham> SearchByType(string term)
        {
            var splist = (from p in db.SanPhams where p.LoaiSP.Equals(term) select p);
            return splist;
        }



        public IQueryable<WebNhaHangOnline.Models.SanPham> SPMoiNhap()
        {
            var splist = db.SanPhams.Where(s => s.isnew == true);
            return splist;
        }

        public IQueryable<WebNhaHangOnline.Models.SanPham> SPKhuyenMai()
        {
            var splist = from p in db.SanPhamKhuyenMais
                         orderby p.GiamGia descending
                         where DateTime.Today >= p.KhuyenMai.NgayBatDau && DateTime.Today <= p.KhuyenMai.NgayKetThuc
                         select p.SanPham;
            return splist;
        }

        public IQueryable<WebNhaHangOnline.Models.SanPham> SPBanChay(int takenum)
        {
            var s = from p in db.ChiTietDonHangs
                    where p.DonHangKH.TinhTrangDH == 3
                    group p by p.MaSP into gro
                    select new {MaSP = gro.Key,sl = gro.Sum(r => r.SoLuong)};
            var splist = from p in db.SanPhams join ca in s on p.MaSP equals ca.MaSP orderby ca.sl descending select p;
            return splist.Take(takenum);
        }

        public IQueryable<WebNhaHangOnline.Models.SanPham> GetAll()
        {
            return db.SanPhams;
        }

        public WebNhaHangOnline.Models.SanPham FindById(string id)
        {
            return db.SanPhams.Find(id);
        }

        public IQueryable<WebNhaHangOnline.Models.HangSanXuat> GetAllHangSX()
        {
            return db.HangSanXuats;
        }

        public IQueryable<WebNhaHangOnline.Models.SanPham> GetAllSP()
        {
            return db.SanPhams;
        }

        public IQueryable<LoaiSP> GetAllLoaiSP()
        {
            return db.LoaiSPs;
        }

        public IQueryable<NhaCungCap> GetAllNhaCC()
        {
            return db.NhaCungCaps.Where(d => d.Net_user != null);
        }

        public void EditSP(WebNhaHangOnline.Models.SanPham sanpham)
        {
            //MaSP,TenSP,LoaiSP,HangSX,XuatXu,GiaTien,MoTa,SoLuong,isnew,ishot
            WebNhaHangOnline.Models.SanPham sp = db.SanPhams.Find(sanpham.MaSP);
            sp.TenSP = sanpham.TenSP;
            sp.LoaiSP = sanpham.LoaiSP;
            sp.HangSX = sanpham.HangSX;
            sp.XuatXu = sanpham.XuatXu;
            sp.GiaGoc = sanpham.GiaGoc;
            sp.GiaTien = tinhgiatien(sp.MaSP, sp.GiaGoc);
            sp.MoTa = sanpham.MoTa;
            sp.SoLuong = sanpham.SoLuong;
            sp.isnew = sanpham.isnew;
            sp.ishot = sanpham.ishot;
            db.Entry(sp).State = EntityState.Modified;
            db.SaveChanges();
        }

        private decimal? tinhgiatien(string masp,decimal? giagoc)
        {
            IQueryable<SanPhamKhuyenMai> s = db.SanPhamKhuyenMais.Where(m => m.MaSP.Equals(masp)).OrderByDescending(m => m.GiamGia);
            if (s.Any())
            {
                return (giagoc * (100-s.First().GiamGia) / 100);
            }
            return giagoc;
        }

        public void DeleteSP(string id)
        {
            WebNhaHangOnline.Models.SanPham sanpham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
        }

        public string ThemSP(WebNhaHangOnline.Models.SanPham sanpham)
        {
            sanpham.MaSP = TaoMa();
            sanpham.GiaTien = sanpham.GiaGoc;
            sanpham.AnhDaiDien = sanpham.MaSP + "1.jpg";
            sanpham.AnhNen = sanpham.MaSP + "2.jpg";
            sanpham.AnhKhac = sanpham.MaSP + "3.jpg";

            // xu ly  n  - n
            var newSPCM =  new Sanphamcanmua();
            newSPCM.MaSP = sanpham.MaSP;
            newSPCM.Soluong = sanpham.SoLuong;
            newSPCM.Ngayketthuc = DateTime.Today.AddYears(1);
            newSPCM.Ngaydang = DateTime.Today;
            newSPCM.Mota = sanpham.MoTa;
            newSPCM.SanPham_Id = sanpham.MaSP;
            newSPCM.NhaCungCap_Id = sanpham.MaNCC;
            db.Sanphamcanmuas.Add(newSPCM);
      
            db.SanPhams.Add(sanpham);
            db.SaveChanges();
            return sanpham.MaSP;
        }

        

        private string TaoMa()
        {
            string maID;
            Random rand = new Random();
            do
            {
                maID = "";
                for (int i = 0; i < 5; i++)
                {
                    maID += rand.Next(9);
                }
            }
            while (!KiemtraID(maID));
            return maID;
        }

        private bool KiemtraID(string maID)
        {
            using (WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
            {
                var temp = db.SanPhams.Find(maID);
                if (temp == null)
                    return true;
                return false;
            }
        }
        public WebNhaHangOnline.Models.SanPham getSanPham(string id)
        {
            var sp = (from p in db.SanPhams where (p.MaSP == id) select p).FirstOrDefault();
            return sp;
        }

        public void ThemTSKT(ThongSoKyThuat item)
        {
            db.ThongSoKyThuats.Add(item);
            db.SaveChanges();
        }

        public IQueryable<ThongSoKyThuat> GetTSKT(string masp)
        {
            return db.ThongSoKyThuats.Where(m => m.MaSP == masp);
        }

        public void DelAllTSKT(string p)
        {
            db.ThongSoKyThuats.RemoveRange(db.ThongSoKyThuats.Where(m => m.MaSP == p));
            db.SaveChanges();
        }

        public IQueryable<WebNhaHangOnline.Models.SanPham> SPHot()
        {
            return db.SanPhams.Where(s => s.ishot == true);
        }

        public void UpdateGiaBan(string p)
        {
            var s = db.SanPhams.Find(p);
            s.GiaTien = tinhgiatien(p, s.GiaGoc);
            db.Entry(s).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateGiaBans(List<WebNhaHangOnline.Models.SanPham> lst)
        {
            using (var db = new WebGiayHangHieuEntities())
            {
                lst.ForEach(m => m.GiaTien = tinhgiatien(m.MaSP, m.GiaGoc));
                db.SaveChanges();
            }
        }

        public void UpdateSL(string masp,int? sl,bool? loaihd)
        {
            if (sl != null)
            {
                var s = db.SanPhams.Find(masp);
                if (loaihd == true)
                    s.SoLuong += sl;
                else if (loaihd == false)
                    s.SoLuong -= sl;                
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void UpdateSLKho(string masp, int? sl, bool? loaihd)
        {
            if (sl != null)
            {
                var s = db.TongSPKhoes.Where(d => d.IDSP == masp).FirstOrDefault();
                if (loaihd == true)
                    s.SL += sl;
                else if (loaihd == false)
                    s.SL -= sl;
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


    }
}