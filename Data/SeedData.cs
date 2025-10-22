//Data/SeedData.cs
using ClaimSystem.Web.Models;
using ClaimSystem.Web.Data;

using Microsoft.AspNetCore.Identity;

namespace ClaimSystem.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            string[] roles = new[] { "Lecturer", "Coordinator", "Manager" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // create test users if not exist
            var testLect = await userManager.FindByEmailAsync("lecturer@example.com");
            if (testLect == null)
            {
                var u = new ApplicationUser { UserName = "lecturer@example.com", Email = "lecturer@example.com" };
                await userManager.CreateAsync(u, "Password123!");
                await userManager.AddToRoleAsync(u, "Lecturer");
            }

            var testCoord = await userManager.FindByEmailAsync("coordinator@example.com");
            if (testCoord == null)
            {
                var u = new ApplicationUser { UserName = "coordinator@example.com", Email = "coordinator@example.com" };
                await userManager.CreateAsync(u, "Password123!");
                await userManager.AddToRoleAsync(u, "Coordinator");
            }

            var testMan = await userManager.FindByEmailAsync("manager@example.com");
            if (testMan == null)
            {
                var u = new ApplicationUser { UserName = "manager@example.com", Email = "manager@example.com" };
                await userManager.CreateAsync(u, "Password123!");
                await userManager.AddToRoleAsync(u, "Manager");
            }
        }
    }
}
