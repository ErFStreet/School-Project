namespace Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //********************

        builder.Property
            (current => current.SecurityStamp)
                .IsRequired();
        //********************

        //********************

        builder.Property
            (current => current.FirstName)
                .HasMaxLength(ValidationHelper.FirstName)
                .IsRequired();
        //********************

        //********************

        builder.Property
            (current => current.LastName)
                .HasMaxLength(ValidationHelper.LastName)
                .IsRequired();
        //********************

        //********************
        builder.Property
            (current => current.IsBanned)
            .HasDefaultValue(false)
            .IsRequired();

        //********************
    }
}
