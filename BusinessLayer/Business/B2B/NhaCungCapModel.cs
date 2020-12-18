﻿using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebNhaHangOnline.Models;

namespace BusinessLayer.Business.B2B
{
    public class NhaCungCapModel
    {
        protected WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();


        public IQueryable<NhaCungCap> TimNCC(string key)
        {
            IQueryable<NhaCungCap> lst = db.NhaCungCaps;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.TenNCC.Contains(key));
            return lst;
        }

        public bool KiemTraTen(string p)
        {
            var temp = db.NhaCungCaps.Where(m => m.TenNCC.Equals(p)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }

        public string ThemHangNCC(NhaCungCap loai)
        {
            loai.MaNCC = TaoMa();
            db.NhaCungCaps.Add(loai);
            db.SaveChanges();
            return loai.MaNCC;
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
            var temp = db.NhaCungCaps.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        public NhaCungCap FindByIdNCC(string id)
        {
            return db.NhaCungCaps.Find(id);
        }


        public void EditNCC(NhaCungCap loai)
        {
            try
            {
                NhaCungCap lsp = db.NhaCungCaps.Find(loai.MaNCC);
                lsp.TenNCC = loai.TenNCC;
                lsp.DiaChi = loai.DiaChi;
                lsp.SDT_NCC = loai.SDT_NCC;
                lsp.Email = loai.Email;
                lsp.Website = loai.Website;
                lsp.Net_user = loai.Net_user;
                db.Entry(lsp).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception ex)
            {

            }
            
        }

        public void DeleteNhaCungCap(string id)
        {
            NhaCungCap loai = db.NhaCungCaps.Find(id);
            db.NhaCungCaps.Remove(loai);
            db.SaveChanges();
        }

        //public object FindById(string id)
        //{
        //    return db.HopDongNCCs.Find(id);
        //}

        //public void DeleteHopDong(string id)
        //{
        //    HopDongNCC loai = db.HopDongNCCs.Find(id);
        //    db.HopDongNCCs.Remove(loai);
        //    db.SaveChanges();
        //}

        //public string ThemHopDong(HopDongNCC loai)
        //{
        //    loai.MaHD = TaoMa();
        //    loai.TinhTrang = true;
        //    loai.TTThanhToan = true;
        //    db.HopDongNCCs.Add(loai);
        //    db.SaveChanges();
        //    EmailTool sendmail = new EmailTool();
        //    sendmail.SendMail(GetParent(loai.MaHD));
        //    return loai.MaHD;
        //}

        //private EmailModel GetParent(string mahd)
        //{
        //    HopDongNCC loai = db.HopDongNCCs.Where(m => m.MaHD.Equals(mahd)).FirstOrDefault();
        //    NhaCungCap ncc = db.NhaCungCaps.Where(m => m.MaNCC.Equals(loai.MaNCC)).FirstOrDefault();
        //    SanPham sp = db.SanPhams.Where(m => m.MaSP.Equals(loai.MaSP)).FirstOrDefault();
        //    string mail = ncc.Email;
        //    string sub = "[Thông báo] Đã chấp nhận đăng ký cung cấp sản phẩm";
        //    string bo = "";
        //    bo += "Xin chào " + ncc.TenNCC + ",<br>";
        //    bo += "Chúng tôi rất vinh hạnh được hợp tác với các bạn với sản phẩm: " + sp.TenSP + ". Và sau đây là chi tiết hợp đồng:<br>";
        //    bo += "Mã hợp đồng: <strong>" + loai.MaHD + "</strong><br>";
        //    bo += "Ngày ký hợp đồng: <strong>" + loai.NgayKy + "</strong><br>";
        //    bo += "Thời hạn hợp đồng theo tháng: <strong>" + loai.ThoiHanHD + "</strong><br>";
        //    bo += "Sản phẩm trong hợp đồng: <strong>" + sp.TenSP + "</strong><br>";
        //    bo += "Số lượng tồn kho tối thiểu để cung cấp hàng: <strong>" + loai.SLToiThieu + "</strong><br>";
        //    bo += "Số lượng cần cung cấp: <strong>" + loai.SLCungCap + "</strong><br>";
        //    bo += "Số ngày giao kể từ ngày xác nhận giao hàng: <strong>" + loai.SoNgayGiao + "</strong><br>";
        //    bo += "Xin cảm ơn.";
        //    EmailModel email = new EmailModel(mail, sub, bo);
        //    return email;
        //}

        //private string TaoMa()
        //{
        //    string maID;
        //    Random rand = new Random();
        //    do
        //    {
        //        maID = "";
        //        for (int i = 0; i < 5; i++)
        //        {
        //            maID += rand.Next(9);
        //        }
        //    }
        //    while (!KiemtraID(maID));
        //    return maID;
        //}

        //private bool KiemtraID(string maID)
        //{
        //    var temp = db.HopDongNCCs.Find(maID);
        //    if (temp == null)
        //        return true;
        //    return false;
        //}


        //public IQueryable<HopDongNCC> TimHopDong(string key, string tensp, bool? loaihd)
        //{
        //    IQueryable<HopDongNCC> lst = db.HopDongNCCs;
        //    if (!string.IsNullOrEmpty(key))
        //        lst = lst.Where(m => m.MaHD.Contains(key));
        //    if (!string.IsNullOrEmpty(tensp))
        //        lst = lst.Where(m => m.SanPham.TenSP.Contains(tensp));
        //    if (loaihd != null)
        //        lst = lst.Where(m => m.IsBuy == loaihd);
        //    return lst;

        //}

        public void ThemNCC(Models.Register2B2ViewModel model, string p)
        {
            NhaCungCap ncc = new NhaCungCap();
            ncc.MaNCC = TaoMaNCC();
            ncc.TenNCC = model.TenNCC;
            ncc.Net_user = p;
            ncc.DiaChi = model.DiaChi;
            ncc.SDT_NCC = model.SDT_NCC;
            ncc.Email = model.Email;
            db.NhaCungCaps.Add(ncc);
            db.SaveChanges();
        }

        private string TaoMaNCC()
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
            while (!KiemtraIDNCC(maID));
            return maID;
        }

        private bool KiemtraIDNCC(string maID)
        {
            var temp = db.NhaCungCaps.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        public NhaCungCap FindByNetUser(string p)
        {
            return db.NhaCungCaps.Where(m => m.Net_user.Equals(p)).FirstOrDefault();
        }

        public void UpdateInfo(Models.EditInfo2B2ViewModel info)
        {
            NhaCungCap lsp = db.NhaCungCaps.Find(info.MaNCC);
            lsp.TenNCC = info.TenNCC;
            lsp.DiaChi = info.DiaChi;
            lsp.SDT_NCC = info.SDT_NCC;
            lsp.Email = info.Email;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        //public IQueryable<HopDongNCC> TimHopDong(string key, string tensp, bool? loaihd, bool? tt)
        //{
        //    IQueryable<HopDongNCC> lst = db.HopDongNCCs;
        //    if (!string.IsNullOrEmpty(key))
        //        lst = lst.Where(m => m.MaHD.Contains(key));
        //    if (!string.IsNullOrEmpty(tensp))
        //        lst = lst.Where(m => m.SanPham.TenSP.Contains(tensp));
        //    if (loaihd != null)
        //        lst = lst.Where(m => m.IsBuy == loaihd);
        //    if (tt != null)
        //        lst = lst.Where(m => m.TinhTrang == tt);
        //    return lst;
        //}

        //public void XacNhanDaGiao(string item, bool tt)
        //{
        //    HopDongNCC lsp = db.HopDongNCCs.Find(item);

        //    if (lsp.TinhTrang == false && tt == true)
        //    {
        //        SanPhamModel sp = new SanPhamModel();
        //        sp.UpdateSL(lsp.MaSP, lsp.SLCungCap, lsp.IsBuy);
        //    }

        //    lsp.TinhTrang = tt;
        //    db.Entry(lsp).State = EntityState.Modified;
        //    db.SaveChanges();

        //}

        //public void XacNhanDaTT(string item, bool tt)
        //{
        //    HopDongNCC lsp = db.HopDongNCCs.Find(item);

        //    lsp.TTThanhToan = tt;
        //    db.Entry(lsp).State = EntityState.Modified;
        //    db.SaveChanges();

        //}
        //public bool Checkthanhtoan(string MaHD)
        //{
        //    var check = (from p in db.HopDongNCCs where p.MaHD == MaHD select new { tt = p.TTThanhToan }).FirstOrDefault();
        //    return (bool)check.tt;
        //}

    }
}