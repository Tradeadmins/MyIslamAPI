using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using MyIslamWebApp.Enums;

namespace MyIslamWebApp.Entities
{
    /// <summary>
    /// User Model
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// SubscriptionComplete
        /// </summary>
        public bool SubscriptionComplete { get; set; }
    }
}