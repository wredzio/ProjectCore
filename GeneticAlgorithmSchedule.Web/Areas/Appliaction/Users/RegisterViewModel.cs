using System.ComponentModel.DataAnnotations;

namespace GeneticAlgorithmSchedule.Web.Areas.Appliaction.Users
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; internal set; }

        [Required]
        public string Password { get; internal set; }

        [Required]
        [CompareAttribute(nameof(Password), ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; internal set; }

        [Required]
        [EmailAddress]
        public string Email { get; internal set; }
    }
}