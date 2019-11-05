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
            if (detail.GetType() == typeof(PlayerGameBetDetail))
            {
                var some = (PlayerGameBetDetail)detail;
                var model =
                    new PlayerGameBetEdit
                    {
                        BaseId = some.BaseId,
                        Sport = some.Sport,
                        League = some.League,
                        PlayerName = some.PlayerName,
                        PlayerTeam = some.PlayerTeam,
                        PlayerPick = some.PlayerPick,
                        Odds = some.Odds,
                        AmountBet = some.AmountBet,
                    };
                return View(model);
            }
            else if (detail.GetType() == typeof(TeamSeasonBetDetail))
            {
                var some = (TeamSeasonBetDetail)detail;
                var model =
                    new TeamSeasonBetEdit
                    {
                        BaseId = some.BaseId,
                        Sport = some.Sport,
                        League = some.League,
                        Team = some.Team,
                        TeamPick = some.TeamPick,
                        Odds = some.Odds,
                        AmountBet = some.AmountBet
                    };
                return View(model);
            }
            else if (detail.GetType() == typeof(SingleGameBetDetail))
            {
                var some = (SingleGameBetDetail)detail;
                var model =
                    new SingleGameBetEdit
                    {
                        BaseId = some.BaseId,
                        Sport = some.Sport,
                        League = some.League,
                        HomeTeam = some.HomeTeam,
                        AwayTeam = some.AwayTeam,
                        GamePick = some.GamePick,
                        Odds = some.Odds,
                        AmountBet = some.AmountBet
                    };
                return View(model);
            }
            else
            {
                return
                    null;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlayerGameBetEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BaseId != id)
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