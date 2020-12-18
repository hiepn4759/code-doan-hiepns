using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHangOnline.Models;
using WebNhaHangOnline.Models.B2B;

namespace WebNhaHangOnline.Controllers.B2B
{
    public class TongSPKhoController : Controller
    {
        // GET: TongSPKho
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TimTongSPKho(string key, int? page)
        {
            TongSPKHoModel spk = new TongSPKHoModel();
            ViewBag.key = key;
            return PhanTrangSP(spk.SearchByName(key), page, null);
        }

        public ActionResult PhanTrangSP(IQueryable<TongSPKho> lst, int? page, int? pagesize)
        {
            var newListArr = new List<TongSPKho>();
            var obj = new TongSPKho();
            TongSPKHoModel spk = new TongSPKHoModel();
            SanPhamModel spmodel = new SanPhamModel();
            KhoSPModel khosp = new KhoSPModel();
            foreach (var item in lst)
            {
                var idSP = spk.FindById(item.IDKho);
                var TenSP = spmodel.getSanPham(idSP.IDSP).TenSP;
                obj = new TongSPKho
                {
                    IDSP = TenSP,
                   SL = idSP.SL,
                    IDKho = idSP.IDKho
                };
                newListArr.Add(obj);
            }
            return PartialView("TimTongSPKho", newListArr);
        }
    }
}