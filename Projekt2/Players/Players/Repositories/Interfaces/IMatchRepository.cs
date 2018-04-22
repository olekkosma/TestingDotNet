using Players.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players.Repositories.Interfaces
{
    public interface IMatchRepository :IDisposable
    {
        IEnumerable<Match> GetAllMatches();
        Match FindById(int? id);
        void Add(Match item);
        void Delete(Match item);
        void Update(Match item);
        void Save();

        
    }
}
