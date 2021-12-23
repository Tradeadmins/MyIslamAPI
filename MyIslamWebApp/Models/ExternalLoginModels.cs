using System.ComponentModel.DataAnnotations;
using MyIslamWebApp.Enums;

namespace MyIslamWebApp.Models
{
    public class RegisterExternalBindingModel
    {
        [Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool SubscriptionComplete { get; set; }
        [Required]
        public string Provider { get; set; }
        [Required]
        public string ExternalUserId { get; set; }        
    }

    public class LoginExternalBindingModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string ExternalUserId { get; set; }
    }

    public class ParsedExternalAccessToken
    {
        public string user_id { get; set; }
        public string app_id { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}