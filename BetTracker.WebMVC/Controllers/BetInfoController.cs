using BetTracker.Models;
using BetTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetTracker.WebMVC.Controllers
{
    [Authorize]
    public class BetInfoController : Controller
    {
        // GET: BetInfo
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BetInfoService(userId);
            var model = service.GetBetsInfo();

            return View(model);
        }

        //GET
        public ActionResult Create(int id)
        {
            return View(new BetInfoCreate { BaseBetID = id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BetInfoCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBetInfoService();

            if (service.CreateBetInfo(model))
            {
                
                TempData["SaveResult"] = "Your bet was created.";
                return RedirectToAction("Index", "Home");
            };

            ModelState.AddModelError("", "Your bet could not be created.");

            return View(model);
        }

        private BetInfoService CreateBetInfoService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BetInfoService(userId);
            return service;
        }
    }
}