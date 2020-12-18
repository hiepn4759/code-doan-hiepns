using BusinessLayer.Business.B2B;
using BusinessLayer.Business.GioHang;
using BusinessLayer.Business.SanPham;
using BusinessLayer.Models.DonHang;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using WebNhaHangOnline.Models;

namespace BusinessLayer.Business.DonHang
{
    public class DonhangKHModel
    {
        public DonHangKH donHang;
        public AspNetUser nguoiMua;
        public String nguoiNhan;
        public String tinhTrangDH;
        public List<DonhangKHModel> Xemdonhang(string makh)
        {
            using (WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
            {
                List<DonhangKHModel> listDh = new List<DonhangKHModel>();
                db.DonHangKHs.AsNoTracking();
                var danhsach = from p in db.DonHangKHs where p.MaKH == makh select p;
                foreach (var temp in danhsach.ToList())
                {
                    AspNetUser users = (from p in db.AspNetUsers where p.Id == makh select p).FirstOrDefault();

                    listDh.Add(new DonhangKHModel()
                    {
                        donHang = temp,
                        nguoiMua = users,
                        tinhTrangDH = gettinhTrangDH(temp.TinhTrangDH)
                    });
                }
                return listDh;
            }
        }

        private string gettinhTrangDH(int? nullable)
        {
            switch (nullable)
            {
                case 0:
                    {
                        return "Chưa giao";
                    }
                case 1:
                    {
                        return "Đang duyệt";
                    }
                case 2:
                    {
                        return "Đang giao hàng";
                    }
                case 3:
                    {
                        return "Đã giao";
                    }
                case 4:
                    {
                        return "Đã hủy";
                    }
            }
            return "Đang duyệt";
        }
        public bool HuyDH(string maDH)
        {
            try
            {
                using (WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
                {
                    string query = "update DonHangKH set TinhTrangDH = '4' where MaDH ='" + maDH + "'";
                    db.Database.ExecuteSqlCommand(query);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public AspNetUser Xemttnguoidung(string id)
        {
            using (WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
            {
                AspNetUser users = (from p in db.AspNetUsers where p.Id == id select p).FirstOrDefault();
                return users;
            }
        }
        public void Luudonhang_(WebNhaHangOnline.Models.Donhangtongquan a, Models.GioHang.Giohang giohang)
        {
            try
            {
                using (WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
                {
                    WebNhaHangOnline.Models.DonHangKH dhkh = new WebNhaHangOnline.Models.DonHangKH();
                    dhkh.MaDH = RandomMa();
                    //dhkh.MaKH = maKH;

                    dhkh.Diachi = a.address;
                    dhkh.Dienthoai = a.phoneNumber;
                    dhkh.Ghichu = a.Note;
                    dhkh.NgayDatMua = DateTime.Now;
                    dhkh.TinhTrangDH = 1;
                    dhkh.Tongtien = giohang.TinhtongtienCart();
                    dhkh.PhiVanChuyen = 0;
                    dhkh = db.DonHangKHs.Add(dhkh);


                    Luuchitietdonhang(giohang, db, dhkh.MaDH);
                    
                    db.SaveChanges();
                }
            }
            catch (Exception e) { }
        }
        public void Luudonhang(WebNhaHangOnline.Models.Donhangtongquan a, string maKH, Models.GioHang.Giohang giohang)
        {           
            try
            {
                using (WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
                {
                    DonHangKH dhkh = new DonHangKH();
                    dhkh.MaDH = RandomMa();
                    dhkh.MaKH = maKH;

                    
                    dhkh.Diachi = a.address;
                    dhkh.Dienthoai = a.phoneNumber;
                    dhkh.Ghichu = a.Note;
                    dhkh.NgayDatMua = DateTime.Now;
                    dhkh.TinhTrangDH = 1;
                    dhkh.Tongtien = giohang.TinhtongtienCart();
                    dhkh.PhiVanChuyen = 0;                    
                    dhkh = db.DonHangKHs.Add(dhkh);
                    

                    Luuchitietdonhang(giohang, db, dhkh.MaDH);
                    if (dhkh.Tongtien >= 500000)
                    {
                        AspNetUser listUserNet = db.AspNetUsers.Find(maKH);
                        if(listUserNet.AccessFailedCount >= 500)
                        {
                            dhkh.Tongtien = dhkh.Tongtien * 0.8;
                            listUserNet.AccessFailedCount += 100;
                            db.Entry(listUserNet).State = EntityState.Modified;
                            db.SaveChanges();
                        }else
                        {
                            listUserNet.AccessFailedCount += 100;
                            db.Entry(listUserNet).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();                    
                }
            }
            catch (Exception e) { }
        }

        private void Luuchitietdonhang(Models.GioHang.Giohang giohang, WebGiayHangHieuEntities db, string maDH)
        {
            foreach (var temp in giohang.getGiohang())
            {               
                ChiTietDonHang chiTiet = new ChiTietDonHang()
                {
                    MaDH = maDH,
                    MaSP = temp.sanPham.MaSP,
                    SoLuong = temp.Soluong,
                    ThanhTien = (decimal)temp.Thanhtien
                };
                db.ChiTietDonHangs.Add(chiTiet);
        }
            db.SaveChanges();
        }
        public string RandomMa()
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
                var temp = db.DonHangKHs.Find(maID);
                if (temp == null)
                    return true;
                return false;
            }
        }

        public IQueryable<DonHangKH> TimDonHang(string key, string mobile, DateTime? date, int? status)
        {
            WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();

            IQueryable<DonHangKH> lst = db.DonHangKHs;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.MaDH.Contains(key));
            if (!string.IsNullOrEmpty(mobile))
                lst = lst.Where(m => m.Dienthoai.Contains(mobile));
            if (status != null)
                lst = lst.Where(m => m.TinhTrangDH == status);
            if (date != null)
                lst = lst.Where(m => m.NgayDatMua.Value.Year == date.Value.Year && m.NgayDatMua.Value.Month == date.Value.Month && m.NgayDatMua.Value.Day == date.Value.Day);
            return lst;

        }

        public bool UpdateTinhTrang(string madh, int? tt)
        {
            if (tt == null) return false;
            try
            {
                WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();
                DonHangKH dh = db.DonHangKHs.Find(madh);
                if (dh.TinhTrangDH == 4 || dh.TinhTrangDH == 3)
                    return false;
                if (dh.TinhTrangDH == 1)
                    if (tt == 2 || tt == 3)
                    {
                        foreach (var item in dh.ChiTietDonHangs)
                        {
                            SanPham.SanPhamModel spm = new SanPham.SanPhamModel();
                            TongSPKHoModel tongKho = new TongSPKHoModel();
                            var countSL = tongKho.FindByIdMaSP(item.MaSP).SL;
                            if(countSL < item.SoLuong)
                            {
                                return false;
                            }else
                            {
                                spm.UpdateSLKho(item.MaSP, item.SoLuong, false);
                            }
                            
                        }
                    }
                if (dh.TinhTrangDH == 2)
                {
                    if (tt == 4)
                    {
                        foreach (var item in dh.ChiTietDonHangs)
                        {
                            SanPham.SanPhamModel spm = new SanPham.SanPhamModel();
                            TongSPKHoModel tongKho = new TongSPKHoModel();
                            var countSL = tongKho.FindByIdMaSP(item.MaSP).SL;
                            if (countSL < item.SoLuong)
                            {
                                return false;
                            }
                            else
                            {
                                spm.UpdateSLKho(item.MaSP, item.SoLuong, true);
                            }
                            
                        }
                    }
                    if (tt == 1) return false;
                }
                string query = "update DonHangKH set TinhTrangDH = " + tt + " where MaDH ='" + madh + "'";
                db.Database.ExecuteSqlCommand(query);
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IQueryable<ChiTietDonHang> ChiTietDonHang(string maDH)
        {
            WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();
            return db.ChiTietDonHangs.Where(m => m.MaDH.Contains(maDH));
        }

        public IQueryable<object> ThongKeDoanhThu(DateTime? froms, DateTime? tos)
        {
            WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();
            var s = from p in db.DonHangKHs
                    where p.TinhTrangDH == 3 && p.NgayDatMua >= froms && p.NgayDatMua <= tos
                    group p by EntityFunctions.TruncateTime(p.NgayDatMua) into gro
                    select new { ngaymua = gro.Key.ToString(), tongtien = gro.Sum(r => r.Tongtien) };
            return s;
        }


        public IQueryable<object> ThongKeTiTrong(DateTime? froms, DateTime? tos)
        {
            WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();
            var s = from p in db.ChiTietDonHangs
                    where p.DonHangKH.TinhTrangDH == 3 && p.DonHangKH.NgayDatMua >= froms && p.DonHangKH.NgayDatMua <= tos
                    group p by p.SanPham.TenSP into gro
                    select new { TenSP = gro.Key, SL = gro.Sum(r => r.SoLuong) };
            return s;
        }
    }
}