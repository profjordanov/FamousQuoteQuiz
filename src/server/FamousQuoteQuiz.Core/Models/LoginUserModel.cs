using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Core.Models
{
    public class LoginUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
