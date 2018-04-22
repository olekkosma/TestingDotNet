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
    public class PlayerRepository : IPlayerRepository, IDisposable
    {
        private PlayerContext _players;
        public PlayerRepository(PlayerContext players)
        {
            _players = players;
        }
        public void Add(Player item)
        {
            _players.Players.Add(item);
        }

        public void Delete(Player item)
        {
            _players.Players.Remove(item);
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

        public Player FindById(int? id)
        {
            return _players.Players.Find(id);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
           return  _players.Players.ToList();
        }

        public void Save()
        {
            _players.SaveChanges();
        }

        public void Update(Player item)
        {
            _players.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Player> GetAllPlayersWitchChildren()
        {
            return _players.Players
                .Include(s => s.Statistics); 
        }
    }
}