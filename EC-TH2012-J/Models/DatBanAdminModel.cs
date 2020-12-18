using WebNhaHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebNhaHangOnline.Models
{
    public class DatBanAdminModel
    {
        private WebGiayHangHieuEntities db;
        public DatBanAdminModel()
        {
            db = new WebGiayHangHieuEntities();
        }
        //public IQueryable<DatBan> GetCategory()
        //{
        //    IQueryable<DatBan> lst = db.DatBans;
        //    return lst;
        //}

        //internal IEnumerable<Ban> GetAllLoaiSP()
        //{
        //    return db.Bans.ToList();
        //}

        
        //internal IEnumerable<Ban> GetAllLoaiBan()
        //{
        //    return db.Bans.ToList();
        //}

        //internal IEnumerable<SanPham> GetAllSanPham()
        //{
        //    return db.SanPhams.ToList();
        //}

        //internal DatBan FindById(int id)
        //{
        //    return db.DatBans.Find(id);
        //}

        //internal void EditLoaiBan(DatBan loai)
        //{
        //    DatBan lsp = db.DatBans.Find(loai.BookingId);
        //    //lsp. = loai.TenLoai;
        //    db.Entry(lsp).State = EntityState.Modified;
        //    db.SaveChanges();
        //}

        //internal void DeleteLoaiBan(string id)
        //{
        //    DatBan loai = db.DatBans.Find(id);
        //    db.DatBans.Remove(loai);
        //    db.SaveChanges();
        //}

        //internal int ThemLoaiBan(DatBan loai)
        //{
        //    loai.BookingId = TaoMa();
        //    loai.StartDate = DateTime.Today;
        //    db.DatBans.Add(loai);
        //    db.SaveChanges();
        //    return loai.BookingId;
        //}

        //private int TaoMa()
        //{
        //    int maID;
        //    Random rand = new Random();
        //    do
        //    {
        //        maID = 1;
        //        for (int i = 0; i < 5; i++)
        //        {
        //            maID += rand.Next(9);
        //        }
        //    }
        //    while (!KiemtraID(maID));
        //    return maID;
        //}

        //private bool KiemtraID(int maID)
        //{
        //    var temp = db.DatBans.Find(maID);
        //    if (temp == null)
        //        return true;
        //    return false;
        //}

        //internal IQueryable<DatBan> SearchByName(string key)
        //{
        //    if (string.IsNullOrEmpty(key))
        //        return db.DatBans;
        //    return db.DatBans.Where(u => u.TableName.Contains(key));
        //}


        //internal bool KiemTraTen(string p)
        //{
        //    var temp = db.DatBans.Where(m => m.TableName.Equals(p)).ToList();
        //    if (temp.Count == 0)
        //        return true;
        //    return false;
        //}
    }
}