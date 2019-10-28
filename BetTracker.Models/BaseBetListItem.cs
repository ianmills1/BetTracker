using BetTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class BaseBetListItem
    {
        [Display(Name = "Bet ID")]
        public int BaseId { get; set; }

        public SportType Sport { get; set; }

        public LeagueType League { get; set; }

        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
