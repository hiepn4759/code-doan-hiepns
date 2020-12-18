using WebNhaHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNhaHangOnline.Models
{
    public class GiaoDienModel
    {
        private WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();

        internal IQueryable<GiaoDien> GetDD()
        {
            return db.GiaoDiens;
        }

        internal IQueryable<Link> GetSlideShow()
        {
            return db.Links.Where(m => m.Group.Contains("SlideShow"));
        }
    }
}