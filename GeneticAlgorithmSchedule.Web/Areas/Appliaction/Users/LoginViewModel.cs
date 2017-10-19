using System.ComponentModel.DataAnnotations;

namespace GeneticAlgorithmSchedule.Web.Areas.Appliaction.Users
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; internal set; }

        [Required]
        public string Password { get; internal set; }
        public bool RememberMe { get; internal set; }
    }
}