﻿using WebNhaHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebNhaHangOnline.Models
{
    public class UserModel
    {
        WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();
        internal AspNetUser FindById(string p)
        {
            return db.AspNetUsers.Find(p);
        }

        internal void UpdateInfo(EditInfoModel info, string id)
        {
            AspNetUser user = new AspNetUser();

            user = db.AspNetUsers.Find(id);
            if (user != null)
            {
                user.Email = info.Email;
                user.PhoneNumber = info.DienThoai;
                user.CMND = info.CMND;
                user.HoTen = info.HoTen;
                user.NgaySinh = info.NgaySinh;
                user.GioiTinh = info.GioiTinh;
                user.DiaChi = info.DiaChi;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal void UpdateImage(string p)
        {
            AspNetUser user = new AspNetUser();
            user = db.AspNetUsers.Find(p);
            if (user != null)
            {
                user.Avatar = p + ".jpg";
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        internal IQueryable<AspNetUser> SearchUser(string key, string email, string hoten, string phone, string quyen)
        {
            IQueryable<AspNetUser> lst = db.AspNetUsers;
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.UserName.Contains(key));
            if (!string.IsNullOrEmpty(email))
                lst = lst.Where(m => m.Email.Contains(email));
            if (!string.IsNullOrEmpty(hoten))
                lst = lst.Where(m => m.HoTen.Contains(hoten));
            if (!string.IsNullOrEmpty(phone))
                lst = lst.Where(m => m.PhoneNumber.Contains(phone));
            if (!string.IsNullOrEmpty(quyen))
                lst = lst.Where(m => m.AspNetRoles.FirstOrDefault().Id.Equals(quyen));
            return lst;
        }

        internal IQueryable<AspNetUser> SearchUserNV(string key, string email, string hoten, string phone, string quyen)
        {
            IQueryable<AspNetUser> lst = db.AspNetUsers.Where(d => d.RoleID == "8d050225-c36f-4790-bd7b-28bb044d90b1");
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.UserName.Contains(key));
            if (!string.IsNullOrEmpty(email))
                lst = lst.Where(m => m.Email.Contains(email));
            if (!string.IsNullOrEmpty(hoten))
                lst = lst.Where(m => m.HoTen.Contains(hoten));
            if (!string.IsNullOrEmpty(phone))
                lst = lst.Where(m => m.PhoneNumber.Contains(phone));
            if (!string.IsNullOrEmpty(quyen))
                lst = lst.Where(m => m.AspNetRoles.FirstOrDefault().Id.Equals(quyen));
            return lst;
        }
        internal IQueryable<AspNetUser> SearchUserAdmin(string key, string email, string hoten, string phone, string quyen)
        {
            IQueryable<AspNetUser> lst = db.AspNetUsers.Where(d => d.RoleID == "dc481379-dfae-4d9e-93d8-dce55add3258");
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.UserName.Contains(key));
            if (!string.IsNullOrEmpty(email))
                lst = lst.Where(m => m.Email.Contains(email));
            if (!string.IsNullOrEmpty(hoten))
                lst = lst.Where(m => m.HoTen.Contains(hoten));
            if (!string.IsNullOrEmpty(phone))
                lst = lst.Where(m => m.PhoneNumber.Contains(phone));
            if (!string.IsNullOrEmpty(quyen))
                lst = lst.Where(m => m.AspNetRoles.FirstOrDefault().Id.Equals(quyen));
            return lst;
        }

        internal IQueryable<AspNetUser> SearchUserKH(string key, string email, string hoten, string phone,string quyen)
        {
            IQueryable<AspNetUser> lst = db.AspNetUsers.Where(d => d.RoleID == "37877e60-4cd3-4eb3-8a94-a5d881a81446");
            if (!string.IsNullOrEmpty(key))
                lst = lst.Where(m => m.UserName.Contains(key));
            if (!string.IsNullOrEmpty(email))
                lst = lst.Where(m => m.Email.Contains(email));
            if (!string.IsNullOrEmpty(hoten))
                lst = lst.Where(m => m.HoTen.Contains(hoten));
            if (!string.IsNullOrEmpty(phone))
                lst = lst.Where(m => m.PhoneNumber.Contains(phone));
            if (!string.IsNullOrEmpty(quyen))
                lst = lst.Where(m => m.AspNetRoles.FirstOrDefault().Id.Equals(quyen));
            return lst;
        }

        internal IQueryable<AspNetRole> GetAllRole()
        {
            return db.AspNetRoles;
        }

        internal void deleleallrole(string item, string quyen)
        {
            var user = db.AspNetUsers.Find(item);
            if(quyen == "Bị khóa")
            {
                user.RoleID = "a00f8232-0674-4ac3-8a6b-89bd5ce471da";
            }
            else  if(quyen == "Khách hàng")
            {
                user.RoleID = "37877e60-4cd3-4eb3-8a94-a5d881a81446";
            }
            else if(quyen == "Nhân viên")
            {
                user.RoleID = "8d050225-c36f-4790-bd7b-28bb044d90b1";
            }else if(quyen == "Nhân Viên Đơn Hàng")
            {
                user.RoleID = "ec481379-dfae-4d9e-93d8-dce55add3258";
            }else if(quyen == "Quản trị viên")
            {
                user.RoleID = "dc481379-dfae-4d9e-93d8-dce55add3258";
            }
            db.Entry(user).Collection("AspNetRoles").Load();
            user.AspNetRoles.Remove(user.AspNetRoles.FirstOrDefault());
            db.SaveChanges();
        }


        internal bool ConfirmMail(string id)
        {
            AspNetUser user = new AspNetUser();

            user = db.AspNetUsers.Find(id);
            if (user != null)
            {
                user.EmailConfirmed = true;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        internal void SendMailConfirm(string p,string url)
        {
            EmailTool sendmail = new EmailTool();
            AspNetUser us = db.AspNetUsers.Find(p);
            if(us != null)
            {
                string mail = us.Email;
                string sub = "[Xác nhận email] Xác nhận đăng ký tại Shop_HiepNS";
                string bo = "";
                bo += "Xin chào " + us.HoTen + ",<br>";
                bo += "Cảm ơn bạn đã đăng ký tịa Shop_HiepNS, đây là link xác nhận email của bạn <br>";
                bo += "Click vào link bên dưới để xác nhận:<br>";
                bo += "<a href=\""+ url +"\">" + url + "</a><br>";
                bo += "Xin cảm ơn.";
                sendmail.SendMail(new EmailModel(mail, sub, bo));
            }

        }
    }
}