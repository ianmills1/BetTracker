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
                        .Single(e => e.TeamId == id && e.OwnerId == _userId);
                return
                    new TeamSeasonBetDetail
                    {
                        TeamId = entity.TeamId,
                        Team = entity.Team,
                        TeamPick = entity.TeamPick,
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
                        .Single(e => e.TeamId == model.TeamId && e.OwnerId == _userId);

                entity.Sport = model.Sport;
                entity.League = model.League;
                entity.Team = model.Team;
                entity.TeamPick = model.TeamPick;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTeamSeasonBet(int teamId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TeamSeasonBets
                        .Single(e => e.TeamId == teamId && e.OwnerId == _userId);

                ctx.TeamSeasonBets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}