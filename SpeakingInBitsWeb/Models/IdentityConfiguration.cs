using Microsoft.AspNetCore.Identity;

namespace SpeakingInBitsWeb.Models
{
    /// <summary>
    /// Provides configuration methods for ASP.NET Core Identity options.
    /// </summary>
    public static class IdentityConfiguration
    {
        /// <summary>
        /// Configures the Identity options for the application.
        /// </summary>
        /// <param name="options">The IdentityOptions to configure.</param>
        public static void ConfigureIdentityOptions(IdentityOptions options)
        {
            // Password settings
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 3;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;

            // Sign in settings
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        }
    }
}
