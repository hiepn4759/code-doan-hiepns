using WebNhaHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebNhaHangOnline.Models.B2B
{
    public class KhoSPModel
    {
        protected WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();


        internal IQueryable<KhoSP> TimNCC(string key)
        {
            IQueryable<KhoSP> lst = db.KhoSPs;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.MaSP.Contains(key));
            return lst;
        }

        internal void EditLoaiSP(KhoSP loai)
        {
            KhoSP lsp = db.KhoSPs.Find(loai.IDKho);

            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal bool KiemTraTen(string p)
        {
            var temp = db.KhoSPs.Where(m => m.MaSP.Equals(p)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }

        internal string ThemHangNCC(KhoSP loai)
        {
            loai.IDKho = TaoMa();
            db.KhoSPs.Add(loai);
            db.SaveChanges();
            return loai.IDKho;
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
            var temp = db.KhoSPs.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        internal KhoSP FindByIdNCC(string id)
        {
            return db.KhoSPs.Find(id);
        }


        internal void EditNCC(KhoSP loai)
        {
            try
            {
                KhoSP lsp = db.KhoSPs.Find(loai.IDKho);
                lsp.MaSP = loai.MaSP;
                lsp.SL = loai.SL;
                db.Entry(lsp).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

        }

        internal void DeleteKhoSP(string id)
        {
            var loai = db.KhoSPs.Where(d => d.MaSP == id);
            foreach(var item in loai)
            {
                db.KhoSPs.Remove(item);
                db.SaveChanges();
            }
            
        }
        internal void ThemKhoSP(KhoSP model)
        {
            var newSP =new SanPhamModel();
            var listSPId = newSP.FindById(model.MaSP);
            model.TenSP = listSPId.TenSP;
            model.IDKho = TaoMa();
            model.MaSP = model.MaSP;
            model.SL = model.SL;
            model.DateImport = DateTime.Now;
            model.DateExport = DateTime.Now;

            // add tong sl kho
            
            


            var newTongKho = new TongSPKHoModel();
            var checkID = newTongKho.FindByIdNCC(model.MaSP).FirstOrDefault() ;
            if (checkID == null)
            {
                //create
                var newModelTongKho = new TongSPKho();
                newModelTongKho.IDKho = model.IDKho;
                newModelTongKho.IDSP = model.MaSP;
                newModelTongKho.SL = model.SL;
                db.TongSPKhoes.Add(newModelTongKho);

                //find id sp
                var newsp = new SanPhamModel();
                var sp = db.SanPhams.Find(model.MaSP);
                sp.SoLuong = model.SL;
                db.Entry(sp).State = EntityState.Modified;
            }
            else
            {
                TongSPKho lsp = db.TongSPKhoes.Where(d => d.IDKho == checkID.IDKho).FirstOrDefault();
                lsp.SL += model.SL;

                //find id sp
                var newsp = new SanPhamModel();
                var sp = db.SanPhams.Find(model.MaSP);
                sp.SoLuong = lsp.SL;

                db.Entry(lsp).State = EntityState.Modified;
            }
            db.KhoSPs.Add(model);
            db.SaveChanges();
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
            var temp = db.KhoSPs.Find(maID);
            if (temp == null)
                return true;
            return false;
        }



        //internal void UpdateInfo(EditInfo2B2ViewModel info)
        //{
        //    NhaCungCap lsp = db.KhoSPs.Find(info.MaNCC);
        //    lsp.TenNCC = info.TenNCC;
        //    lsp.DiaChi = info.DiaChi;
        //    lsp.SDT_NCC = info.SDT_NCC;
        //    lsp.Email = info.Email;
        //    db.Entry(lsp).State = EntityState.Modified;
        //    db.SaveChanges();
        //}
    }
}