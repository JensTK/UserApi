using Dapper.Contrib.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApi.Model
{
    [SwaggerSchema("User account")]
    public class UserAccount : IDbObject
    {
        [Key]
        [SwaggerSchema("User ID", ReadOnly = true)]
        public int UserId { get; set; }
        [SwaggerSchema("Username, must be unique")]
        public string UserName { get; set; }
        [SwaggerSchema("Email address")]
        public string? Email { get; set; }
    }
}
