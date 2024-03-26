namespace Server.Infrastructure.Extensions;

public static class RegisterDatabaseExtension
{
    public static void RegisterDatabase(this IServiceCollection services, IConfiguration
        configuration)
    {
        var connectionString =
            configuration["DatabaseSettings:ConnectionString"];

        if (connectionString is null)
            throw new NullReferenceException(nameof(connectionString));

        services.AddDbContext<ApplicationDbContext>
            (options => options.UseSqlServer(connectionString: connectionString));
    }
}
