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
    public class SingleGameBetController : Controller
    {
        // GET: SingleGameBet
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SingleGameBetService(userId);
            var model = service.GetSingleGameBets();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SingleGameBetCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSingleGameBetService();
            var baseBetID = service.CreateSingleGameBet(model);
            if (baseBetID >= 0)
            {
                TempData["SaveResult"] = "Your bet was created";
                return RedirectToAction("Create", "BetInfo", new { id = baseBetID });
            };



            ModelState.AddModelError("", "Your bet could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateSingleGameBetService();
            var model = svc.GetSingleGameBetById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateSingleGameBetService();
            var detail = service.GetSingleGameBetById(id);
            var model =
                new SingleGameBetEdit
                {
                    BaseId = detail.BaseId,
                    Sport = detail.Sport,
                    League = detail.League,
                    HomeTeam = detail.HomeTeam,
                    AwayTeam = detail.AwayTeam,
                    GamePick = detail.GamePick,
                    Odds = detail.Odds,
                    AmountBet = detail.AmountBet
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SingleGameBetEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BaseId != id)
            {
                ModelState.AddModelError("", "Game ID Mismatch");
                return View(model);
            }

            var service = CreateSingleGameBetService();

            if (service.UpdateSingleGameBet(model))
            {
                TempData["SaveResult"] = "Your bet was updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your bet could not be updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSingleGameBetService();
            var model = svc.GetSingleGameBetById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBet(int id)
        {
            var service = CreateSingleGameBetService();

            service.DeleteSingleGameBet(id);

            TempData["SaveResult"] = "Your bet was deleted";

            return RedirectToAction("Index");
        }

        private SingleGameBetService CreateSingleGameBetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SingleGameBetService(userId);
            return service;
        }
    }
}