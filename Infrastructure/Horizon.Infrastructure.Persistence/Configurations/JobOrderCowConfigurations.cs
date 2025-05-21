namespace Horizon.Infrastructure.Persistence.Configurations;
internal class JobOrderCowConfigurations : IEntityTypeConfiguration<JobOrderCow>
{
    public void Configure(EntityTypeBuilder<JobOrderCow> builder)
    {
        builder.ToTable("JobOrderCows");

        builder.HasKey(jc => jc.Id);


        builder.Property(jc => jc.CowIdentifier)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(jc => jc.Type)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(jc => jc.OrderCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(jc => jc.TypeId)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(jc => jc.Weight)
               .IsRequired();

        builder.Property(jc => jc.JobId)
               .IsRequired();

        builder.HasOne(jc => jc.JobOrder)
               .WithMany(j => j.Cows)
               .HasForeignKey(jc => jc.JobId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
