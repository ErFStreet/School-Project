namespace Data.Configurations;

internal class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.Property(current => current.ClassCode)
            .IsRequired();
    }
}
