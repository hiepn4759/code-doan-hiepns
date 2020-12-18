using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNhaHangOnline.Models;

namespace BusinessLayer.Business.MainMenu
{
    public class MainMenuModel
    {
        private WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();

        public List<Link> GetMenuLink()
        {
            return db.Links.Where(m => m.Group.Equals("MainMenu")).ToList();
        }
        public List<MenuItem> GetMenuList() 
        { 
            List<MenuItem> mnlist = new List<MenuItem>();
            var loaiSPlst = db.LoaiSPs.OrderBy(a => a.MaLoai).Where(a => !a.MaLoai.Equals("NOTTT")).ToList();
            foreach (var item in loaiSPlst)
            {
                MenuItem mnitem = new MenuItem();
                mnitem.LoaiSP = item;
                mnitem.HangSX = this.GetHangSXLst(item.MaLoai);
                mnlist.Add(mnitem);
            }
            return mnlist;
        }

        private List<WebNhaHangOnline.Models.HangSanXuat> GetHangSXLst(string maloai)
        {
            List<WebNhaHangOnline.Models.HangSanXuat> hsxlist = (from p in db.SanPhams where p.LoaiSP == maloai select p.HangSanXuat).Distinct().ToList();
            return hsxlist;
        }
    }

    public class MenuItem
    {
        public LoaiSP LoaiSP { get; set; }
        public List<WebNhaHangOnline.Models.HangSanXuat> HangSX { get; set; }
    }
}

