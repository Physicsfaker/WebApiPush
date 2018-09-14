using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiPush.Models;

namespace WebApiPush.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult PushMessage(string token, string user, string message)
        {
            //var parameters = new NameValueCollection {
            // { "token", "aruz1ejkrwyxb19jz6wi93kgtu1b2j" },
            // { "user", "uz839pedyew2bif1p8qimhzm8ybyx9" },
            // { "message", "I win!" }
            //};


            if (String.IsNullOrEmpty(token) || String.IsNullOrEmpty(user) || String.IsNullOrEmpty(message))
                throw new NullReferenceException(string.Format("one of the lines is empty"));

            var parameters = new NameValueCollection {
             { "token", token },
             { "user", user },
             { "message", message }
            };

            using (var client = new WebClient())
            {
                client.UploadValues("https://api.pushover.net/1/messages.json", parameters);
            }
            return View("Index");
        }
    }
}
