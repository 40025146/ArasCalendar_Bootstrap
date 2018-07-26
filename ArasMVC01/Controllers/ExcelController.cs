using Aras.IOM;
using ArasMVC01.Controllers.ExcelObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArasMVC01.Controllers
{
    public class ExcelController : Controller
    {
        // GET: Excel
        public ActionResult Index()
        { 
            return View();
        }
        [HttpPost]
        public JsonResult Upload()
        {
            object obj = null;
            Dictionary<string, object> jo = new Dictionary<string, object>();
            //## 如果有任何檔案類型才做
            if (Request.Files.AllKeys.Any())
            {
                //## 讀取指定的上傳檔案ID
                var httpPostedFile = Request.Files["UploadedImage"];
                var isHeader =Request.Form["isHead"];
                var importStartRow = Request.Form["importStartRow"];
                //## 真實有檔案，進行上傳
                if (httpPostedFile != null && httpPostedFile.ContentLength != 0)
                {
                    var fileName = Path.GetFileName(httpPostedFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/FileUploads"), fileName);
                    httpPostedFile.SaveAs(path);
                    jo.Add("result", true);
                    ExcelTool exceltool =new ExcelTool();
                    obj =  exceltool.ExcelToJson(path, isHeader, importStartRow);
                    //jo.Add("result", obj);
                }
            }
            JsonResult jOBJ = Json(obj);
            return Json(obj);
        }
        [HttpPost]
        public JsonResult ApplyAML(string aml)
        {
            Dictionary<string, object> jo = new Dictionary<string, object>();
            Innovator inn = (Innovator)Session["innovator"];
            string message = "";
            if (inn != null)
            {
                try
                {

                    aml = aml.Replace("&lt", "<").Replace("&gt", ">").Replace("\\n", "");
                    Item itm = inn.applyAML(aml);

                    if (itm.isError())
                    {
                        message = itm.getErrorString();
                        jo.Add("status", "Error");
                        jo.Add("message", message);
                    }
                    else
                    {
                        jo.Add("status", "OK");
                    }
                }
                catch (Exception ex)
                {
                    message = ex.ToString();
                    jo.Add("status", "Error");
                    jo.Add("message", message);
                }
            }
            else
            {
                jo.Add("status", "Error");
                jo.Add("message", "Login is error.");
            }

            return Json(jo);
        }
    }
}