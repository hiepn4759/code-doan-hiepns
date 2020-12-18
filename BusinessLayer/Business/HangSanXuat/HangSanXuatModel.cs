using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebNhaHangOnline.Models;

namespace BusinessLayer.Business.HangSanXuat
{
    public class HangSanXuatModel
    {
        private WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();
        public IQueryable<WebNhaHangOnline.Models.HangSanXuat> GetHangSX()
        {
            IQueryable<WebNhaHangOnline.Models.HangSanXuat> lst = db.HangSanXuats;
            return lst;
        }

        public WebNhaHangOnline.Models.HangSanXuat FindById(string id)
        {
            return db.HangSanXuats.Find(id);
        }

        public void EditHangSX(WebNhaHangOnline.Models.HangSanXuat loai)
        {
            WebNhaHangOnline.Models.HangSanXuat lsp = db.HangSanXuats.Find(loai.HangSX);
            lsp.TenHang = loai.TenHang;
            lsp.TruSoChinh = loai.TruSoChinh;
            lsp.QuocGia = loai.QuocGia;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteHangSX(string id)
        {
            WebNhaHangOnline.Models.HangSanXuat loai = db.HangSanXuats.Find(id);
            db.HangSanXuats.Remove(loai);
            db.SaveChanges();
        }

        public bool KiemTraTen(string p)
        {
            var temp = db.HangSanXuats.Where(m => m.TenHang.Equals(p)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }

        public string ThemHangSX(WebNhaHangOnline.Models.HangSanXuat loai)
        {
            loai.HangSX = TaoMa();
            db.HangSanXuats.Add(loai);
            db.SaveChanges();
            return loai.HangSX;
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
            var temp = db.HangSanXuats.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        public IQueryable<WebNhaHangOnline.Models.HangSanXuat> SearchByName(string key)
        {
            if (string.IsNullOrEmpty(key))
                return db.HangSanXuats;
            return db.HangSanXuats.Where(u => u.TenHang.Contains(key));
        }
    }
}