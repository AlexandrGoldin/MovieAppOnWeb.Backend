﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieApp.ApplicationCore.Constants;
using MovieApp.Infrastructure.Data;

namespace MovieApp.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(ApplicationContext identityDbContext,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (identityDbContext.Database.IsSqlServer())
            {
                identityDbContext.Database.Migrate();
            }

            await roleManager.CreateAsync(new IdentityRole(Roles.ADMINISTRATORS));

            var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

            string adminUserName = "admin@microsoft.com";
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser != null)
            {
                await userManager.AddToRoleAsync(adminUser, Roles.ADMINISTRATORS);
            }
        }
    }
}
