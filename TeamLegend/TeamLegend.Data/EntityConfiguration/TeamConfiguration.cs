namespace TeamLegend.Data.EntityConfiguration
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Stadium)
                .WithMany(s => s.TenantTeams)
                .HasForeignKey(t => t.StadiumId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.League)
                .WithMany(l => l.Teams)
                .HasForeignKey(t => t.LeagueId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.Manager)
                .WithOne(m => m.Team)
                .HasForeignKey<Manager>(m => m.TeamId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}
