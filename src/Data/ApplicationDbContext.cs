namespace Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    #region Constrcuture
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options: options)
    {
    }
    #endregion /Constrcuture

    #region Properties
    public DbSet<Class>? Classes { get; set; }

    public DbSet<Lesson>? Lessons { get; set; }

    public DbSet<LearnRelation>? LearnRelations { get; set; }
    #endregion /Properties

    #region Methods
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());

        builder.ApplyConfiguration(new ClassConfiguration());

        builder.ApplyConfiguration(new LessonConfiguration());
    }
    #endregion /Methods
}
