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
    public class PlayerGameBetController : Controller
    {
        // GET: PlayerGameBet
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PlayerGameBetService(userId);
            var model = service.GetPlayerGameBets();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerGameBetCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePlayerGameBetService();
            var baseBetID = service.CreatePlayerGameBet(model);
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
            var svc = CreatePlayerGameBetService();
            var model = svc.GetPlayerGameBetById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreatePlayerGameBetService();
            var detail = service.GetPlayerGameBetById(id);
            var model =
                new PlayerGameBetEdit
                {
                    PlayerId = detail.PlayerId,
                    Sport = detail.Sport,
                    League = detail.League,
                    PlayerName = detail.PlayerName,
                    PlayerTeam = detail.PlayerTeam,
                    PlayerPick = detail.PlayerPick,
                    Odds = detail.Odds,
                    AmountBet = detail.AmountBet,
                    ToWin = detail.ToWin
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlayerGameBetEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.PlayerId != id)
            {
                ModelState.AddModelError("", "Player ID Mismatch");
                return View(model);
            }

            var service = CreatePlayerGameBetService();

            if (service.UpdatePlayerGameBet(model))
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
            var svc = CreatePlayerGameBetService();
            var model = svc.GetPlayerGameBetById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBet(int id)
        {
            var service = CreatePlayerGameBetService();

            service.DeletePlayerGameBet(id);

            TempData["SaveResult"] = "Your bet was deleted";

            return RedirectToAction("Index");
        }

        private PlayerGameBetService CreatePlayerGameBetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PlayerGameBetService(userId);
            return service;
        }
    }
}