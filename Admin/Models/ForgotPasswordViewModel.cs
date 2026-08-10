using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
