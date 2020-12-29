using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SimpleCRUD.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult ErrorPage(string id)
        {
            Response.TrySkipIisCustomErrors = true; //已經在錯誤頁面，故忽略IIS自訂錯誤
            if (Enum.TryParse(id, out HttpStatusCode code))
            {
                Response.StatusCode = (int)code;
                return View(code);
            }
            Response.StatusCode = 404;
            return View(HttpStatusCode.NotFound);
        }
    }
}
