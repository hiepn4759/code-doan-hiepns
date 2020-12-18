using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNhaHangOnline.Models;

namespace BusinessLayer.Business.B2B
{
    public class DoitacModel
    {
        public List<NhaCungCap> LayDoitac()
        {
            using (WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
            {
                var ds = (from p in db.NhaCungCaps where p.Net_user == null select p).ToList();
                //var ds = (from p in db.NhaCungCaps select p).ToList();
                return ds;
            }
        }
    }
}
