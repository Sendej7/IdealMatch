using System.ComponentModel.DataAnnotations;

namespace webapi.Models.Accounts
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
