using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Data
{
    public class TeamSeasonBet : BaseBet
    {
        [Required]
        [Display(Name = "Season Bet ID")]
        public int TeamId { get; set; }

        [Required]
        public string Team { get; set; }

        [Required]
        [Display(Name = "Your Bet")]
        public string TeamPick { get; set; }
    }
}