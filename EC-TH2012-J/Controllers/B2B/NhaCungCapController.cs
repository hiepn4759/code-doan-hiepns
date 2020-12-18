using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHangOnline.Models;
using PagedList;
using System.Net;

namespace WebNhaHangOnline.Controllers.B2B
{
    // set quyền
    [AuthLog(Roles = "Quản trị viên")]
    public class NhaCungCapController : Controller
    {
        //
        // nha cung cap
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SearchByName(string term)
        {
            NhaCungCapModel sp = new NhaCungCapModel();
            IQueryable<NhaCungCap> lst = sp.TimNCC(term);
            var splist = (from p in lst orderby p.TenNCC descending select new { p.MaNCC, p.TenNCC }).Take(5);
            return Json(splist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TimNCC(string key, int? page)
        {
            NhaCungCapModel ncc = new NhaCungCapModel();
            ViewBag.key = key;
            return PhanTrangNCC(ncc.TimNCC(key).Where(m=>!string.IsNullOrEmpty(m.Net_user)), page, null);
        }

        public ActionResult PhanTrangNCC(IQueryable<NhaCungCap> lst, int? page, int? pagesize)
        {
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return PartialView("NCCPartial", lst.OrderBy(m => m.TenNCC).ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNCC(NhaCungCap loai)
        {
            NhaCungCapModel spm = new NhaCungCapModel();
            if (ModelState.IsValid && spm.KiemTraTen(loai.TenNCC))
            {
                string maloai = spm.ThemHangNCC(loai);
                return View("Index");
            }
            return View("Index", loai);
        }

        public ActionResult kiemtra(string key)
        {
            NhaCungCapModel spm = new NhaCungCapModel();
            if (spm.KiemTraTen(key))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditNhaCungCap(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCapModel lm = new NhaCungCapModel();
            NhaCungCap sp = lm.FindByIdNCC(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNhaCungCap(NhaCungCap loai)
        {
            NhaCungCapModel spm = new NhaCungCapModel();
            if (ModelState.IsValid)
            {
                spm.EditNCC(loai);
                return RedirectToAction("Index");
            }
            return View(loai);
        }


        public ActionResult DeleteNhaCungCap(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCapModel spm = new NhaCungCapModel();
            if (spm.FindByIdNCC(id) == null)
            {
                return HttpNotFound();
            }
            spm.DeleteNhaCungCap(id);
            return TimNCC(null, null);
        }

        [HttpPost]
        public ActionResult MultibleDel(List<string> lstdel)
        {
            if (lstdel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var item in lstdel)
            {
                NhaCungCapModel spm = new NhaCungCapModel();
                if (spm.FindByIdNCC(item) == null)
                {
                    return HttpNotFound();
                }
                spm.DeleteNhaCungCap(item);
            }
            return TimNCC(null, null);
        }
    }
}