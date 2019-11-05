using BetTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class PlayerGameBetListItem : BaseBetListItem
    {
        [Display(Name = "Player Bet ID")]
        public int BaseId { get; set; }

        [Display(Name = "Player")]
        public string PlayerName { get; set; }

        [Display(Name = "Player Team")]
        public string PlayerTeam { get; set; }

        [Display(Name = "Your Bet")]
        public string PlayerPick { get; set; }
    }
}
