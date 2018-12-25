namespace TeamLegend.Data.EntityConfiguration
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
    {
        public void Configure(EntityTypeBuilder<Fixture> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasIndex(f => new { f.FixtureRound, f.LeagueId })
                .IsUnique();

            builder.HasOne(f => f.League)
                .WithMany(l => l.Fixtures)
                .HasForeignKey(f => f.LeagueId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
