using BetTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetTracker.WebMVC.Controllers
{
    [Authorize]
    public class BaseBetController : Controller
    {
        // GET: BaseBet
        public ActionResult Index()
        {
            var model = new BaseBetListItem[0];
            return View(model);
        }
    }
}