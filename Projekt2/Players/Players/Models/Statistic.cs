using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Players.Models
{
    public class Statistic
    {
        public int StatisticID { get; set; }
        public int PlayerID { get; set; }
        public int MatchID { get; set; }
        [Range(0, 20)]
        public int goals { get; set; }
        [Range(0, 20)]
        public int assists { get; set; }
        [Range(0, 120)]
        public int minutes { get; set; }

        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }
    }
}