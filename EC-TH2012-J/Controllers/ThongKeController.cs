using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaHangOnline.Models;

namespace WebNhaHangOnline.Controllers
{
    [AuthLog(Roles = "Quản trị viên")]
    public class ThongKeController : Controller
    {
        //
        // GET: /ThongKe/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongKeDoanhThu()
        {
            return View();
        }

        public ActionResult ThongKeDonHang()
        {
            return View();
        }

        public ActionResult ThongKeKhuyenMai()
        {
            return View();
        }
        public ActionResult ThongKeDTTheoTG(DateTime? froms, DateTime? tos)
        {
            if (froms == null)
                froms = DateTime.Today.AddMonths(-1);
            if (tos == null)
                tos = DateTime.Today;
            ViewBag.froms = froms.Value.ToShortDateString();
            ViewBag.tos = tos.Value.ToShortDateString();
            DonhangKHModel donhang = new DonhangKHModel();
            var List_ = donhang.ThongKeDoanhThu(froms, tos).ToArray();
            return PartialView("TheoTime", donhang.ThongKeDoanhThu(froms, tos).ToList());
        }

        public ActionResult ThongKeDTTheoTG_(DateTime? froms, DateTime? tos)
        {
            if (froms == null)
                froms = DateTime.Today.AddMonths(-1);
            if (tos == null)
                tos = DateTime.Today;
            ViewBag.froms = froms.Value.ToShortDateString();
            ViewBag.tos = tos.Value.ToShortDateString();
            DonhangKHModel donhang = new DonhangKHModel();
            var List_ = donhang.ThongKeDoanhThu(froms, tos).ToList();
            return Json(List_, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ThongKeTiTrong(DateTime? froms, DateTime? tos)
        {
            if (froms == null)
                froms = DateTime.Today.AddMonths(-1);
            if (tos == null)
                tos = DateTime.Today;
            ViewBag.froms = froms.Value.ToShortDateString();
            ViewBag.tos = tos.Value.ToShortDateString();
            DonhangKHModel donhang = new DonhangKHModel();
            var List_ = donhang.ThongKeTiTrong(froms, tos).ToList();
            return PartialView("TheoTiTrong", donhang.ThongKeTiTrong(froms, tos).ToList());
        }

        public ActionResult ThongKeDonHangS(DateTime? froms, DateTime? tos)
        {
            if (froms == null)
                froms = DateTime.Today.AddMonths(-1);
            if (tos == null)
                tos = DateTime.Today;
            ViewBag.froms = froms.Value.ToShortDateString();
            ViewBag.tos = tos.Value.ToShortDateString();
            DonhangKHModel donhang = new DonhangKHModel();
            var List_ = donhang.ThongKeTiTrong(froms, tos).ToList();
            return Json(List_, JsonRequestBehavior.AllowGet);
        }


        public class TKKhuyenMai
        {
            public string TenSP { get; set; }
            public decimal GiaGoc { get; set; }
            public decimal GiaTien { get; set; }
            public int GiamGia { get; set; }
            public DateTime NgayKetThuc { get; set; }
            public DateTime NgayBatDau { get; set; }
        }

        public ActionResult ThongKeKhuyenMais(DateTime? froms, DateTime? tos)
        {
            if (froms == null)
                froms = DateTime.Today.AddMonths(-1);
            if (tos == null)
                tos = DateTime.Today;
            //database
            DateTime datevalue = (Convert.ToDateTime(froms.ToString()));

            String dy = datevalue.Day.ToString();
            String mn = datevalue.Month.ToString();
            String yy = datevalue.Year.ToString();
            var strFrom = yy + "-" + mn + "-" + dy;

            DateTime datevaluetos = (Convert.ToDateTime(tos.ToString()));

            String dytos = datevaluetos.Day.ToString();
            String mntos = datevaluetos.Month.ToString();
            String yytos = datevaluetos.Year.ToString();

            var strtos = yytos + "-" + mntos + "-" + dytos;

            var newlist = new List<TKKhuyenMai>();
            var connectionString = @"Data Source=NGUYENHIEP\SQLEXPRESS;Initial Catalog=WebGiayHangHieu;User ID=sa;Password=123456";
            var queryString = "SELECT SanPham.TenSP, SanPham.GiaGoc, SanPham.GiaTien, SanPhamKhuyenMai.GiamGia , KhuyenMai.NgayKetThuc, KhuyenMai.NgayBatDau FROM KhuyenMai INNER JOIN SanPhamKhuyenMai on KhuyenMai.MaKM = SanPhamKhuyenMai.MaKM INNER JOIN SanPham ON SanPham.MaSP  = SanPhamKhuyenMai.MaSP WHERE KhuyenMai.NgayBatDau <= '@froms'  and KhuyenMai.NgayKetThuc >= '@tos'";
            queryString = queryString.Replace("@froms", strFrom);
            queryString = queryString.Replace("@tos", strtos);
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
                            var kq = Newtonsoft.Json.JsonConvert.DeserializeObject<TKKhuyenMai>(json);
                            newlist.Add(kq);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                connection.Close();
            }
            return Json(newlist, JsonRequestBehavior.AllowGet);
        }
    }
}