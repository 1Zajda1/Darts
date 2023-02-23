using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;
using Sdc.Data;
using Sdc.Model;
using System.Text.RegularExpressions;
using Group = Sdc.Model.Group;

namespace Sdc.Services
{
    public class MatchService : IMatchService
    {
        private SdcContext db;
        public MatchService(SdcContext context)
        {
            db = context;
        }

        public bool AddTournament(Tournament tournament)
        {
            TUR_Tournament dbTournament = new TUR_Tournament
            {
                Name = tournament.Name,
                NumberOfPlayers = tournament.NumberOfPlayers,
                NumberOfGroups = tournament.NumberOfGroups,
                PlayerIsPlayoff = tournament.PlayerIsPlayoff,
                FullScore = tournament.FullScore,
                NumberOfLegs = tournament.NumberOfLegs,
                NumberOfSets = tournament.NumberOfSets,
            };

            db.TUR_Tournaments.Add(dbTournament);

            int groupCount = tournament.Players.Count / tournament.NumberOfGroups;
            Random random = new Random();

            // Shuffle the players array
            tournament.Players = tournament.Players.OrderBy(x => random.Next()).ToList();

            List<Group> groups = new List<Group>();
            // Assign players to groups
            for (int i = 0; i < tournament.NumberOfGroups; i++)
            {
                Group group = new Group() { GroupName = i };
                for (int j = 0; j < groupCount; j++)
                {
                    group.Players.Add(tournament.Players.ElementAt(i * groupCount + j));
                }
                groups.Add(group);
            }

            foreach (var groupItem in groups)
            {
                Tournament turnaj = TournamentGenerator.GenerateTournament(groupItem.Players, tournament.Name);

                foreach (var item in turnaj.Matches)
                {
                    dbTournament.MAT_Matches.Add(new MAT_Match
                    {
                        Player1Id = item.Player1Id,
                        Player2Id = item.Player2Id,
                        FullScore = dbTournament.FullScore,
                        NumberOfLegs = dbTournament.NumberOfLegs,
                        NumberOfSets = dbTournament.NumberOfSets,
                        Tournament = dbTournament,
                        Group = groupItem.GroupName
                    });
                }

            }
            db.SaveChanges();
            return true;
        }

        public bool ChangeSides(int Id)
        {
            var match = db.MAT_Matchs.Find(Id);
            match.Player1 = null;
            match.Player2 = null;
            var player1Id = match.Player1Id;
            match.Player1Id = match.Player2Id;
            match.Player2Id = player1Id;
            db.SaveChanges();
            return true;
        }

