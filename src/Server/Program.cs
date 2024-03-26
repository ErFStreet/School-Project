namespace Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register Services

        builder.Services.RegisterServices();

        builder.Services.RegisterJwt(configuration: builder.Configuration);

        builder.Services.RegisterDatabase(configuration: builder.Configuration);

        // Register Application

        var app = builder.Build();

        // Initialize Database

        await app.InitializeDatabase();

        app.RegisterApplication();

        app.Run();
    }
}
