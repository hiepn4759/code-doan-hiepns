//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace WebNhaHangOnline.Models
//{
//    public class FDatBan
//    {
//        private Entities db = new Entities();

//        public FDatBan()
//        {
//            db = new Entities();
//        }

//        public IQueryable<DatBan> DatPhongs
//        {
//            get { return db.DatBans; }
//        }

//        public int Insert(DatBan model)
//        {
//            db.DatBans.Add(model);
//            db.SaveChanges();
//            return model.BookingId;

//            //loai.BookingId = TaoMa();
//            //loai.StartDate = DateTime.Today;
//            //db.DatBans.Add(loai);
//            //db.SaveChanges();
//            //return loai.BookingId;
//        }

//        public int Update(DatBan model)
//        {
//            DatBan dbEntry = db.DatBans.Find(model.BookingId);
//            if (dbEntry == null)
//            {
//                return -1;
//            }
//            dbEntry.BookingId = model.BookingId;
//            dbEntry.TableName = model.TableName;
//            dbEntry.StartDate = model.StartDate;
//            dbEntry.EndDate = model.EndDate;
//            //dbEntry.NgayTra = model.NgayTra;
//            dbEntry.TotalPerson = model.TotalPerson;
//            db.SaveChanges();
//            return model.BookingId;
//        }

//        public int Delete(int MaDatPhong)
//        {
//            DatBan dbEntry = db.DatBans.Find(MaDatPhong);
//            if (dbEntry == null)
//            {
//                return -1;
//            }
//            db.DatBans.Remove(dbEntry);
//            db.SaveChanges();
//            return MaDatPhong;
//        }
//    }
//}