        public bool GeneratePlayoff(Tournament tournament)
        {
            var groups = tournament.Matches.Select(x => x.Group).Distinct().ToList();
            if (tournament.PlayerIsPlayoff == 0)
                return true;
            if (tournament.PlayerIsPlayoff == 2 && tournament.NumberOfGroups == 2)
            {
                int player1Id = 0;
                int player2Id = 0;
                foreach (var group in groups)
                {
                    List<TeamResult> list = TournamentGenerator.GetTournamentResults(group, tournament);
                    if (group == 0)
                        player1Id = list.OrderBy(y => y.Wins).Select(y => y.Team).FirstOrDefault();
                    if (group == 1)
                        player2Id = list.OrderBy(y => y.Wins).Select(y => y.Team).FirstOrDefault();
                }

                SaveMatch(new MatchFull
                {
                    DateTimeCreated = DateTime.Now,
                    FullScore = tournament.FullScore,
                    NumberOfLegs = tournament.NumberOfLegs + 1,
                    NumberOfSets = tournament.NumberOfSets,
                    Player1Id = player1Id,
                    Player2Id = player2Id,
                    PositionInPlayoff = 1,
                    TournamentId = tournament.Id
                });
            }

            if (tournament.PlayerIsPlayoff == 4 && tournament.NumberOfGroups == 2)
            {
                int Aplayer1Id = 0;
                int Aplayer2Id = 0;
                int Bplayer1Id = 0;
                int Bplayer2Id = 0;
                foreach (var group in groups)
                {
                    List<TeamResult> list = TournamentGenerator.GetTournamentResults(group, tournament);
                    if (group == 0)
                        Aplayer1Id = list.OrderBy(y => y.Wins).Select(y => y.Team).FirstOrDefault();
                    if (group == 0)
                        Aplayer2Id = list.OrderBy(y => y.Wins).Select(y => y.Team).ElementAt(1);

                    if (group == 1)
                        Bplayer1Id = list.OrderBy(y => y.Wins).Select(y => y.Team).FirstOrDefault();
                    if (group == 1)
                        Bplayer2Id = list.OrderBy(y => y.Wins).Select(y => y.Team).ElementAt(1);
                }

                SaveMatch(new MatchFull
                {
                    DateTimeCreated = DateTime.Now,
                    FullScore = tournament.FullScore,
                    NumberOfLegs = tournament.NumberOfLegs + 1,
                    NumberOfSets = tournament.NumberOfSets,
                    Player1Id = Aplayer1Id,
                    Player2Id = Bplayer2Id,
                    PositionInPlayoff = 2,
                    TournamentId = tournament.Id
                });

                SaveMatch(new MatchFull
                {
                    DateTimeCreated = DateTime.Now,
                    FullScore = tournament.FullScore,
                    NumberOfLegs = tournament.NumberOfLegs + 1,
                    NumberOfSets = tournament.NumberOfSets,
                    Player1Id = Bplayer1Id,
                    Player2Id = Aplayer2Id,
                    PositionInPlayoff = 3,
                    TournamentId = tournament.Id
                });
            }

            if (tournament.PlayerIsPlayoff == 8 && tournament.NumberOfGroups == 2)
            {
                int Aplayer1Id = 0;
                int Aplayer2Id = 0;
                int Aplayer3Id = 0;
                int Aplayer4Id = 0;
                int Bplayer1Id = 0;
                int Bplayer2Id = 0;
                int Bplayer3Id = 0;
                int Bplayer4Id = 0;
                foreach (var group in groups)
                {
                    List<TeamResult> list = TournamentGenerator.GetTournamentResults(group, tournament);
                    if (group == 0)
                    {
                        Aplayer1Id = list.OrderBy(y => y.Wins).Select(y => y.Team).FirstOrDefault();
                        Aplayer2Id = list.OrderBy(y => y.Wins).Select(y => y.Team).ElementAt(1);
                        Aplayer3Id = list.OrderBy(y => y.Wins).Select(y => y.Team).ElementAt(2);
                        Aplayer4Id = list.OrderBy(y => y.Wins).Select(y => y.Team).ElementAt(3);
                    }

                    if (group == 1) { 
                        Bplayer1Id = list.OrderBy(y => y.Wins).Select(y => y.Team).FirstOrDefault();
                        Bplayer2Id = list.OrderBy(y => y.Wins).Select(y => y.Team).ElementAt(1);
                        Bplayer3Id = list.OrderBy(y => y.Wins).Select(y => y.Team).ElementAt(2);
                        Bplayer4Id = list.OrderBy(y => y.Wins).Select(y => y.Team).ElementAt(3);
                    }
                }

                SaveMatch(new MatchFull
                {
                    DateTimeCreated = DateTime.Now,
                    FullScore = tournament.FullScore,
                    NumberOfLegs = tournament.NumberOfLegs + 1,
                    NumberOfSets = tournament.NumberOfSets,
                    Player1Id = Aplayer1Id,
                    Player2Id = Bplayer4Id,
                    PositionInPlayoff = 4,
                    TournamentId = tournament.Id
                });

                SaveMatch(new MatchFull
                {
                    DateTimeCreated = DateTime.Now,
                    FullScore = tournament.FullScore,
                    NumberOfLegs = tournament.NumberOfLegs + 1,
                    NumberOfSets = tournament.NumberOfSets,
                    Player1Id = Bplayer1Id,
                    Player2Id = Aplayer4Id,
                    PositionInPlayoff = 5,
                    TournamentId = tournament.Id
                });


                SaveMatch(new MatchFull
                {
                    DateTimeCreated = DateTime.Now,
                    FullScore = tournament.FullScore,
                    NumberOfLegs = tournament.NumberOfLegs + 1,
                    NumberOfSets = tournament.NumberOfSets,
                    Player1Id = Bplayer2Id,
                    Player2Id = Aplayer3Id,
                    PositionInPlayoff = 6,
                    TournamentId = tournament.Id
                });

                SaveMatch(new MatchFull
                {
                    DateTimeCreated = DateTime.Now,
                    FullScore = tournament.FullScore,
                    NumberOfLegs = tournament.NumberOfLegs + 1,
                    NumberOfSets = tournament.NumberOfSets,
                    Player1Id = Aplayer2Id,
                    Player2Id = Bplayer3Id,
                    PositionInPlayoff = 7,
                    TournamentId = tournament.Id
                });

                TUR_Tournament turnaj = db.TUR_Tournaments.FirstOrDefault(x => x.Id == tournament.Id);
                turnaj.HasPlayoff = true;
            }
            return true;
        }

