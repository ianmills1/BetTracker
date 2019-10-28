using BetTracker.Data;
using BetTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Services
{
    public class SingleGameBetService
    {
        private readonly Guid _userId;

        public SingleGameBetService(Guid userId)
        {
            _userId = userId;
        }

        public int CreateSingleGameBet(SingleGameBetCreate model)
        {
            var entity =
                new SingleGameBet()
                {
                    OwnerId = _userId,
                    Sport = model.Sport,
                    League = model.League,
                    HomeTeam = model.HomeTeam,
                    AwayTeam = model.AwayTeam,
                    GamePick = model.GamePick,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.SingleGameBets.Add(entity);

                if (ctx.SaveChanges() == 1)
                {
                    return entity.BaseId;
                }
                else return 0;
            }
        }

        public IEnumerable<SingleGameBetListItem> GetSingleGameBets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .SingleGameBets
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new SingleGameBetListItem
                                {
                                    Sport = e.Sport,
                                    League = e.League,
                                    HomeTeam = e.HomeTeam,
                                    AwayTeam = e.AwayTeam,
                                    GamePick = e.GamePick,
                                    CreatedUtc = DateTimeOffset.Now
                                }
                        );

                return query.ToArray();
            }
        }

        public SingleGameBetDetail GetSingleGameBetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SingleGameBets
                        .Single(e => e.GameId == id && e.OwnerId == _userId);
                return
                    new SingleGameBetDetail
                    {
                        GameId = entity.GameId,
                        HomeTeam = entity.HomeTeam,
                        AwayTeam = entity.AwayTeam,
                        GamePick = entity.GamePick
                    };
            }
        }

        public bool UpdateSingleGameBet(SingleGameBetEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SingleGameBets
                        .Single(e => e.GameId == model.GameId && e.OwnerId == _userId);

                entity.Sport = model.Sport;
                entity.League = model.League;
                entity.HomeTeam = model.HomeTeam;
                entity.AwayTeam = model.AwayTeam;
                entity.GamePick = model.GamePick;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSingleGameBet(int gameId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SingleGameBets
                        .Single(e => e.GameId == gameId && e.OwnerId == _userId);

                ctx.SingleGameBets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}