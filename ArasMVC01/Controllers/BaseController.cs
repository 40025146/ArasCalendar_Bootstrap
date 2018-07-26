using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aras.IOM;
using ArasMVC01.Models;
namespace ArasMVC01.Controllers
{
    public class BaseController : Controller
    {
       
        public class LoginInfo
        {
            public string 網路位址 { get; set; }
            public string 資料庫名稱 { get; set; }
            public string 帳號 { get; set; }
            public string 密碼 { get; set; }
        }
    }
}