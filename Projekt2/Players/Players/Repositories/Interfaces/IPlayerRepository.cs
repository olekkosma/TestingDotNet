using Players.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players.Repositories.Interfaces
{
    public interface IPlayerRepository :IDisposable
    {
        IEnumerable<Player> GetAllPlayers();
        IEnumerable<Player> GetAllPlayersWitchChildren();
        Player FindById(int? id);
        void Add(Player item);
        void Delete(Player item);
        void Update(Player item);
        void Save();

        
    }
}
