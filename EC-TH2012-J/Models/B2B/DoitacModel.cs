using WebNhaHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNhaHangOnline.Models
{
    public class DoitacModel
    {
        public List<NhaCungCap> LayDoitac()
        {
            using(WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
            {
                var ds = (from p in db.NhaCungCaps where p.Net_user == null select p).ToList();
                //var ds = (from p in db.NhaCungCaps select p).ToList();
                return ds;
            }
        }
    }
}