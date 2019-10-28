using BetTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class PlayerGameBetCreate
    {
        [Required]
        public SportType Sport { get; set; }

        [Required]
        public LeagueType League { get; set; }

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
