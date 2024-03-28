namespace Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Class>? Classes { get; set; }

    public DbSet<Lesson>? Lessons { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());

        builder.ApplyConfiguration(new ClassConfiguration());

        builder.ApplyConfiguration(new LessonConfiguration());
    }
}
