using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class BetInfoListItem
    {
        [Display(Name = "Bet ID")]
        public int BetId { get; set; }

        public int Odds { get; set; }

        [Display(Name = "Amount Bet")]
        public decimal AmountBet { get; set; }

        [Display(Name = "To Win")]
        public decimal ToWin { get; set; }

        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
