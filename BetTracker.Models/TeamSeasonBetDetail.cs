using BetTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class TeamSeasonBetDetail
    {
        [Display(Name = "Season Bet ID")]
        public int TeamId { get; set; }

        [Required]
        public SportType Sport { get; set; }

        [Required]
        public LeagueType League { get; set; }

        public string Team { get; set; }

        [Display(Name = "Your Bet")]
        public string TeamPick { get; set; }

        public int Odds { get; set; }

        [Display(Name = "Amount Bet")]
        public decimal AmountBet { get; set; }

        [Display(Name = "To Win")]
        public decimal ToWin { get; set; }

        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
