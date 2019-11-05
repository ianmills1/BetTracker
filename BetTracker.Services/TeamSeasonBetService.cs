using BetTracker.Data;
using BetTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Services
{
    public class TeamSeasonBetService
    {
        private readonly Guid _userId;

        public TeamSeasonBetService(Guid userId)
        {
            _userId = userId;
        }

        public int CreateTeamSeasonBet(TeamSeasonBetCreate model)
        {
            var entity =
                new TeamSeasonBet()
                {
                    OwnerId = _userId,
                    Sport = model.Sport,
                    League = model.League,
                    Team = model.Team,
                    TeamPick = model.TeamPick,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.TeamSeasonBets.Add(entity);

                if (ctx.SaveChanges() == 1)
                {
                    return entity.BaseId;
                }
                else return 0;
            }
        }

        public IEnumerable<TeamSeasonBetListItem> GetTeamSeasonBets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .TeamSeasonBets
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new TeamSeasonBetListItem
                                {
                                    BaseId = e.BaseId,
                                    Sport = e.Sport,
                                    League = e.League,
                                    Team = e.Team,
                                    TeamPick = e.TeamPick,
                                    CreatedUtc = DateTimeOffset.Now
                                }
                        );

                return query.ToArray();
            }
        }

        public TeamSeasonBetDetail GetTeamSeasonBetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TeamSeasonBets
                        .Single(e => e.BaseId == id && e.OwnerId == _userId);
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
                        ToWin = entityBetInfo.ToWin,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        public bool UpdateTeamSeasonBet(TeamSeasonBetEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TeamSeasonBets
                        .Single(e => e.BaseId == model.BaseId && e.OwnerId == _userId);

                entity.Sport = model.Sport;
                entity.League = model.League;
                entity.Team = model.Team;
                entity.TeamPick = model.TeamPick;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTeamSeasonBet(int baseId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TeamSeasonBets
                        .Single(e => e.BaseId == baseId && e.OwnerId == _userId);

                ctx.TeamSeasonBets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}