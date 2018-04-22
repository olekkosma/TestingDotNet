using Players.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players.Repositories.Interfaces
{
    public interface IStatisticRepository :IDisposable
    {
        IEnumerable<Statistic> GetAllStatistics();
        IEnumerable<Player> GetAllPlayers();
        IEnumerable<Match> GetAllMatches();

        IEnumerable<Statistic> GetAllStatisticsWithChildren();
        Statistic FindById(int? id);
        void Add(Statistic item);
        void Delete(Statistic item);
        void Update(Statistic item);
        void Save();

        
    }
}
