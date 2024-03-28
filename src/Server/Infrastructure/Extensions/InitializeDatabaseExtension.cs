namespace Server.Infrastructure.Extensions;

public static class InitializeDatabaseExtension
{
    public static async Task InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var database =
             scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var anyUser =
            database.Users!
            .Any()
            ;

        if (anyUser)
            return;

        var roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

        var roles =
           new List<string>()
           {
               RoleHelper.Admin,
               RoleHelper.Employee,
               RoleHelper.Teacher,
               RoleHelper.Student,
           };

        foreach (var item in roles)
        {
            var role = new Role
            {
                Name = item,
                IsDeleted = false,
            };

            await roleManager.CreateAsync(role);
        }

        var user = new User
        {
            FirstName = "Erfan",
            LastName = "Edalati",
            Email = "ErfannStreet@gmail.com",
            IsSystemic = true,
            UserName = "Erfan_Edalati",
        };

        var userManager =
            scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await userManager.CreateAsync(user, password: "erfanEdalati48@");

        await userManager.AddToRoleAsync(user, RoleHelper.Admin);
    }
}
