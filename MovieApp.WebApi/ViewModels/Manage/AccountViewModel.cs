using System.ComponentModel.DataAnnotations;

namespace MovieApp.WebApi.ViewModels.Manage
{
    public class AccountViewModel
    {
        public string? Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        public string? StatusMessage { get; set; }
    }
}
