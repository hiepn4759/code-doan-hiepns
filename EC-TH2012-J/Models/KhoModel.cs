//using WebNhaHangOnline.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace WebNhaHangOnline.Models
//{
//    public class KhoModel
//    {
//        private WebGiayHangHieuEntities db;
//        public KhoModel()
//        {
//            db = new WebGiayHangHieuEntities();
//        }
//        public IQueryable<Kho> GetCategory()
//        {
//            IQueryable<Kho> lst = db.Khoes;
//            return lst;
//        }

//        internal Kho FindById(string id)
//        {
//            return db.Khoes.Find(id);
//        }

//        internal void EditLoaiBan(Kho loai)
//        {
//            Kho lsp = db.Khoes.Find(loai.KhoId);
//            lsp.Giatri = loai.Giatri;
//            lsp.SoLuong = loai.SoLuong;
//            lsp.ThuocTInh = loai.ThuocTInh;
//            db.Entry(lsp).State = EntityState.Modified;
//            db.SaveChanges();
//        }

//        internal void DeleteLoaiBan(string id)
//        {
//            Kho loai = db.Khoes.Find(id);
//            db.Khoes.Remove(loai);
//            db.SaveChanges();
//        }

//        internal string ThemLoaiBan(Kho loai)
//        {
//            loai.KhoId = TaoMa();
//            db.Khoes.Add(loai);
//            db.SaveChanges();
//            return loai.KhoId;
//        }

//        private string TaoMa()
//        {
//            string maID;
//            Random rand = new Random();
//            do
//            {
//                maID = "";
//                for (int i = 0; i < 5; i++)
//                {
//                    maID += rand.Next(9);
//                }
//            }
//            while (!KiemtraID(maID));
//            return maID;
//        }

//        private bool KiemtraID(string maID)
//        {
//            var temp = db.Khoes.Find(maID);
//            if (temp == null)
//                return true;
//            return false;
//        }

//        internal IQueryable<Kho> SearchByName(string key)
//        {
//            if (string.IsNullOrEmpty(key))
//                return db.Khoes;
//            return db.Khoes.Where(u => u.Giatri.Contains(key));
//        }


//        internal bool KiemTraTen(string p)
//        {
//            var temp = db.Khoes.Where(m => m.Giatri.Equals(p)).ToList();
//            if (temp.Count == 0)
//                return true;
//            return false;
//        }
//    }
//}