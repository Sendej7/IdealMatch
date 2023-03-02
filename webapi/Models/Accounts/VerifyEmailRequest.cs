using System.ComponentModel.DataAnnotations;

namespace webapi.Models.Accounts
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
