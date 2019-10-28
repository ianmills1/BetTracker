using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Data
{
    public enum BetType
        {
            Spread = 1,

            Moneyline,

            OverUnder,

            SeasonWinTotal,

            MakePlayoffs,

            MissPlayoffs,

            WinTitle,

            TotalTouchdowns,

            TotalYards,

            TotalCompletions,

            TotalCarries,

            TotalReceptions,

            TotalPoints,

            [Description("Total Rebounds")]
            TotalRebounds,

            [Description("Total Assists")]
            TotalAssists,

            [Description("Total Hits")]
            TotalHits,

            [Description("Total Home Runs")]
            TotalHomeRuns,

            [Description("Total RBIs")]
            TotalRBIs,

            [Description("Total Strikeouts")]
            TotalStrikeouts,

            [Description("Total Goals")]
            TotalGoals,

            [Description("Total Saves")]
            TotalSaves,

            [Description("Other")]
            Other
        }

    public class BetInfo
    {
        [Key]
        [Display(Name = "Bet ID")]
        public int BetId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public int Odds { get; set; }

        [Required]
        [Display(Name = "Amount Bet")]
        public decimal AmountBet { get; set; }

        [Required]
        [Display(Name = "To Win")]
        public decimal ToWin { get; set; }
        
        [Required]
        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [ForeignKey("Bet")]
        public int BaseBetId { get; set; }

        public virtual BaseBet Bet { get; set; }
    }
}