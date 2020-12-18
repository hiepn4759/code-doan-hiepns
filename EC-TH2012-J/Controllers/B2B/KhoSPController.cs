using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHangOnline.Models;
using PagedList;
using System.Net;
using WebNhaHangOnline.Models.B2B;

namespace WebNhaHangOnline.Controllers.B2B
{

    [AuthLog(Roles = "Quản trị viên")]
    public class KhoSPController : Controller
    {
        // GET: KhoSP
        public ActionResult Index()
        {
            SanPhamModel spm = new SanPhamModel();
            ViewBag.ALlSP = new SelectList(spm.GetAllSP(), "MaSP", "TenSP");
            return View();
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSLKhoSP(KhoSP KhoSanPham)
        {
            KhoSPModel spm = new KhoSPModel();
            if (ModelState.IsValid)
            {
                spm.ThemKhoSP(KhoSanPham);
            }
            return Redirect("Index");
       }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKhoS(TongSPKho loai)
        {
            TongSPKHoModel spm = new TongSPKHoModel();
            if (ModelState.IsValid)
            {
                spm.EditLoaiSP(loai);
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        public ActionResult EditKhoS(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TongSPKHoModel spm = new TongSPKHoModel();
            TongSPKho sp = spm.FindByIdNCCOne(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        public void  DeleteSPKho(string id)
        {
            if (id == null)
            {
                
            }
            TongSPKHoModel spm = new TongSPKHoModel();
            KhoSPModel khmodel = new KhoSPModel();

            var listss = spm.FindById(id);
            //khmodel.DeleteKhoSP(listss.IDSP);
            spm.DeleteLoaiSP(id);
        }
    }
}