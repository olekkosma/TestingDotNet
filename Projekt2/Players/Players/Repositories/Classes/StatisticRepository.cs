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
    public class StatisticRepository : IStatisticRepository, IDisposable
    {
        private PlayerContext _players;
        public StatisticRepository(PlayerContext players)
        {
            _players = players;
        }
        public void Add(Statistic item)
        {
            _players.Statistics.Add(item);
        }

        public void Delete(Statistic item)
        {
            _players.Statistics.Remove(item);
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

        public Statistic FindById(int? id)
        {
            return _players.Statistics.Find(id);
        }

        public IEnumerable<Statistic> GetAllStatistics()
        {
           return  _players.Statistics.ToList();
        }

        public void Save()
        {
            _players.SaveChanges();
        }

        public void Update(Statistic item)
        {
            _players.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Statistic> GetAllStatisticsWithChildren()
        {
            return _players.Statistics.Include(s => s.Match).Include(s => s.Player);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _players.Players.ToList();
        }

        public IEnumerable<Match> GetAllMatches()
        {
            return _players.Matches.ToList();
        }
    }
}