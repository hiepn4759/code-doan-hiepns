//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;


//namespace WebNhaHangOnline.Models
//{
//    public class FBan
//    {
//        private Entities db = new Entities();

//        public FBan()
//        {
//            db = new Entities();
//        }

//        public IQueryable<Ban> Phongs
//        {
//            get { return db.Bans; }
//        }

//        public string Insert(Ban model)
//        {
//            db.Bans.Add(model);
//            db.SaveChanges();
//            return model.MaBan;
//        }

//        public string Update(Ban model)
//        {
//            Ban dbEntry = db.Bans.Find(model.MaBan);
//            if (dbEntry == null)
//            {
//                return null;
//            }
//            dbEntry.MaBan = model.MaBan;
//            dbEntry.TenPhong = model.TenPhong;
//            dbEntry.MaLoai = model.MaLoai;
//            dbEntry.DienTich = model.DienTich;
//            dbEntry.GiaThue = model.GiaThue;
//            db.SaveChanges();
//            return model.MaBan;
//        }

//        public string Delete(string MaPhong)
//        {
//            Ban dbEntry = db.Bans.Find(MaPhong);
//            if (dbEntry == null)
//            {
//                return null;
//            }
//            db.Bans.Remove(dbEntry);
//            db.SaveChanges();
//            return MaPhong;
//        }

//        public List<BanView> toanBoPhong()
//        {
//            List<BanView> listPhongView;
//            var query = from s in db.LoaiBans
//                        join r in db.Bans on s.MaLoai equals r.MaLoai
//                        select new BanView
//                        {
//                            MaBan = r.MaBan,
//                            TenPhong = r.TenPhong,
//                            MaLoai = r.MaLoai,
//                            DienTich = r.DienTich,
//                            GiaThue = r.GiaThue,
//                            TenLoai = s.TenLoai,
//                            DuongDanAnh = s.DuongDanAnh
//                        };
//            listPhongView = query.ToList();
//            return listPhongView;
//        }

//        public List<BanView> timKiem(string loaiTimKiem, string mucTimKiem, int giaTriTimKiem)
//        {
//            List<BanView> listPhongView;
//            var query = from s in db.Bans
//                        join r in db.LoaiBans on s.MaLoai equals r.MaLoai
//                        select new BanView
//                        {
//                            MaBan = s.MaBan,
//                            TenPhong = s.TenPhong,
//                            DienTich = s.DienTich,
//                            GiaThue = s.GiaThue,
//                            MaLoai = r.MaLoai,
//                            TenLoai = r.TenLoai,
//                            DuongDanAnh = r.DuongDanAnh
//                        };
//            if (loaiTimKiem == "Số lượng người")
//            {               
//                if (mucTimKiem == "<=") listPhongView = query.Where(m => m.DienTich <= giaTriTimKiem).ToList();
//                else listPhongView = query.Where(m => m.GiaThue >= giaTriTimKiem).ToList();
//            }
//            else
//            {
//                if (mucTimKiem == "<=") listPhongView = query.Where(m => m.DienTich <= giaTriTimKiem).ToList();
//                else listPhongView = query.Where(m => m.GiaThue >= giaTriTimKiem).ToList();
//            }
//            return listPhongView;
//        }

//    }
//}