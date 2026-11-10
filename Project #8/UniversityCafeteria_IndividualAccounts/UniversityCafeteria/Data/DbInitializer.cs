using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UniversityCafeteria.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureCreatedAsync();

        var roles = new[] { "User", "Admin", "SuperAdmin" };
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

        // No privileged account is created with a hard-coded password.
        // Create the first SuperAdmin manually in the database or add a secure,
        // environment-variable-based bootstrap process before deployment.
    }
}
