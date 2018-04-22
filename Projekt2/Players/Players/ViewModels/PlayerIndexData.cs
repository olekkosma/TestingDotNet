using Players.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Players.ViewModels
{
    public class PlayerIndexData
    {
        public IEnumerable<PLayerExtended> PLayerExtendeds { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<Statistic> Statistics { get; set; }
       
    }

    public class PLayerExtended
    {
        public Player Players { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Minutes { get; set; }
    }
}