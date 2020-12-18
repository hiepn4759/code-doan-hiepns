//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace WebNhaHangOnline.Models
//{
//    public class Ban_LoaiBanModel
//    {
//        private Entities db;
//        public Ban_LoaiBanModel()
//        {
//            db = new Entities();
//        }
//        public IQueryable<Ban> GetCategory()
//        {
//            IQueryable<Ban> lst = db.Bans;
//            return lst;
//        }

//        internal IEnumerable<LoaiBan> GetAllLoaiSP()
//        {
//            return db.LoaiBans.ToList();
//        }
//        internal Ban FindById(string id)
//        {
//            return db.Bans.Find(id);
//        }

//        internal void EditLoaiBan(Ban loai)
//        {
//            Ban lsp = db.Bans.Find(loai.MaBan);
//            lsp.MaBan = loai.MaBan;
//            lsp.TenPhong = loai.TenPhong;
//            lsp.MaLoai = loai.MaLoai;
//            lsp.DienTich = loai.DienTich;
//            lsp.GiaThue = loai.GiaThue;
//            lsp.MieuTa = loai.MieuTa;
//            db.Entry(lsp).State = EntityState.Modified;
//            db.SaveChanges();
//        }

//        internal void DeleteLoaiBan(string id)
//        {
//            Ban loai = db.Bans.Find(id);
//            db.Bans.Remove(loai);
//            db.SaveChanges();
//        }

//        internal string ThemLoaiBan(Ban loai)
//        {
//            loai.MaBan = TaoMa();
//            db.Bans.Add(loai);
//            db.SaveChanges();
//            return loai.MaBan;
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
//            var temp = db.Bans.Find(maID);
//            if (temp == null)
//                return true;
//            return false;
//        }

//        internal IQueryable<Ban> SearchByName(string key)
//        {
//            if (string.IsNullOrEmpty(key))
//                return db.Bans;
//            return db.Bans.Where(u => u.TenPhong.Contains(key));
//        }


//        internal bool KiemTraTen(string p)
//        {
//            var temp = db.Bans.Where(m => m.TenPhong.Equals(p)).ToList();
//            if (temp.Count == 0)
//                return true;
//            return false;
//        }
//    }
//}