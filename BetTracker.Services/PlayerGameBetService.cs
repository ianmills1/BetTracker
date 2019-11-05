using BetTracker.Data;
using BetTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Services
{
    public class PlayerGameBetService
    {
        private readonly Guid _userId;

        public PlayerGameBetService(Guid userId)
        {
            _userId = userId;
        }

        public int CreatePlayerGameBet(PlayerGameBetCreate model)
        {
            var entity =
                new PlayerGameBet()
                {
                    OwnerId = _userId,
                    Sport = model.Sport,
                    League = model.League,
                    PlayerName = model.PlayerName,
                    PlayerTeam = model.PlayerTeam,
                    PlayerPick = model.PlayerPick,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.PlayerGameBets.Add(entity);

                if (ctx.SaveChanges() == 1)
                {
                    return entity.BaseId;
                }
                else return 0;
            }
        }

        public IEnumerable<PlayerGameBetListItem> GetPlayerGameBets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .PlayerGameBets
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new PlayerGameBetListItem
                                {
                                    BaseId = e.BaseId,
                                    Sport = e.Sport,
                                    League = e.League,
                                    PlayerName = e.PlayerName,
                                    PlayerTeam = e.PlayerTeam,
                                    PlayerPick = e.PlayerPick,
                                    CreatedUtc = DateTimeOffset.Now
                                }
                        );

                return query.ToArray();
            }
        }

        public BetDetail GetPlayerGameBetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var something = ctx.BaseBets.Where(e => e.BaseId == id).ToList();
                if (something[0].GetType() == typeof(PlayerGameBet))
                {
                    var entity = something.OfType<PlayerGameBet>().Single();
                    var entityBetInfo =
                    ctx
                        .BetsInfo
                        .Single(e => e.BaseBetId == id);

                    return
                        new PlayerGameBetDetail
                        {
                            BaseId = entity.BaseId,
                            PlayerName = entity.PlayerName,
                            PlayerTeam = entity.PlayerTeam,
                            PlayerPick = entity.PlayerPick,
                            Odds = entityBetInfo.Odds,
                            AmountBet = entityBetInfo.AmountBet,
                            ToWin = entityBetInfo.ToWin,
                            CreatedUtc = entity.CreatedUtc
                        };
                }
                else if (something[0].GetType() == typeof(TeamSeasonBet))
                {
                    var entity = something.OfType<TeamSeasonBet>().Single();
                    var entityBetInfo =
                    ctx
                        .BetsInfo
                        .Single(e => e.BaseBetId == id);

                    return
                        new TeamSeasonBetDetail
                        {
                            BaseId = entity.BaseId,
                            Team = entity.Team,
                            TeamPick = entity.TeamPick,
                            Odds = entityBetInfo.Odds,
                            AmountBet = entityBetInfo.AmountBet,
                            ToWin = entityBetInfo.ToWin
                        };
                }
                else if (something[0].GetType() == typeof(SingleGameBet))
                {
                    var entity = something.OfType<SingleGameBet>().Single();
                    var entityBetInfo =
                    ctx
                        .BetsInfo
                        .Single(e => e.BaseBetId == id);

                    return
                        new SingleGameBetDetail
                        {
                            BaseId = entity.BaseId,
                            HomeTeam = entity.HomeTeam,
                            AwayTeam = entity.AwayTeam,
                            GamePick = entity.GamePick,
                            Odds = entityBetInfo.Odds,
                            AmountBet = entityBetInfo.AmountBet,
                            ToWin = entityBetInfo.ToWin
                        };
                }
                else
                {
                    return
                        null;
                }
            }
        }

        public bool UpdatePlayerGameBet(PlayerGameBetEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PlayerGameBets
                        .Single(e => e.BaseId == model.BaseId && e.OwnerId == _userId);

                entity.Sport = model.Sport;
                entity.League = model.League;
                entity.PlayerName = model.PlayerName;
                entity.PlayerTeam = model.PlayerTeam;
                entity.PlayerPick = model.PlayerPick;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePlayerGameBet(int baseId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PlayerGameBets
                        .Single(e => e.BaseId == baseId && e.OwnerId == _userId);

                ctx.PlayerGameBets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
