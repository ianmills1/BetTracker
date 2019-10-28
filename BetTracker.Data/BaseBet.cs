using BetTracker.Data;
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
    public enum SportType
        {
            Football = 1,

            Basketball,

            Baseball,

            Hockey,

            Soccer
        }

    public enum LeagueType
        {
            NFL = 1,

            NCAAFootball,

            NBA,

            NBAGLeague,

            NCAABasketball,

            MLB,

            MinorLeagueBaseball,

            NHL,

            MLS,

            InternationalSoccer
        }

    public abstract class BaseBet
    {
        [Key]
        [Display(Name = "Base Bet ID")]
        public int BaseId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public SportType Sport { get; set; }

        [Required]
        public LeagueType League { get; set; }

        [Required]
        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}