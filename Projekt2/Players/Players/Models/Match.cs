using Players.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Players.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string City { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [MatchFormat]
        [Display(Name = "Match result")]
        public string Result { get; set; }

        public virtual ICollection<Statistic> Statistics { get; set; }
    }
}