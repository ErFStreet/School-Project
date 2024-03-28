namespace Server.Infrastructure.Extensions;

public static class RegisterServicesExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Register Scpoes

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IClassService, ClassService>();

        services.AddScoped<IUserService, UserService>();
    }
}
