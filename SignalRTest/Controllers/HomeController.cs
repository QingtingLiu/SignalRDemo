using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using WebGrease.Css.Extensions;

namespace SignalRTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Test()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.onConnected("111", "222", "333");
            return View();
        }
    }
}