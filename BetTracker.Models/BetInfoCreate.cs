using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Models
{
    public class BetInfoCreate
    {
        [Required]
        public int Odds { get; set; }

        [Required]
        [Display(Name = "Amount Bet")]
        public decimal AmountBet { get; set; }

        public int BaseBetID { get; set; }
    }
}
