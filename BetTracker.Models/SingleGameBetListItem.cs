using BetTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class SingleGameBetListItem : BaseBetListItem
    {
        [Display(Name = "Game ID")]
        public int GameId { get; set; }

        [Display(Name = "Home Team")]
        public string HomeTeam { get; set; }

        [Display(Name = "Away Team")]
        public string AwayTeam { get; set; }

        [Display(Name = "Your Bet")]
        public string GamePick { get; set; }
    }
}
