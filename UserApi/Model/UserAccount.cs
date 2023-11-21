using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace UserApi.Model
{
    public class UserAccount : IDbObject
    {
        [ExplicitKey]
        [Required]
        public string UserName { get; set; }

        public string? PasswordHash { get; set; }
        public string? Email { get; set; }

        public UserAccount() { }

        public UserAccount(string UserName, string? PasswordHash = null, string? Email = null)
        {
            this.UserName = UserName;
            this.PasswordHash = PasswordHash;
            this.Email = Email;
        }
    }
}
