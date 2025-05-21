namespace Horizon.Infrastructure.Persistence.Configurations;
internal class SlaughteredCowConfiguration : IEntityTypeConfiguration<SlaughteredCow>
{
    public void Configure(EntityTypeBuilder<SlaughteredCow> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CowIdentifier).IsRequired().HasMaxLength(50);
        builder.Property(x => x.OrderCode).IsRequired().HasMaxLength(50);

        builder.HasMany(x => x.Quarters)
               .WithOne(q => q.SlaughteredCow)
               .HasForeignKey(q => q.SlaughteredCowId);

        builder.Property(c => c.IsUnplanned)
               .IsRequired()
               .HasDefaultValue(true);

    }
}

