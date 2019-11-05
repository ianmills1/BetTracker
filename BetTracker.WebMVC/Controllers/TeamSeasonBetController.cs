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
    public class TeamSeasonBetController : Controller
    {
        // GET: TeamSeasonBet
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TeamSeasonBetService(userId);
            var model = service.GetTeamSeasonBets();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamSeasonBetCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTeamSeasonBetService();
            var baseBetID = service.CreateTeamSeasonBet(model);
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
            var svc = CreateTeamSeasonBetService();
            var model = svc.GetTeamSeasonBetById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateTeamSeasonBetService();
            var detail = service.GetTeamSeasonBetById(id);
            var model =
                new TeamSeasonBetEdit
                {
                    BaseId = detail.BaseId,
                    Sport = detail.Sport,
                    League = detail.League,
                    Team = detail.Team,
                    TeamPick = detail.TeamPick,
                    Odds = detail.Odds,
                    AmountBet = detail.AmountBet
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TeamSeasonBetEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BaseId != id)
            {
                ModelState.AddModelError("", "Team ID Mismatch");
                return View(model);
            }

            var service = CreateTeamSeasonBetService();

            if (service.UpdateTeamSeasonBet(model))
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
            var svc = CreateTeamSeasonBetService();
            var model = svc.GetTeamSeasonBetById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBet(int id)
        {
            var service = CreateTeamSeasonBetService();

            service.DeleteTeamSeasonBet(id);

            TempData["SaveResult"] = "Your bet was deleted";

            return RedirectToAction("Index");
        }

        private TeamSeasonBetService CreateTeamSeasonBetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TeamSeasonBetService(userId);
            return service;
        }
    }
}