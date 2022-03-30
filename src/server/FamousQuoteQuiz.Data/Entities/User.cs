using Microsoft.AspNetCore.Identity;
using System;

namespace FamousQuoteQuiz.Data.Entities
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