        public List<MatchFull> GetMatches()
        {
            return db.MAT_Matchs.OrderByDescending(x => x.DateTimeCreated).Select(x => new MatchFull
            {
                Id = x.Id,
                DateTimeCreated = x.DateTimeCreated,
                DateTimeDone = x.DateTimeDone,
                DateTimePlaned = x.DateTimePlaned,
                DateTimePlayed = x.DateTimePlayed,
                Player1Id = x.Player1Id,
                Player2Id = x.Player2Id,
                Player1Name = x.Player1.UserName,
                Player2Name = x.Player2.UserName,
                Player1Score = x.Player1Score,
                Player2Score = x.Player2Score,
                Player1ScoreSets = x.MAT_Sets.Sum(y => y.Player1Score),
                Player2ScoreSets = x.MAT_Sets.Sum(y => y.Player2Score),
                Player1ScoreLegs = x.MAT_Sets.SelectMany(y => y.Legs).Sum(y => y.Player1Score),
                Player2ScoreLegs = x.MAT_Sets.SelectMany(y => y.Legs).Sum(y => y.Player2Score),
                PositionInPlayoff = x.PositionInPlayoff,
                TournamentId = x.TournamentId,
                FullScore = x.FullScore,
                Group = x.Group,
                NumberOfLegs = x.NumberOfLegs,
                NumberOfSets = x.NumberOfSets
            }).ToList();
        }

        public MatchFull GetMatchFull(int Id)
        {
            return db.MAT_Matchs.Where(x => x.Id == Id).Select(x => new MatchFull
            {
                Id = x.Id,
                DateTimeCreated = x.DateTimeCreated,
                DateTimeDone = x.DateTimeDone,
                DateTimePlaned = x.DateTimePlaned,
                DateTimePlayed = x.DateTimePlayed,
                Player1Id = x.Player1Id,
                Player2Id = x.Player2Id,
                FullScore = x.FullScore,
                NumberOfLegs = x.NumberOfLegs,
                NumberOfSets = x.NumberOfSets,
                Player1Name = x.Player1.UserName,
                Player2Name = x.Player2.UserName,
                PositionInPlayoff = x.PositionInPlayoff,
                TournamentId = x.TournamentId,
                Group = x.Group,
                
            }).FirstOrDefault();

        }

