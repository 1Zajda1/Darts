using Sdc.Migrations;
using System.Collections.Generic;

namespace Sdc.Model
{
    public static class TournamentGenerator
    {
        public static Tournament GenerateTournament(List<int> players, string tournamentName)
        {
            var tournament = new Tournament
            {
                Name = tournamentName,
                Players = players,
                Matches = new List<MatchFull>()
            };

            // Generate the matches using the teams
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = i + 1; j < players.Count; j++)
                {
                    if (!tournament.Matches.Any(x => x.Player1Id == players[j] && x.Player2Id == players[i]) && !tournament.Matches.Any(x => x.Player1Id == players[i] && x.Player2Id == players[j]))
                    {
                        var match = new MatchFull
                        {
                            Player1Id = players[i],
                            Player2Id = players[j]
                        };

                        tournament.Matches.Add(match);
                    }
                }
            }

            return tournament;
        }

        public static Tournament GenerateTournament2(List<int> players, string tournamentName, int group)
        {
            var tournament = new Tournament
            {
                Name = tournamentName,
                Players = players,
                Matches = new List<MatchFull>()
            };

            if (players.Count % 2 != 0)
            {
                players.Add(0);
            }
            int numTeams = players.Count;
            int numDays = (numTeams - 1);
            int halfSize = numTeams / 2;

            List<int> teams = new List<int>();

            teams.AddRange(players.Skip(halfSize).Take(halfSize));
            teams.AddRange(players.Skip(1).Take(halfSize - 1).ToArray().Reverse());

            int teamsSize = teams.Count;

            for (int day = 0; day < numDays; day++)
            {

                int teamIdx = day % teamsSize;

                tournament.Matches.Add(new MatchFull
                {
                    Player1Id = teams[teamIdx],
                    Player2Id = players[0],
                    DateTimeCreated = DateTime.Now,
                    Group = group
                });

                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;
                    tournament.Matches.Add(new MatchFull
                    {
                        Player1Id = teams[firstTeam],
                        Player2Id = teams[secondTeam],
                        DateTimeCreated = DateTime.Now,
                        Group = group
                    });
                    //Console.WriteLine("{0} vs {1}", teams[firstTeam], teams[secondTeam]);

                }
            }
            return tournament;
        }

        public static List<TeamResult> GenerateTable(List<int> Teams, List<MatchFull> Matches)
        {
            var table = new List<TeamResult>();

            foreach (var team in Teams)
            {
                var result = new TeamResult
                {
                    Team = team,
                    Wins = 0,
                    Losses = 0,
                    Draws = 0
                };

                foreach (var match in Matches)
                {
                    if (match.Player1Id == team)
                    {
                        if (match.Player1Score > match.Player2Score)
                        {
                            result.Wins++;
                        }
                        else if (match.Player1Score < match.Player2Score)
                        {
                            result.Losses++;
                        }
                        else
                        {
                            result.Draws++;
                        }
                    }
                    else if (match.Player2Id == team)
                    {
                        if (match.Player1Score < match.Player2Score)
                        {
                            result.Wins++;
                        }
                        else if (match.Player1Score > match.Player2Score)
                        {
                            result.Losses++;
                        }
                        else
                        {
                            result.Draws++;
                        }
                    }
                }

                table.Add(result);
            }

            return table.OrderByDescending(x => x.Wins).ToList();
        }
        public static List<TeamResult> GetTournamentResults(int group, Tournament tournament)
        {
            List<int> playerIds = tournament.Matches.Where(x => x.Group == group && x.PositionInPlayoff == null).Select(x => x.Player1Id.GetValueOrDefault()).Distinct().ToList();
            playerIds.AddRange(tournament.Matches.Where(x => x.Group == group && x.PositionInPlayoff == null).Select(x => x.Player2Id.GetValueOrDefault()).Distinct().ToList());
            List<int> allplayerIds = playerIds.Distinct().ToList();
            return GenerateTable(allplayerIds, tournament.Matches.Where(x => x.Group == group && x.PositionInPlayoff == null).ToList());

        }
        //      for (int round = 1; round<limit; round++) {
        //	Console.WriteLine("Round #{0}", round);
        //	branch(1, 1, limit - round + 1);
        //      Console.WriteLine();
        //}

        //public static List<MatchFull> GenerateBracket(int seed, int level, int limit)
        //{
        //    var levelSum = (int)Math.Pow(2, level) + 1;

        //    if (limit == level + 1)
        //    {
        //        Console.WriteLine("Seed {0} vs. Seed {1}", seed, levelSum - seed);
        //        return;
        //    }
        //    else if (seed % 2 == 1)
        //    {
        //        GenerateBracket(seed, level + 1, limit);
        //        GenerateBracket(levelSum - seed, level + 1, limit);
        //    }
        //    else
        //    {
        //        GenerateBracket(levelSum - seed, level + 1, limit);
        //        GenerateBracket(seed, level + 1, limit);
        //    }
        //}
    }
    public class TeamResult
    {
        public int Team { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int Matches => Wins + Losses;
    }

}


