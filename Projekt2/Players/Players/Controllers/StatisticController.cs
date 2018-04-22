using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Players.DAL;
using Players.Models;
using Players.Repositories;
using Players.Repositories.Interfaces;

namespace Players.Controllers
{
    public class StatisticController : Controller
    {
        private IStatisticRepository _statisticRepository;
        public StatisticController()
        {
            _statisticRepository = new StatisticRepository(new PlayerContext());
        }
        public StatisticController(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        // GET: Statistic
        public ActionResult Index()
        {
            var statistics = _statisticRepository.GetAllStatisticsWithChildren();
            return View(statistics.ToList());
        }

        // GET: Statistic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic statistic = _statisticRepository.FindById(id);
            if (statistic == null)
            {
                return HttpNotFound();
            }
            return View(statistic);
        }

        // GET: Statistic/Create
        public ActionResult Create()
        {
            ViewBag.MatchID = new SelectList(_statisticRepository.GetAllMatches(), "MatchID", "City");
            ViewBag.PlayerID = new SelectList(_statisticRepository.GetAllPlayers(), "ID", "LastName");
            return View();
        }

        // POST: Statistic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatisticID,PlayerID,MatchID,goals,assists,minutes")] Statistic statistic)
        {
            if (ModelState.IsValid)
            {
                _statisticRepository.Add(statistic);
                _statisticRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.MatchID = new SelectList(_statisticRepository.GetAllMatches(), "MatchID", "City", statistic.MatchID);
            ViewBag.PlayerID = new SelectList(_statisticRepository.GetAllPlayers(), "ID", "LastName", statistic.PlayerID);
            return View(statistic);
        }

        // GET: Statistic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic statistic = _statisticRepository.FindById(id);
            if (statistic == null)
            {
                return HttpNotFound();
            }
            ViewBag.MatchID = new SelectList(_statisticRepository.GetAllMatches(), "MatchID", "City", statistic.MatchID);
            ViewBag.PlayerID = new SelectList(_statisticRepository.GetAllPlayers(), "ID", "LastName", statistic.PlayerID);
            return View(statistic);
        }

        // POST: Statistic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatisticID,PlayerID,MatchID,goals,assists,minutes")] Statistic statistic)
        {
            if (ModelState.IsValid)
            {
                _statisticRepository.Update(statistic);
                _statisticRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.MatchID = new SelectList(_statisticRepository.GetAllMatches(), "MatchID", "City", statistic.MatchID);
            ViewBag.PlayerID = new SelectList(_statisticRepository.GetAllPlayers(), "ID", "LastName", statistic.PlayerID);
            return View(statistic);
        }

        // GET: Statistic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic statistic = _statisticRepository.FindById(id);
            if (statistic == null)
            {
                return HttpNotFound();
            }
            return View(statistic);
        }

        // POST: Statistic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statistic statistic = _statisticRepository.FindById(id);
            _statisticRepository.Delete(statistic);
            _statisticRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _statisticRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
