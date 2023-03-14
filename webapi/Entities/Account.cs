using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;
using webapi.Entities.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webapi.Entities
{
    public class Account
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public ShowMe ShowMe { get; set; }
        public string Email { get; set; } = string.Empty;
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        [Column("AcceptTerms")]
        public int _AcceptTerms { get; set; }
        [NotMapped]
        public bool AcceptTerms
        {
            get
            {
                return _AcceptTerms != 0;
            }
            set
            {
                _AcceptTerms = value ? 1 : 0;
            }
        }

        public Role Role { get; set; }
        public string VerificationToken { get; set; } = string.Empty;
        public DateTime? Verified { get; set; }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}
