namespace TeamLegend.Data.EntityConfiguration
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.CurrentTeam)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.CurrentTeamId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
