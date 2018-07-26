
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Aras.IOM;
using System.Threading.Tasks;

namespace ArasMVC01.Controllers
{
    public class CalendarController : Controller
    {
        //static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "ArasMVC01";
        // GET: Canlendar
        public ActionResult Index()
        {
            //var credential = await GetCredentialForApiAsync();
            return View();
        }
        //private readonly IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);

        //private async Task<UserCredential> GetCredentialForApiAsync()
        //{
        //    var initializer = new GoogleAuthorizationCodeFlow.Initializer
        //    {
        //        ClientSecrets = new ClientSecrets
        //        {
        //            ClientId = MyClientSecrets.ClientId,
        //            ClientSecret = MyClientSecrets.ClientSecret,
        //        },
        //        Scopes = MyRequestedScopes.Scopes,
        //    };
        //    var flow = new GoogleAuthorizationCodeFlow(initializer);

        //    var identity = await HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(
        //        DefaultAuthenticationTypes.ApplicationCookie);
        //    var userId = identity.FindFirstValue(MyClaimTypes.GoogleUserId);

        //    var token = await dataStore.GetAsync<TokenResponse>(userId);
        //    return new UserCredential(flow, userId, token);
        //}
        [HttpPost]
        public async Task<JsonResult> GetCalendarTask()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            
            List<CalendarPatial> calendarList = new List<CalendarPatial>();
            int row = 0;
            //using (var stream =
            //    new FileStream(Server.MapPath("\\FileUploads\\goolgle-license.json"), FileMode.Open, FileAccess.Read))
            //{
            //    string credPath = Server.MapPath("\\Checkout\\goolgle-license.json");

            //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
            //            Scopes,
            //            "229463515451-r1349f18r3i7sdk8mhvhf0vgse7dkrpk.apps.googleusercontent.com",
            //            CancellationToken.None,
            //            new FileDataStore(credPath, true)).Result;
            //}
            //var model = new UpcomingEventsViewModel();

            //var credential = await GetCredentialForApiAsync();

            //var initializer = new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = "ASP.NET MVC5 Calendar Sample",
            //};
            //// Create Google Calendar API service.
            //var service = new CalendarService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = ApplicationName,
            //});

            //// Define parameters of request.
            //EventsResource.ListRequest request = service.Events.List("primary");
            //request.TimeMin = DateTime.Now;
            //request.ShowDeleted = true;
            //request.SingleEvents = true;
            //request.MaxResults = 10;
            //request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            //// List events.
            //Events events = request.Execute();
            //if (events.Items != null && events.Items.Count > 0)
            //{
            //    foreach (var eventItem in events.Items)
            //    {
            //        string when = eventItem.Start.DateTime.ToString();
            //        if (String.IsNullOrEmpty(when))
            //        {
            //            when = eventItem.Start.Date.ToString();
            //            DateTime d = new DateTime();
            //            d = DateTime.Parse(when);
            //            when = d.ToString("yyyy-MM-ddTHH:mm:ss");
                        
            //        }
                   
            //        string str = string.Format("{0} ({1})", eventItem.Summary, when);
            //        CalendarPatial cal = new CalendarPatial();
            //        cal.title = eventItem.Summary;
            //        cal.start = when;
            //        cal.description=eventItem.Description;
            //        calendarList.Add(cal);
            //    }
            //}
            //else
            //{
               
            //}
            
            return Json(calendarList);
        }
        [HttpGet]
        public JsonResult GetArasWork()
        {
            List<CalendarPatial> calendarList = new List<CalendarPatial>();
            if (Session["innovator"] != null)
            {
                Innovator inn = (Innovator)Session["innovator"];
                Item work_note = inn.newItem("work_note","get");
                work_note = work_note.apply();
                if (work_note.isError() == false)
                {
                    for(int i=0;i< work_note.getItemCount(); i++)
                    {
                        Item work = work_note.getItemByIndex(i);
                        CalendarPatial cal = new CalendarPatial();
                        cal.id = work.getProperty("id", "");
                        cal.title = work.getProperty("title", "");
                        cal.description = work.getProperty("content", "");
                        cal.start = work.getProperty("startdate", "");
                        cal.end = work.getProperty("enddate", "");
                        cal.className = "plm_work";
                        calendarList.Add(cal);
                    }
                }
            } 
            return Json(calendarList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewWork(CalendarPatial calendar)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (Session["innovator"] != null)
            {
                Innovator inn = (Innovator)Session["innovator"];

                Item work_note;
                if (string.IsNullOrWhiteSpace(calendar.id))
                {
                    work_note = inn.newItem("work_note", "add");
                }
                else
                {
                    work_note = inn.newItem("work_note", "edit");
                    work_note.setAttribute("where","id='"+ calendar.id+"'");
                }

               
                work_note.setProperty("title", calendar.title);
                work_note.setProperty("content", calendar.description);
                work_note.setProperty("startdate", calendar.start+"T00:00:00");
                if (calendar.end == "") calendar.end = calendar.start;
                work_note.setProperty("enddate", calendar.end + "T00:00:00");
                string beforeAml = work_note.dom.OuterXml;
                work_note = work_note.apply();
                if (work_note.isError())
                {
                    result.Add("error", work_note.getErrorString());
                    result.Add("error_aml" , beforeAml);
                }
                else
                {
                    result.Add("result", work_note.dom.OuterXml);
                }
            }
            return Json(result);
        }
        // GET: Canlendar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: Canlendar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Canlendar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Canlendar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Canlendar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Canlendar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Canlendar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
