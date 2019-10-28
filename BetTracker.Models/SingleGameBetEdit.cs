using BetTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class SingleGameBetEdit
    {
        [Display(Name = "Game ID")]
        public int GameId { get; set; }

        public SportType Sport { get; set; }

        public LeagueType League { get; set; }

        [Display(Name = "Home Team")]
        public string HomeTeam { get; set; }

        [Display(Name = "Away Team")]
        public string AwayTeam { get; set; }

        [Display(Name = "Your Bet")]
        public string GamePick { get; set; }

        public int Odds { get; set; }

        [Display(Name = "Amount Bet")]
        public decimal AmountBet { get; set; }
    }
}
