using BusinessLayer.Models.B2B;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebNhaHangOnline.Models;

namespace BusinessLayer.Business.B2B
{
    public class SanphamcanmuaModel
    {
        WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();
        public List<Sanphamcanmua> getDS(int index,int count)
        {
            using(WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
            {
                var ds =  db.Sanphamcanmuas.OrderBy(m=>m.Ngaydang).Skip(index).Take(count).ToList();
                return ds;
            }
        }
        public WebNhaHangOnline.Models.SanPham getSP(string maSP)
        {
            using (WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
            {
                return (from p in db.SanPhams where p.MaSP == maSP select p).FirstOrDefault();
            }
        }
        public Sanphamcanmua getSanphamcanmua(int ID)
        {
            using(WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
            {
                Sanphamcanmua var = (from p in db.Sanphamcanmuas where p.ID == ID select p).FirstOrDefault();
                return var;
            }
        }
        public IQueryable<Sanphamcanmua> TimSPCM(string key)
        {
            IQueryable<Sanphamcanmua> lst = db.Sanphamcanmuas;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.SanPham.TenSP.Contains(key));
            return lst;
        }

        public void DeleteSPCM(int id)
        {
            Sanphamcanmua loai = db.Sanphamcanmuas.Find(id);
            db.Sanphamcanmuas.Remove(loai);
            db.SaveChanges();
        }

        public string ThemSPCM(Sanphamcanmua loai)
        {
            loai.Ngaydang = DateTime.Today;
            db.Sanphamcanmuas.Add(loai);
            db.SaveChanges();
            return null;
        }

        public void EditSPCM(Models.B2B.SanPhamCanMuaEdit loai)
        {
            Sanphamcanmua lsp = db.Sanphamcanmuas.Find(loai.ID);
            lsp.Soluong = loai.Soluong;
            lsp.Ngayketthuc = loai.Ngayketthuc;
            lsp.Mota = loai.Mota;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}