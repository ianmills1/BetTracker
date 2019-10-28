using BetTracker.Data;
using BetTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetTracker.Services
{
    public class BetInfoService
    {
        private readonly Guid _userId;

        public BetInfoService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBetInfo(BetInfoCreate model)
        {
            var entity =
                new BetInfo()
                {
                    OwnerId = _userId,
                    Odds = model.Odds,
                    AmountBet = model.AmountBet,
                    ToWin = (model.Odds > 0) ? (model.AmountBet * model.Odds * 0.01m) : model.AmountBet / (model.Odds * -0.01m),
                    CreatedUtc = DateTimeOffset.Now,
                    BaseBetId = model.BaseBetID,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.BetsInfo.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BetInfoListItem> GetBetsInfo()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .BetsInfo
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new BetInfoListItem
                                {
                                    Odds = e.Odds,
                                    AmountBet = e.AmountBet,
                                    ToWin = e.ToWin,
                                    CreatedUtc = DateTimeOffset.Now
                                }
                        );

                return query.ToArray();
            }
        }
    }
}