        public MatchFull GetDetailMatch(int Id)
        {
            return db.MAT_Matchs.Where(x => x.Id == Id).Select(x => new MatchFull
            {
                Id = x.Id,
                DateTimeCreated = x.DateTimeCreated,
                DateTimeDone = x.DateTimeDone,
                DateTimePlaned = x.DateTimePlaned,
                DateTimePlayed = x.DateTimePlayed,
                Player1Id = x.Player1Id,
                Player1Score = x.Player1Score,
                Player2Score = x.Player2Score,
                Player2Id = x.Player2Id,
                FullScore = x.FullScore,
                NumberOfLegs = x.NumberOfLegs,
                NumberOfSets = x.NumberOfSets,
                Player1Name = x.Player1.UserName,
                Player2Name = x.Player2.UserName,
                PositionInPlayoff = x.PositionInPlayoff,
                TournamentId = x.TournamentId,
                Group = x.Group,
                Sets = x.MAT_Sets.Select(s => new MatchSet
                {
                    IsDone = true,
                    Player1Score = s.Player1Score,
                    Player2Score = s.Player2Score,
                    MatchLegs = s.Legs.Select(l => new MatchLeg
                    {
                        IsDone = true,
                        Player1Score = l.Player1Score,
                        Player2Score = l.Player2Score,
                        MatchRounds = l.Rounds.Select(r => new MatchRound
                        {
                            PlayerId= r.PlayerId,
                            RoundCount= r.RoundCount,
                            Shot1 = r.Shots.Where(y => y.ShotCount == 1).Select(y => new MatchShot
                            {
                                IsClosingDart = y.Shot.IsClosingDart,
                                IsDouble = y.Shot.IsDouble,
                                IsForClosing= y.Shot.IsForClosing,
                                IsFilled = y.Shot.IsFilled,
                                Number = y.Shot.Number,
                                IsTriple = y.Shot.IsTriple,
                                IsOvershot = y.Shot.IsOvershot,
                                IsSkipped = y.Shot.IsSkipped
                            }).FirstOrDefault(),
                            Shot2 = r.Shots.Where(y => y.ShotCount == 2).Select(y => new MatchShot
                            {
                                IsClosingDart = y.Shot.IsClosingDart,
                                IsDouble = y.Shot.IsDouble,
                                IsForClosing = y.Shot.IsForClosing,
                                IsFilled = y.Shot.IsFilled,
                                Number = y.Shot.Number,
                                IsTriple = y.Shot.IsTriple,
                                IsOvershot = y.Shot.IsOvershot,
                                IsSkipped = y.Shot.IsSkipped
                            }).FirstOrDefault(),
                            Shot3 = r.Shots.Where(y => y.ShotCount == 3).Select(y => new MatchShot
                            {
                                IsClosingDart = y.Shot.IsClosingDart,
                                IsDouble = y.Shot.IsDouble,
                                IsForClosing = y.Shot.IsForClosing,
                                IsFilled = y.Shot.IsFilled,
                                Number = y.Shot.Number,
                                IsTriple = y.Shot.IsTriple,
                                IsOvershot = y.Shot.IsOvershot,
                                IsSkipped = y.Shot.IsSkipped
                            }).FirstOrDefault(),
                            
                        }).ToList()
                    }).ToList()
                }).ToList()
            }).FirstOrDefault();
           
        }

        public Tournament GetTournament(int Id)
        {
            Tournament tournament = new Tournament();
            var dbTurnaj = db.TUR_Tournaments.Where(x => x.Id == Id).Include(x => x.MAT_Matches).ThenInclude(x => x.Player1).Include(x => x.MAT_Matches).ThenInclude(x => x.Player2).First();
            tournament.NumberOfPlayers = dbTurnaj.NumberOfPlayers;
            tournament.Name = dbTurnaj.Name;
            tournament.HasPlayoff = dbTurnaj.HasPlayoff;
            tournament.NumberOfLegs = dbTurnaj.NumberOfLegs;
            tournament.FullScore = dbTurnaj.FullScore;
            tournament.NumberOfSets = dbTurnaj.NumberOfSets;
            tournament.NumberOfGroups = dbTurnaj.NumberOfGroups;
            tournament.PlayerIsPlayoff = dbTurnaj.PlayerIsPlayoff;
            tournament.Id = dbTurnaj.Id;

            foreach (var item in dbTurnaj.MAT_Matches)
            {
                tournament.Matches.Add(new MatchFull
                {
                    Id = item.Id,
                    DateTimeCreated = item.DateTimeCreated,
                    DateTimeDone = item.DateTimeDone,
                    DateTimePlaned = item.DateTimePlaned,
                    DateTimePlayed = item.DateTimePlayed,
                    Player1Id = item.Player1Id,
                    Player2Id = item.Player2Id,
                    Player1Name = item.Player1.UserName,
                    Player2Name = item.Player2.UserName,
                    Player1Score = item.Player1Score,
                    Player2Score = item.Player2Score,
                    PositionInPlayoff = item.PositionInPlayoff,
                    TournamentId= item.TournamentId,
                    FullScore= item.FullScore,
                    Group = item.Group,
                    NumberOfLegs= item.NumberOfLegs,
                    NumberOfSets = item.NumberOfSets
                });
            }

            foreach (var item in db.PLA_Players)
            {
                tournament.Players.Add(item.Id);
            }

            return tournament;
        }

        public List<Tournament> GetTournaments()
        {
            return db.TUR_Tournaments.Select(x => new Tournament
            {
                NumberOfMatches = x.MAT_Matches.Count,
                Name = x.Name,
                Id = x.Id,
                NumberOfPlayers = x.NumberOfPlayers
            }).ToList();
        }

