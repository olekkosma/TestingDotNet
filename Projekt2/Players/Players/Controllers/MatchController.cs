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
    public class MatchController : Controller
    {
        private IMatchRepository _matchRepository;
        public MatchController( )
        {
            _matchRepository = new MatchRepository(new PlayerContext());
        }
        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        // GET: Match
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var matches = from s in _matchRepository.GetAllMatches()
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                matches = matches.Where(s => s.City.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "city_desc":
                    matches = matches.OrderByDescending(s => s.City);
                    break;
                case "Date":
                    matches = matches.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    matches = matches.OrderByDescending(s => s.Date);
                    break;
                default:
                    matches = matches.OrderBy(s => s.City);
                    break;
            }

            return View(matches.ToList());
        }

        // GET: Match/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = _matchRepository.FindById(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: Match/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Match/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatchID,City,Date,Result")] Match match)
        {
            if (ModelState.IsValid)
            {
                _matchRepository.Add(match);
                _matchRepository.Save();
                return RedirectToAction("Index");
            }

            return View(match);
        }

        // GET: Match/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = _matchRepository.FindById(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Match/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MatchID,City,Date,Result")] Match match)
        {
            if (ModelState.IsValid)
            {
                _matchRepository.Update(match);
                _matchRepository.Save();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: Match/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = _matchRepository.FindById(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = _matchRepository.FindById(id);
            _matchRepository.Delete(match);
            _matchRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _matchRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
