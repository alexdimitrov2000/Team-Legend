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
        }
    }
}
