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

        public PlayerGameBetDetail GetPlayerGameBetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var something = ctx.BaseBets.Where(e => e.BaseId == id).ToList();
                if (something[0].GetType() == typeof(PlayerGameBet))
                {
                    var thing = something.OfType<PlayerGameBet>().Single();
                }


                var entity =
                    ctx
                        .PlayerGameBets
                        .Single(e => e.PlayerId == id && e.OwnerId == _userId);
                return
                    new PlayerGameBetDetail
                    {
                        PlayerId = entity.PlayerId,
                        PlayerName = entity.PlayerName,
                        PlayerTeam = entity.PlayerTeam,
                        PlayerPick = entity.PlayerPick
                    };
            }
        }

        public bool UpdatePlayerGameBet(PlayerGameBetEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PlayerGameBets
                        .Single(e => e.PlayerId == model.PlayerId && e.OwnerId == _userId);

                entity.Sport = model.Sport;
                entity.League = model.League;
                entity.PlayerName = model.PlayerName;
                entity.PlayerTeam = model.PlayerTeam;
                entity.PlayerPick = model.PlayerPick;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePlayerGameBet(int playerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PlayerGameBets
                        .Single(e => e.PlayerId == playerId && e.OwnerId == _userId);

                ctx.PlayerGameBets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
