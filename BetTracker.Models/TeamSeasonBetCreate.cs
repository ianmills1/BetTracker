using BetTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class TeamSeasonBetCreate
    {
        [Required]
        public SportType Sport { get; set; }

        [Required]
        public LeagueType League { get; set; }

        [Required]
        public string Team { get; set; }

        [Required]
        [Display(Name = "Your Bet")]
        public string TeamPick { get; set; }
    }
}
