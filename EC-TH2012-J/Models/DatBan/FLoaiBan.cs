//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace WebNhaHangOnline.Models
//{
//    public class HamLoaiPhong
//    {
//        private WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();

//        public HamLoaiPhong()
//        {
//            db = new Entities();
//        }

//        public IQueryable<LoaiBan> LoaiPhongs
//        {
//            get { return db.LoaiBans; }
//        }

//        public string Insert(LoaiBan model)
//        {
//            db.LoaiBans.Add(model);
//            db.SaveChanges();
//            return model.MaLoai;
//        }

//        public string Update(LoaiBan model)
//        {
//            LoaiBan dbEntry = db.LoaiBans.Find(model.MaLoai);
//            if (dbEntry == null)
//            {
//                return null;
//            }
//            dbEntry.MaLoai = model.MaLoai;
//            dbEntry.TenLoai = model.TenLoai;
//            dbEntry.GhiChu = model.GhiChu;
//            dbEntry.DuongDanAnh = model.DuongDanAnh;
//            db.SaveChanges();
//            return model.MaLoai;
//        }

//        public string Delete(string MaLoai)
//        {
//            LoaiBan dbEntry = db.LoaiBans.Find(MaLoai);
//            if (dbEntry == null)
//            {
//                return null;
//            }
//            db.LoaiBans.Remove(dbEntry);
//            db.SaveChanges();
//            return MaLoai;
//        }
//    }
//}