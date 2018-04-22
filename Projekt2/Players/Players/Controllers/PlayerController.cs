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
using Players.ViewModels;

namespace Players.Controllers
{
    public class PlayerController : Controller
    {
        private IPlayerRepository _playerRepository;
        public PlayerController()
        {
            _playerRepository = new PlayerRepository(new PlayerContext());
        }
        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public IEnumerable<PLayerExtended> Playerss { get; set; }
        // GET: Player
        public ActionResult Index(int? id)
        {
            var viewModel = new PlayerIndexData();


            viewModel.Players = _playerRepository.GetAllPlayersWitchChildren();

            viewModel.PLayerExtendeds = viewModel.Players.Select(player =>
            new PLayerExtended
            {
                Players = player,
                Goals = player.Statistics.Sum(stat => stat.goals),
                Assists = player.Statistics.Sum(stat => stat.assists),
                Minutes = player.Statistics.Sum(stat => stat.minutes)
            })
            .OrderBy(stat => stat.Players.LastName);

            if (id != null)
            {
                ViewBag.ID = id.Value;
                viewModel.Statistics = viewModel.Players.Where(
                    i => i.ID == id.Value).Single().Statistics;
            }

            return View(viewModel);
        }

        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = _playerRepository.FindById(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName")] Player player)
        {
            if (ModelState.IsValid)
            {
                _playerRepository.Add(player);
                _playerRepository.Save();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = _playerRepository.FindById(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName")] Player player)
        {
            if (ModelState.IsValid)
            {
                _playerRepository.Update(player);
                _playerRepository.Save();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = _playerRepository.FindById(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = _playerRepository.FindById(id);
            _playerRepository.Delete(player);
            _playerRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _playerRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
