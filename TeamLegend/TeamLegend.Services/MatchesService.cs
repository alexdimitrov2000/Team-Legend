namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;

    public class MatchesService : IMatchesService
    {
        private readonly ApplicationDbContext context;

        public MatchesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Match> CreateAsync(Match match)
        {
            if (match == null)
                return null;

            await this.context.Matches.AddAsync(match);
            await this.context.SaveChangesAsync();

            return match;
        }

        public async Task<List<Match>> GetAllPlayedAsync()
        {
            return await this.context.Matches.Where(m => m.IsPlayed == true).ToListAsync();
        }

        public async Task<List<Match>> GetAllUnplayedAsync()
        {
            return await this.context.Matches.Where(m => m.IsPlayed == false).ToListAsync();
        }
        
        public async Task<List<Match>> GenerateMatches(League league)
        {
            //var teams = league.Teams.ToList();
            //teams.Remove(teams.First(t => t.Name == "Chelsea FC"));

            //var matches = new List<Match>();

            //var random = new Random();

            //foreach (var team in teams)
            //{
            //    var opponents = teams.Where(t => t.Name != team.Name).ToList();

            //    for (int i = opponents.Count; i > 0; i--)
            //    {
            //        var opponent = opponents[random.Next(0, opponents.Count)];
            //        var matchDate = DateTime.UtcNow.AddDays(7 * i);

            //        var match = new Match
            //        {
            //            HomeTeam = team,
            //            HomeTeamId = team.Id,
            //            AwayTeam = opponent,
            //            AwayTeamId = opponent.Id,
            //            Date = matchDate,
            //        };

            //        matches.Add(match);
            //        opponents.Remove(opponent);
            //    }
            //}

            //var teamsToUpdate = new List<Team>();
            //while (teams.Count > 0)
            //{
            //    var teamsCount = teams.Count;
            //    var homeTeam = teams[random.Next(0, teamsCount)];
            //    var awayTeam = teams[random.Next(0, teamsCount)];

            //    if (homeTeam.Name == awayTeam.Name)
            //        continue;

            //    var match = new Match
            //    {
            //        HomeTeam = homeTeam,
            //        AwayTeam = awayTeam,
            //        Date = DateTime.UtcNow
            //    };

            //    matches.Add(match);

            //    homeTeam.Matches.Add(match);
            //    awayTeam.Matches.Add(match);
            //    teamsToUpdate.Add(homeTeam);
            //    teamsToUpdate.Add(awayTeam);

            //    teams.Remove(homeTeam);
            //    teams.Remove(awayTeam);
            //    Console.WriteLine($"{match.HomeTeam.Name} vs {match.AwayTeam.Name}");
            //}

            //this.context.Teams.UpdateRange(teamsToUpdate);
            //await this.context.Matches.AddRangeAsync(matches);
            //await this.context.SaveChangesAsync();

            //return matches;
            return null;
        }
    }
}
