using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHangOnline.Models;
using PagedList;
using PagedList.Mvc;
using WebNhaHangOnline.Models;
using System.Data.Entity;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;

namespace WebNhaHangOnline.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult SearchForm()
        {
            return PartialView("_SearchFormPartial");
        }
        public ActionResult CategoryList()
        {
            CategoryModel cat = new CategoryModel();
            var lst = cat.GetCategory().ToList();
            return PartialView("_CategoryListPartial", lst);
        }

        [HttpPost]
        public ActionResult SearchByName(string term)
        {
            SanPhamModel sp = new SanPhamModel();
            IQueryable<SanPham> lst = sp.SearchByName(term);
            var newlist = new List<SanPham>();
            var connectionString = @"Data Source=NGUYENHIEP\SQLEXPRESS;Initial Catalog=WebGiayHangHieu;User ID=sa;Password=123456";
            var queryString = "SELECT SanPham.* FROM SanPham INNER JOIN LoaiSP ON SanPham.LoaiSP = LoaiSP.MaLoai INNER JOIN HangSanXuat ON SanPham.HangSX = HangSanXuat.HangSX WHERE CONTAINS(SanPham.*, '" + '"' + term + "*" + '"' + "') OR CONTAINS(LoaiSP.*, '" + '"' + term + "*" + '"' + "') OR CONTAINS(HangSanXuat.*, '" + '"' + term + "*" + '"' + "')";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        var result = new List<ExpandoObject>();
                        while (reader.Read())
                        {
                            var e = new ExpandoObject();
                            var d = e as IDictionary<string, object>;
                            for (int i = 0; i < reader.FieldCount; i++)
                                d.Add(reader.GetName(i), DBNull.Value.Equals(reader[i]) ? null : reader[i]);
                            result.Add(e);
                        }
                        var data = result;
                        foreach (var item in data)
                        {
                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                            var kq = Newtonsoft.Json.JsonConvert.DeserializeObject<SanPham>(json);
                            newlist.Add(kq);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var log = ex.Message;
                }
                connection.Close();
            }


            //var splist = (from p in newlist orderby p.MaSP descending select new { p.MaSP, p.TenSP, p.GiaTien, p.AnhDaiDien }).Take(5);
            return Json(newlist, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AdvancedSearchView(string term, string loai, string hangsx, int? minprice, int? maxprice)
        {
            ViewBag.Name = term;
            ViewBag.loai = loai;
            ViewBag.hangsx = hangsx;
            ViewBag.minprice = minprice;
            ViewBag.maxprice = maxprice;
            return View("AdvancedSearchView");
        }

        public ActionResult AdvancedSearchP(string term, string loai, string hangsx, string typeview, int? page, int? minprice, int? maxprice)
        {
            ViewBag.Name = term;
            ViewBag.loai = loai;
            ViewBag.hangsx = hangsx;
            ViewBag.minprice = minprice;
            ViewBag.maxprice = maxprice;
            ViewBag.type = typeview;
            SanPhamModel sp = new SanPhamModel();
            IQueryable<SanPham> lst = sp.AdvancedSearch(term, loai, hangsx, minprice, maxprice);
            return PhanTrangAdvanced(lst, page);
        }

        private ActionResult PhanTrangAdvanced(IQueryable<SanPham> lst, int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            lst = lst.OrderByDescending(m => m.MaSP);
            return View("_AdvancedSearchPartial", lst.ToPagedList(pageNumber, pageSize));
        }
    }
}