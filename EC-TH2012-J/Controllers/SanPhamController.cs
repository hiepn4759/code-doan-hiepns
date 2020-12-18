using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHangOnline.Models;
using PagedList;
using PagedList.Mvc;
using WebNhaHangOnline.App_Start;
using WebNhaHangOnline.Models;

namespace WebNhaHangOnline.Controllers
{
    public class SanPhamController : Controller
    {
        private WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();
        private SanPhamModel sp = new SanPhamModel();
        // GET: SanPham
        [Trackingactionfilter]
        public ActionResult Index(string id)
        {
            SanPham sp = db.SanPhams.Find(id);
            return View("ProductDetail",sp);
        }
        public ActionResult SearchByName(string tensp)
        {
            var splist = db.SanPhams.Where(u => u.TenSP.Contains(tensp));
            ViewBag.CurrentFilter = tensp;
            splist = splist.OrderByDescending(u => u.TenSP);
            return View(splist);
        }

        public ActionResult SearchByType(string MaLoai)
        {
            var splist = (from p in db.SanPhams where p.LoaiSP.Equals(MaLoai) select p);
            return View(splist);
        }
        public ActionResult Loadsplienquan(string maloai,int sl)
        {
            IQueryable<SanPham> splist = sp.SearchByType(maloai);
            splist = splist.Take(sl);
            return PartialView("_PartialSanPhamLienQuan", splist);
        }
        
        public ActionResult ThongSoKyThuat(string MaSP)
        {
            SanPhamModel spm = new SanPhamModel();
            return PartialView("ThongSoKyThuatPartial", spm.GetTSKT(MaSP));
        }

    }
}