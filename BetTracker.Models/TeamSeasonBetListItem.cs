using BetTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class TeamSeasonBetListItem : BaseBetListItem
    {
        [Display(Name = "Team ID")]
        public int BaseId { get; set; }

        public string Team { get; set; }

        [Display(Name = "Your Bet")]
        public string TeamPick { get; set; }
    }
}
