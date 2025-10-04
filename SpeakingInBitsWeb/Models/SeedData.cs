using Microsoft.AspNetCore.Identity;

namespace SpeakingInBitsWeb.Models
{
    /// <summary>
    /// Provides methods for seeding initial data into the database.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Creates all necessary roles in the system if they don't already exist.
        /// </summary>
        /// <param name="roleManager">The role manager service.</param>
        public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = ["Instructor", "Student"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        /// <summary>
        /// Creates a default instructor user if one doesn't already exist.
        /// </summary>
        /// <param name="userManager">The user manager service.</param>
        public static async Task CreateDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            string defaultEmail = "instructor@example.com";
            ApplicationUser? defaultUser = await userManager.FindByEmailAsync(defaultEmail);

            if (defaultUser == null)
            {
                ApplicationUser newUser = new()
                {
                    UserName = "DefaultInstructor",
                    Email = defaultEmail,
                    EmailConfirmed = true,
                    FirstName = "Default",
                    LastName = "Instructor"
                };

                // Ensure password meets password strength requirements
                IdentityResult result = await userManager.CreateAsync(newUser, "Programming01#");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "Instructor");
                }
            }
        }
    }
}
