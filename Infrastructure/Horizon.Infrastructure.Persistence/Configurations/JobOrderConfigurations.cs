namespace Horizon.Infrastructure.Persistence.Configurations;
internal class JobOrderConfigurations : IEntityTypeConfiguration<JobOrder>
{
    public void Configure(EntityTypeBuilder<JobOrder> builder)
    {
        builder.ToTable("JobOrders");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.OrderCode)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(j => j.OrderType)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(j => j.OrderDate)
               .IsRequired();

        builder.Property(j => j.NumberOfCows)
               .IsRequired();

        builder.Property(j => j.ClientName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(j => j.ClientCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(j => j.Status)
                .HasConversion<string>()
                .HasMaxLength(25)
                .IsRequired();

    }
}
