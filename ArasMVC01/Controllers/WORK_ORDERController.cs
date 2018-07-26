using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArasMVC01.Models;
using Aras.IOM;
using ArasMVC01.ActionFilter;
namespace ArasMVC01.Controllers
{
    public class WORK_ORDERController : BaseController
    {
        private InnovatorSolutionsEntities db = new InnovatorSolutionsEntities();
        public WORK_ORDERRepository WorkOrderRepo =RepositoryHelper.GetWORK_ORDERRepository();
        // GET: WORK_ORDER
        public ActionResult Index()
        {
            var data = WorkOrderRepo.FindAll();
            //var data = db.WORK_ORDER.ToList();
            return View(data);
        }

        // GET: WORK_ORDER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WORK_ORDER wORK_ORDER = db.WORK_ORDER.Find(id);
            if (wORK_ORDER == null)
            {
                return HttpNotFound();
            }
            return View(wORK_ORDER);
        }

        // GET: WORK_ORDER/Create
        [ShareActionFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: WORK_ORDER/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "START_DATE,FINISH_DATE,ITEM_NUMBER,COST")] WORK_ORDER wORK_ORDER)
        {
            Innovator inn = (Innovator)Session["innovator"];
            
            Item itm = inn.newItem("work order","add");
            itm.setProperty("START_DATE".ToLower(), wORK_ORDER.START_DATE.ToString());
            itm.setProperty("FINISH_DATE".ToLower(), wORK_ORDER.FINISH_DATE.ToString());
            itm.setProperty("ITEM_NUMBER".ToLower(), wORK_ORDER.ITEM_NUMBER.ToString());
            itm.setProperty("COST".ToLower(), wORK_ORDER.COST.ToString());
            itm = itm.apply();
            if (itm.isError())
            {
                TempData["Message"] = itm.getErrorString();
            }
            else
            {
                TempData["Message"] = "新增成功";
            }
            return RedirectToAction("Index");
            

            return View(wORK_ORDER);
        }

        // GET: WORK_ORDER/Edit/5
        [ShareActionFilter]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WORK_ORDER wORK_ORDER = db.WORK_ORDER.Find(id);
            if (wORK_ORDER == null)
            {
                return HttpNotFound();
            }
            return View(wORK_ORDER);
        }

        // POST: WORK_ORDER/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,START_DATE,FINISH_DATE,ITEM_NUMBER,COST")] WORK_ORDER wORK_ORDER)
        {
            Innovator inn = (Innovator)Session["innovator"];
           
            Item itm = WorkOrderRepo.FindItemById(inn,"work order",wORK_ORDER.id);
            itm.setProperty("START_DATE".ToLower(),wORK_ORDER.START_DATE.ToString());
            itm.setProperty("FINISH_DATE".ToLower(), wORK_ORDER.FINISH_DATE.ToString());
            itm.setProperty("ITEM_NUMBER".ToLower(), wORK_ORDER.ITEM_NUMBER.ToString());
            itm.setProperty("COST".ToLower(), wORK_ORDER.COST.ToString());
            WorkOrderRepo.EditItem(itm);
            if (itm.isError() == true)
            {
                TempData["Message"] = itm.getErrorString();
            }
            else
            {
                TempData["Message"] = "修改成功";
                return RedirectToAction("Index");
            }
            
            
            return View(wORK_ORDER);
        }

        // GET: WORK_ORDER/Delete/5
        [ShareActionFilter]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WORK_ORDER wORK_ORDER = db.WORK_ORDER.Find(id);
            if (wORK_ORDER == null)
            {
                return HttpNotFound();
            }
            return View(wORK_ORDER);
        }

        // POST: WORK_ORDER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WORK_ORDER wORK_ORDER = db.WORK_ORDER.Find(id);
            db.WORK_ORDER.Remove(wORK_ORDER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
