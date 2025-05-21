
namespace Horizon.Infrastructure.Persistence.Configurations;
internal class CowQuarterConfiguration : IEntityTypeConfiguration<CowQuarter>
{
    public void Configure(EntityTypeBuilder<CowQuarter> builder)
    {
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Sequence).IsRequired();
        builder.Property(q => q.Weight).IsRequired();
        builder.Property(q => q.CreatedAt).IsRequired();

        builder.Property(q => q.QuarterType)
               .HasConversion<string>()
               .IsRequired();
    }
}
