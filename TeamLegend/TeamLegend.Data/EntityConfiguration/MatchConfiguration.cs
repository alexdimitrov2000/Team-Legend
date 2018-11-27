namespace TeamLegend.Data.EntityConfiguration
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.HomeTeam)
                .WithMany(t => t.Matches)
                .HasForeignKey(m => m.HomeTeamId);

            // Unnecessary relationship configuration
            // builder.HasOne(m => m.AwayTeam)
            //     .WithMany(t => t.Matches)
            //     .HasForeignKey(m => m.AwayTeamId);

            builder.HasOne(m => m.Fixture)
                .WithMany(f => f.Matches)
                .HasForeignKey(m => m.FixtureId);
        }
    }
}
