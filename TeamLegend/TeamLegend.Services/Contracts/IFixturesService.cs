namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;

    public interface IFixturesService
    {
        Task<Fixture> GetOrCreateAsync(int fixtureRound, string leagueId);

        Task<Fixture> GetByLeagueIdAndRoundAsync(string leagueId, int round);

        Task<int> ValidateRoundAsync(string leagueId, int round);
    }
}
