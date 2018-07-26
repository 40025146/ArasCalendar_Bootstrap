using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasMVC01.Controllers
{
    public class CalendarPatial
    {
        public string id { set; get; } = "";
        public string title { set; get; } = "";
        public string start { set; get; } = "";
        public string end { set; get; } = "";
        public bool allDay { set; get; } = false;
        public string className { set; get; } = "info";
        public string description { set; get; } = "";

    }
}