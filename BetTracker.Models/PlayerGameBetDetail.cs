using BetTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class PlayerGameBetDetail
    {
        [Display(Name = "Player Bet ID")]
        public int PlayerId { get; set; }

        [Required]
        public SportType Sport { get; set; }

        [Required]
        public LeagueType League { get; set; }

        [Display(Name = "Player")]
        public string PlayerName { get; set; }

        [Display(Name = "Player Team")]
        public string PlayerTeam { get; set; }

        [Display(Name = "Your Bet")]
        public string PlayerPick { get; set; }

        public int Odds { get; set; }

        [Display(Name = "Amount Bet")]
        public decimal AmountBet { get; set; }

        [Display(Name = "To Win")]
        public decimal ToWin { get; set; }

        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
