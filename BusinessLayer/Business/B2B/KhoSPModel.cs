using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BusinessLayer.Business.SanPham;
using WebNhaHangOnline.Models;

namespace BusinessLayer.Business.B2B
{
    public class KhoSPModel
    {
        protected WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();


        public IQueryable<KhoSP> TimNCC(string key)
        {
            IQueryable<KhoSP> lst = db.KhoSPs;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.MaSP.Contains(key));
            return lst;
        }

        public void EditLoaiSP(KhoSP loai)
        {
            KhoSP lsp = db.KhoSPs.Find(loai.IDKho);

            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool KiemTraTen(string p)
        {
            var temp = db.KhoSPs.Where(m => m.MaSP.Equals(p)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }

        public string ThemHangNCC(KhoSP loai)
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

        public KhoSP FindByIdNCC(string id)
        {
            return db.KhoSPs.Find(id);
        }


        public void EditNCC(KhoSP loai)
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

        public void DeleteKhoSP(string id)
        {
            var loai = db.KhoSPs.Where(d => d.MaSP == id);
            foreach(var item in loai)
            {
                db.KhoSPs.Remove(item);
                db.SaveChanges();
            }
            
        }
        public void ThemKhoSP(KhoSP model)
        {
            var newSP =new SanPham.SanPhamModel();
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
            }
            else
            {
                TongSPKho lsp = db.TongSPKhoes.Where(d => d.IDKho == checkID.IDKho).FirstOrDefault();
                lsp.SL += model.SL;
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



        //public void UpdateInfo(EditInfo2B2ViewModel info)
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