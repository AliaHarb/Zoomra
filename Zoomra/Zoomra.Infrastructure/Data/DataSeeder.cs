using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zoomra.Domain.Entities;

namespace Zoomra.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
           
            var roles = new List<string>() { "Admin", "Hospital", "Donor" };

            foreach (var role in roles)
            {
                var result = await roleManager.RoleExistsAsync(role);

                if (!result)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@zoomra.com";
            var adminPassword = "Password@123"; 

            var existAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existAdmin is null)
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = "AdminZoomra",
                    FullName = "System Administrator",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    // لازم ندي قيم للحقول الإجبارية اللي ضفناها عشان الـ CreateAsync ميضربش إيرور
                    NationalId = "00000000000000",
                    BloodType = "O+",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    TotalDonationsCount = 0,
                    RewardPoints = 0
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    // هندي لليوزر ده صلاحية الأدمن
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}