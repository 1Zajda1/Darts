using Microsoft.EntityFrameworkCore;
using Sdc.Data;
using Sdc.Model;

namespace Sdc.Services
{
    public class PlayerService : IPlayerService
    {
        private SdcContext db;
        public PlayerService(SdcContext context) { db = context; }
        public Player GetPlayer(int? Id)
        {
            return db.PLA_Players.Where(x => x.Id == Id).Select(x => new Player
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Password = x.Password,
                Rank = x.Rank,
                Surname = x.Surname,
                UserName = x.UserName,
                IsAdmin = x.IsAdmin
            }).FirstOrDefault();
        }

        public Player GetPlayerByPassword(string username, string password)
        {
            return db.PLA_Players.Where(x => x.UserName == username && x.Password == password).Select(x => new Player
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Password = x.Password,
                Rank = x.Rank,
                Surname = x.Surname,
                UserName = x.UserName,
                IsAdmin = x.IsAdmin
            }).FirstOrDefault();
        }

        public List<Player> GetPlayers()
        {
            return db.PLA_Players.Select(x => new Player
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Password = x.Password,
                Rank = x.Rank,
                Surname = x.Surname,
                UserName = x.UserName,
                CountMatches = x.Player1Matches.Where(x => x.DateTimeDone != null).Count() + x.Player2Matches.Where(x => x.DateTimeDone != null).Count(),
            }).ToList();
        }

        public bool SavePlayer(Player player)
        {
            PLA_Player dbPlayer = new PLA_Player();
            db.Entry(dbPlayer).CurrentValues.SetValues(player);
            if (dbPlayer.Id < 1)
            {
                dbPlayer.Email = "neco";
                dbPlayer.Rank = 1000;
                db.PLA_Players.Add(dbPlayer);
            }
            db.SaveChanges();
            return true;
        }

        public List<StatsModel> GetStats(int? playerId)
        {
            IQueryable<MAT_Round>? closeNumbers = null;
            if (playerId.HasValue)
            {
                closeNumbers = db.MAT_Rounds.Where(y => y.PlayerId == playerId).Include(x => x.Player).Include(x => x.Shots).ThenInclude(x => x.Shot).OrderByDescending(y => y.Shots.Where(x => !x.Shot.IsOvershot).Sum(x => x.Shot.Number)).Take(10);
            } else
            {
                closeNumbers = db.MAT_Rounds.Include(x => x.Player).Include(x => x.Shots).ThenInclude(x => x.Shot).OrderByDescending(y => y.Shots.Where(x => !x.Shot.IsOvershot).Sum(x => x.Shot.Number)).Take(10);
            }
            var toReturn = new List<StatsModel>();
            foreach (var item in closeNumbers)
            {
                toReturn.Add(new StatsModel
                {
                    Id = item.Id,
                    Number = item.Shots.Where(x => !x.Shot.IsOvershot).Sum(x => x.Shot.Number.GetValueOrDefault()),
                    Name = item.Player.UserName
                    //MatchId = item.Leg.MAT_Set.MatchId
                });
            }

            return toReturn;
        }

        public List<StatsModel> GetCloseStats(int? playerId)
        {
            IQueryable<MAT_Round>? closeNumbers = null;
            if (playerId.HasValue)
            {
                closeNumbers = db.MAT_Rounds.Where(y => y.PlayerId == playerId).Include(x => x.Player).Include(x => x.Shots).ThenInclude(x => x.Shot).Where(y => y.Shots.Any(x => x.Shot.IsClosingDart));
            }
            else
            {
                closeNumbers = db.MAT_Rounds.Include(x => x.Player).Include(x => x.Shots).ThenInclude(x => x.Shot).Where(y => y.Shots.Any(x => x.Shot.IsClosingDart));
            }
            var toReturn = new List<StatsModel>();
            foreach (var item in closeNumbers.OrderByDescending(x => x.Shots.Sum(y => y.Shot.Number)).Take(10))
            {
                toReturn.Add(new StatsModel
                {
                    Id = item.Id,
                    Number = item.Shots.Sum(x => x.Shot.Number.GetValueOrDefault()),
                    Name = item.Player.UserName
                    //MatchId = item.Leg.MAT_Set.MatchId
                });
            }

            return toReturn;
        }

        public Tuple<int, int, double?> GetPercent(int? playerId)
        {
            int numberOfClose = db.MAT_Rounds.Where(y => y.PlayerId == playerId).Count(y => y.Shots.Any(x => x.Shot.IsForClosing));
            int closingDarts = db.MAT_Rounds.Where(y => y.PlayerId == playerId).Count(y => y.Shots.Any(x => x.Shot.IsClosingDart));
            double percent = (double)closingDarts / (double)numberOfClose * 100;
            return new Tuple<int, int, double?>(numberOfClose, closingDarts, percent);
        }
    }
}
