using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aras.IOM;
namespace ArasMVC01.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Session["innovator"] != null)
            {
                var q = Session["innovator"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginInfo[] info)
        {
            string message = "";
            if (info != null)
            {
                HttpServerConnection cnx = IomFactory.CreateHttpServerConnection(info[0].網路位址, info[0].資料庫名稱,info[0].帳號, info[0].密碼);
                Item login_result = cnx.Login();
                if (login_result.isError())
                {
                    message = "登入失敗:" + login_result.getErrorString();
                }
                else {
                    message = "登入成功";
                    Session["innovator"] = login_result.getInnovator();

                    string uID = login_result.getInnovator().getUserID();
                    Session["user_id"] = uID;
                    Session["user_name"] = login_result.getInnovator().getItemById("User", uID).getProperty("keyed_name", "");
                }
                TempData["Result"] = message;
                
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}