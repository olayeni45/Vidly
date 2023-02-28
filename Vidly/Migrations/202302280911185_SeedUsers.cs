namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) 
                    VALUES (N'2f19c9c8-d602-49de-99ab-33f7b38b6b4a', N'admin@vidly.com', 0, N'AMWzRwvkV6DZ/VxQxgI6K0t52oZgrrwWvMnfqsLsNZgfzoj82jsX3roeJR72YQxR0A==', N'845c85e3-ea41-4d52-bf58-431ec97fecae', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) 
                    VALUES (N'eec84f2c-12ad-4d01-bd95-22b092559448', N'guest@vidly.com', 0, N'AIIDPe5Xa3vYJWDAqwV7ryaitSOlVP/H/LtkqkUc938S/duhZx6quwPo1lP8fXuUpQ==', N'51dc1e3d-0f7b-4010-bdb9-6f3522aa592e', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

                    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) 
                    VALUES (N'febfc838-30e3-44ed-9c35-fc4985a7afb0', N'CanManageMovies')

                    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) 
                    VALUES (N'2f19c9c8-d602-49de-99ab-33f7b38b6b4a', N'febfc838-30e3-44ed-9c35-fc4985a7afb0')

                ");
        }

        public override void Down()
        {
        }
    }
}
