-- Run once, after registering the account that should become the first SuperAdmin.
-- Replace the email below with the account's exact registered email.
DECLARE @Email NVARCHAR(256) = N'CHANGE-ME@example.com';
DECLARE @UserId NVARCHAR(450);

SELECT @UserId = Id FROM dbo.AspNetUsers WHERE Email = @Email;
IF @UserId IS NULL
    THROW 50001, 'No Identity user exists with that email.', 1;

DECLARE @RoleId NVARCHAR(450);
SELECT @RoleId = Id FROM dbo.AspNetRoles WHERE Name = N'SuperAdmin';
IF @RoleId IS NULL
    THROW 50002, 'SuperAdmin role was not seeded. Run the application once first.', 1;

IF NOT EXISTS (SELECT 1 FROM dbo.AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId)
    INSERT INTO dbo.AspNetUserRoles (UserId, RoleId) VALUES (@UserId, @RoleId);
