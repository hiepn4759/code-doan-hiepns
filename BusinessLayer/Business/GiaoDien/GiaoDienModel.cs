using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNhaHangOnline.Models;

namespace BusinessLayer.Business.GiaoDien
{
    public class GiaoDienModel
    {
        private WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();

        internal IQueryable<WebNhaHangOnline.Models.GiaoDien> GetDD()
        {
            return db.GiaoDiens;
        }

        internal IQueryable<Link> GetSlideShow()
        {
            return db.Links.Where(m => m.Group.Contains("SlideShow"));
        }
    }
}