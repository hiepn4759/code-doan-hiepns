using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHangOnline.Models;
using PagedList;
using System.Net;
using WebNhaHangOnline.Models.B2B;
using System.Data.Entity;

namespace WebNhaHangOnline.Models.B2B
{
    public class TongSPKHoModel
    {
        protected WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();

        internal TongSPKho FindById(string id)
        {
            return db.TongSPKhoes.Where(d => d.IDKho == id).FirstOrDefault();
        }

        internal void DeleteLoaiSP(string id)
        {
            TongSPKho loai = db.TongSPKhoes.Where(d => d.IDKho == id).FirstOrDefault();
            db.TongSPKhoes.Remove(loai);
            db.SaveChanges();
        }

        internal void EditLoaiSP(TongSPKho loai)
        {
            TongSPKho lsp = db.TongSPKhoes.Where(d => d.IDKho == loai.IDKho).FirstOrDefault();
            lsp.SL = loai.SL;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal bool KiemTraTen(string p)
        {
            var temp = db.TongSPKhoes.Where(m => m.IDKho.Equals(p)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }

        internal IQueryable<TongSPKho> SearchByName(string key)
        {
            if (string.IsNullOrEmpty(key))
                return db.TongSPKhoes;
            return db.TongSPKhoes.Where(u => u.IDSP.Contains(key));
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
            var temp = db.TongSPKhoes.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        internal IEnumerable<TongSPKho> FindByIdNCC(string id)
        {
            return db.TongSPKhoes.Where(d => d.IDSP == id);
        }
        internal TongSPKho FindByIdNCCOne(string id)
        {
            return db.TongSPKhoes.Where(d => d.IDKho == id).FirstOrDefault();
        }
        internal TongSPKho FindByIdMaSP(string id)
        {
            return db.TongSPKhoes.Where(d => d.IDSP == id).FirstOrDefault();
        }

        internal IEnumerable<TongSPKho> Gets()
        {
            return db.TongSPKhoes;
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
            var temp = db.TongSPKhoes.Find(maID);
            if (temp == null)
                return true;
            return false;
        }
    }
}