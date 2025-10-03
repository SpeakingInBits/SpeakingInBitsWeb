using Microsoft.AspNetCore.Identity;

namespace SpeakingInBitsWeb.Models
{
    /// <summary>
    /// Represents a custom application user with additional profile information.
    /// </summary>
    /// <remarks>Use this class to store and manage user-specific data beyond the default identity
    /// fields.</remarks>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// The user's legal first name
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// The user's legal last name
        /// </summary>
        public required string LastName { get; set; }
    }
}
