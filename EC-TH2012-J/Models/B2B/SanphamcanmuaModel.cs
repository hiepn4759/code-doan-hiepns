using WebNhaHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebNhaHangOnline.Models
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
        public SanPham getSP(string maSP)
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
        internal IQueryable<Sanphamcanmua> TimSPCM(string key)
        {
            IQueryable<Sanphamcanmua> lst = db.Sanphamcanmuas;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.SanPham.TenSP.Contains(key));
            return lst;
        }

        internal void DeleteSPCM(int id)
        {
            Sanphamcanmua loai = db.Sanphamcanmuas.Find(id);
            db.Sanphamcanmuas.Remove(loai);
            db.SaveChanges();
        }

        internal string ThemSPCM(Sanphamcanmua loai)
        {
            loai.Ngaydang = DateTime.Today;
            db.Sanphamcanmuas.Add(loai);
            db.SaveChanges();
            return null;
        }

        internal void EditSPCM(SanPhamCanMuaEdit loai)
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