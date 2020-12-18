//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace WebNhaHangOnline.Models
//{
//    public class BanModel
//    {
//        private Entities db;
//        public BanModel()
//        {
//            db = new Entities();
//        }
//        public IQueryable<LoaiBan> GetCategory()
//        {
//            IQueryable<LoaiBan> lst = db.LoaiBans;
//            return lst;
//        }

//        internal LoaiBan FindById(string id)
//        {
//            return db.LoaiBans.Find(id);
//        }

//        internal void EditLoaiBan(LoaiBan loai)
//        {
//            LoaiBan lsp = db.LoaiBans.Find(loai.MaLoai);
//            lsp.TenLoai = loai.TenLoai;
//            db.Entry(lsp).State = EntityState.Modified;
//            db.SaveChanges();
//        }

//        internal void DeleteLoaiBan(string id)
//        {
//            LoaiBan loai = db.LoaiBans.Find(id);
//            db.LoaiBans.Remove(loai);
//            db.SaveChanges();
//        }

//        internal string ThemLoaiBan(LoaiBan loai)
//        {
//            loai.MaLoai = TaoMa();
//            db.LoaiBans.Add(loai);
//            db.SaveChanges();
//            return loai.MaLoai;
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
//            var temp = db.LoaiBans.Find(maID);
//            if (temp == null)
//                return true;
//            return false;
//        }

//        internal IQueryable<LoaiBan> SearchByName(string key)
//        {
//            if (string.IsNullOrEmpty(key))
//                return db.LoaiBans;
//            return db.LoaiBans.Where(u => u.TenLoai.Contains(key));
//        }


//        internal bool KiemTraTen(string p)
//        {
//            var temp = db.LoaiBans.Where(m => m.TenLoai.Equals(p)).ToList();
//            if (temp.Count == 0)
//                return true;
//            return false;
//        }
//    }
//}