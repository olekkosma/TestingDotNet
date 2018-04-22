using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Players.DAL;
using Players.Models;
using Players.Repositories;
using Players.Repositories.Interfaces;

namespace Players.Repositories
{
    public class MatchRepository : IMatchRepository,IDisposable
    {
        private PlayerContext _players;
        public MatchRepository(PlayerContext players)
        {
            _players = players;
        }
        public void Add(Match item)
        {
            _players.Matches.Add(item);
        }

        public void Delete(Match item)
        {
            _players.Matches.Remove(item);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _players.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Match FindById(int? id)
        {
            return _players.Matches.Find(id);
        }

        public IEnumerable<Match> GetAllMatches()
        {
           return  _players.Matches.ToList();
        }

        public void Save()
        {
            _players.SaveChanges();
        }

        public void Update(Match item)
        {
            _players.Entry(item).State = EntityState.Modified;
        }
    }
}