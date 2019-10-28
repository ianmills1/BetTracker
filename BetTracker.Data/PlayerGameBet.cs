using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Data
{
    public class PlayerGameBet : BaseBet
    {
        [Required]
        [Display(Name = "Player Bet ID")]
        public int PlayerId { get; set; }

        [Required]
        [Display(Name = "Player")]
        public string PlayerName { get; set; }

        [Required]
        [Display(Name = "Player Team")]
        public string PlayerTeam { get; set; }

        [Required]
        [Display(Name = "Your Bet")]
        public string PlayerPick { get; set; }
    }
}