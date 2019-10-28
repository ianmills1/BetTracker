using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Data
{
    public class SingleGameBet : BaseBet
    {
        [Required]
        [Display(Name = "Game Bet ID")]
        public int GameId { get; set; }

        [Required]
        [Display(Name = "Home Team")]
        public string HomeTeam { get; set; }

        [Required]
        [Display(Name = "Away Team")]
        public string AwayTeam { get; set; }

        [Required]
        [Display(Name = "Your Bet")]
        public string GamePick { get; set; }
    }
}