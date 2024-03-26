namespace Data.Configurations;

internal class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.Property(current => current.LessonName)
            .IsRequired();

        builder.Property(current => current.LessonDescription)
            .IsRequired();
    }
}
