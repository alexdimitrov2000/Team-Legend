using System;
using System.Collections.Generic;
using System.Linq;
using TeamLegend.Models;

namespace GenerateMatches
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Coverlet
            var teams = new List<Team>
            {
                new Team {Id = "1", Name = "Manchester United"},
                new Team {Id = "2", Name = "Manchester City"},
                new Team {Id = "3", Name = "Liverpool"},
                new Team {Id = "4", Name = "Tottenham"},
                new Team {Id = "5", Name = "Chelsea"},
                new Team {Id = "6", Name = "Cardiff"},
                new Team {Id = "7", Name = "Arsenal"},
                new Team {Id = "8", Name = "Watford"},
                new Team {Id = "9", Name = "West Brom"},
                new Team {Id = "10", Name = "Bolton"},
                new Team {Id = "11", Name = "Birmingham"},
                new Team {Id = "12", Name = "Southampton"},
                new Team {Id = "13", Name = "Huddersfield"},
                new Team {Id = "14", Name = "Leicester"},
            };

            var matches = new List<Match>();

            var random = new Random();

            //foreach (var team in teams)
            //{
            //    var opponents = teams.Where(t => t.Name != team.Name).ToList();

            //    //var weekNumber = 1;
            //    //foreach (var opponent in opponents)
            //    //{
            //    //    matches.Add(new Match
            //    //    {
            //    //        HomeTeam = team,
            //    //        AwayTeam = opponent,
            //    //        Date = DateTime.UtcNow.AddDays(7 * weekNumber)
            //    //    });
            //    //    weekNumber++;
            //    //}
            //    for (int i = opponents.Count; i > 0; i--)
            //    {
            //        var opponent = opponents[random.Next(0, opponents.Count)];
            //        var opponentName = opponent.Name;
            //        var matchDate = DateTime.UtcNow.AddDays(7 * i);

            //        matches.Add(new Match
            //        {
            //            HomeTeam = team,
            //            HomeTeamId = team.Id,
            //            AwayTeam = opponent,
            //            AwayTeamId = opponent.Id,
            //            Date = matchDate,
            //        });

            //        opponents.Remove(opponent);
            //    }
            //}

            var days = 1;
            while (teams.Count > 0)
            {
                var teamsCount = teams.Count;
                var homeTeam = teams[random.Next(0, teamsCount)];
                var awayTeam = teams[random.Next(0, teamsCount)];

                if (homeTeam.Name == awayTeam.Name)
                    continue;
                var match = new Match
                {
                    HomeTeam = homeTeam,
                    AwayTeam = awayTeam,
                    Date = DateTime.UtcNow.AddDays(days)
                };
                days++;
                matches.Add(match);
                teams.Remove(homeTeam);
                teams.Remove(awayTeam);
            }

            var lastMatches = matches.OrderByDescending(m => m.Date).ToList();
            lastMatches.ForEach(m => Console.WriteLine($"On {m.Date.ToShortDateString()} {m.HomeTeam.Name} vs {m.AwayTeam.Name}"));

            var matchesOn31 = matches.Where(m => m.Date.Day == 25).ToList();
            Console.WriteLine();

            var fixture = new Fixture
            {
                FixtureRound = 1,
            };

            foreach (var match in matchesOn31)
            {
                //if (fixture.Matches.Any(m => m.HomeTeamId == match.HomeTeamId || m.HomeTeamId == match.AwayTeamId
                //                     || m.AwayTeamId == match.HomeTeamId || m.AwayTeamId == match.AwayTeamId))
                //{
                //    continue;
                //}

                fixture.Matches.Add(match);

                //Console.WriteLine($"On {match.Date.ToShortDateString()} {match.HomeTeam.Name} vs {match.AwayTeam.Name}");
            }

            Console.WriteLine();
        }
    }
}