        public bool SaveCompleteMatch(MatchFull MatchFull)
        {
            if (MatchFull == null)
                return false;


            var match = db.MAT_Matchs.Find(MatchFull.Id);
            match.Player1Score = MatchFull.Player1Score;
            match.Player2Score = MatchFull.Player2Score;
            match.DateTimeDone = DateTime.Now;
            foreach (var set in MatchFull.Sets)
            {
                MAT_Set dbSet = new MAT_Set
                {
                    MatchId = match.Id,
                    Player1Score = set.Player1Score,
                    Player2Score = set.Player2Score,
                };
                db.MAT_Sets.Add(dbSet);
                foreach (var leg in set.MatchLegs)
                {
                    MAT_Leg dbLeg = new MAT_Leg
                    {
                        MAT_Set = dbSet,
                        Player1Score = leg.Player1Score,
                        Player2Score = leg.Player2Score,
                    };
                    db.MAT_Legs.Add(dbLeg);

                    #region Rounds

                    foreach (var round in leg.MatchRounds)
                    {
                        MAT_Round dbRound = new MAT_Round
                        {
                            Leg = dbLeg,
                            PlayerId = round.PlayerId,
                            RoundCount = round.RoundCount
                        };
                        db.MAT_Rounds.Add(dbRound);

                        MAT_Shot shot1 = new MAT_Shot();
                        db.Entry(shot1).CurrentValues.SetValues(round.Shot1);
                        db.MAT_Shots.Add(shot1);
                        db.MAT_Shot_x_Round.Add(new MAT_Shot_x_Round
                        {
                            ShotCount = 1,
                            Shot = shot1,
                            Round = dbRound
                        });

                        MAT_Shot shot2 = new MAT_Shot();
                        db.Entry(shot2).CurrentValues.SetValues(round.Shot2);
                        db.MAT_Shots.Add(shot2);
                        db.MAT_Shot_x_Round.Add(new MAT_Shot_x_Round
                        {
                            ShotCount = 2,
                            Shot = shot2,
                            Round = dbRound
                        });

                        MAT_Shot shot3 = new MAT_Shot();
                        db.Entry(shot3).CurrentValues.SetValues(round.Shot3);
                        db.MAT_Shots.Add(shot3);
                        db.MAT_Shot_x_Round.Add(new MAT_Shot_x_Round
                        {
                            ShotCount = 3,
                            Shot = shot3,
                            Round = dbRound
                        });

                    }
                    #endregion
                }


            }



            PLA_Player player1 = db.PLA_Players.Find(match.Player1Id);
            PLA_Player player2 = db.PLA_Players.Find(match.Player2Id);

            Tuple<int, int> result = DartHelper.EloRating(player1.Rank, player2.Rank, match.Player1Score > match.Player2Score ? 1 : 0, match.Player2Score > match.Player1Score ? 1 : 0);

            PLA_EloHistory eloHistory = new PLA_EloHistory
            {
                DateTimeChanged = DateTime.Now,
                PlayerId = player1.Id,
                RankOld = player1.Rank,
                RankNew = result.Item1
            };

            PLA_EloHistory eloHistory2 = new PLA_EloHistory
            {
                DateTimeChanged = DateTime.Now,
                PlayerId = player2.Id,
                RankOld = player2.Rank,
                RankNew = result.Item2
            };
            player1.Rank = eloHistory.RankNew;
            player2.Rank = eloHistory2.RankNew;

            db.PLA_EloHistories.Add(eloHistory);
            db.PLA_EloHistories.Add(eloHistory2);
            db.SaveChanges();
            return true;
        }

        public bool SaveMatch(MatchFull MatchFull)
        {
            MAT_Match match = new MAT_Match();
            db.Entry(match).CurrentValues.SetValues(MatchFull);
            if (MatchFull.Id < 1)
                db.MAT_Matchs.Add(match);
            db.SaveChanges();
            return true;
        }

        public bool DeleteMatch(int Id)
        {
            db.MAT_Matchs.Remove(db.MAT_Matchs.Find(Id));
            db.SaveChanges();
            return true;
        }
    }
}
