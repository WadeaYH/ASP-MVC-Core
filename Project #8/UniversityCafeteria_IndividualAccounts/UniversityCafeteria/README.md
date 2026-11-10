# University Cafeteria Supply Request Management

ASP.NET Core MVC project using **Individual Accounts / ASP.NET Core Identity** and Entity Framework Core with SQL Server.

## Roles
- **User**: register, login, browse products, create supply requests, and view only their own requests.
- **Admin**: all product management, view all requests, approve/reject requests.
- **SuperAdmin**: Admin permissions plus user management and role changes.

New registrations are assigned the `User` role automatically. The application does not create custom user or role tables: `ApplicationDbContext` inherits from `IdentityDbContext<ApplicationUser>`, so Identity manages its standard tables (`AspNetUsers`, `AspNetRoles`, `AspNetUserRoles`, etc.).

## Run
1. Open `UniversityCafeteria.csproj` in Visual Studio 2026 / an IDE supporting .NET 10.
2. Ensure SQL Server LocalDB is installed. Change `ConnectionStrings:DefaultConnection` in `appsettings.json` if you use another SQL Server instance.
3. Restore NuGet packages and run the project.
4. On first run, `EnsureCreated` creates the database and Identity tables, and the app seeds the three role definitions.

## Important first-time setup: SuperAdmin
For security, this project does **not** create a SuperAdmin with a hard-coded password. Create a user through Register, then promote that account to `SuperAdmin` once using a trusted database/admin setup process. After that, the SuperAdmin can manage roles through the Manage Users page. Do not expose an unauthenticated role-assignment endpoint.

## Database note
This starter uses `EnsureCreated()` to make first-run setup straightforward. If you later add EF Core migrations, switch to `Database.MigrateAsync()` and create/apply a migration. Do not mix `EnsureCreated` and migrations on the same database.

## Notes
- The registration form saves Full Name on the custom `ApplicationUser`.
- Product stock is validated when requests are submitted. Stock is not decremented on approval because the assignment does not explicitly define inventory deduction rules.
- Users can edit or delete their own requests while they remain Pending; admins can review all requests and change their status.
