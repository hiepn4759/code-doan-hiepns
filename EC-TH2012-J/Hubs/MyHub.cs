using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using WebNhaHangOnline.Models.Chat.ViewModels;
using WebNhaHangOnline.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.IO;
using PagedList;
using PagedList.Mvc;
using WebNhaHangOnline.Models;

using WebNhaHangOnline.Models.Chat;

namespace Chat.Web.Hubs
{
    public class MyHub : Hub
    {
        static List<UserInfo> UsersList = new List<UserInfo>();
        static List<MessageInfo> MessageList = new List<MessageInfo>();

        public void Connect(string userName, string password)
        {
            var id = Context.ConnectionId;
            string userGroup = "";

            var ctx = new WebGiayHangHieuEntities();

        }
    }
}