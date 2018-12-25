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
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(m => m.Fixture)
                .WithMany(f => f.Matches)
                .HasForeignKey(m => m.FixtureId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
