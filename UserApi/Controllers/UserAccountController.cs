using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;
using UserApi.DB;
using UserApi.Model;

namespace UserApi.Controllers
{
    [ApiController]
    [SwaggerTag("Manage user accounts")]
    public class UserAccountController : Controller
    {
        private IConfiguration configuration;
        public IUserAccountDb UserAccountDb { get; set; }

        public UserAccountController(IConfiguration configuration)
        {
            configuration = configuration;
            UserAccountDb = new UserAccountDb(configuration.GetConnectionString("UserAccountDb"));
        }

        [HttpGet]
        [Route("users")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get all users")]
        // [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        public ActionResult<List<UserAccount>> GetUserAccounts()
        {
            Debug.WriteLine("GET /users");
            try
            {
                return Ok(UserAccountDb.GetList<UserAccount>());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("users/getUser")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Get single user account, by username or ID. Only one value is allowed.")]
        // [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        public ActionResult<UserAccount> GetUserAccount(int? userId = null, string? userName = null)
        {
            Debug.WriteLine($"GET /users/getUser: {userId?.ToString() ?? userName}");
            if ((userId == null && userName == null) || (userId != null && userName != null)) {
                return BadRequest("Either id or username must be set");
            }
            try
            {
                var account = (userId != null) ? (UserAccountDb.Get<UserAccount, int>((int)userId)) : (UserAccountDb.GetUserAccountByUserName(userName));
                if (account == null) {
                    Debug.WriteLine($"Account { userId?.ToString() ?? userName } not found");
                    return BadRequest("Account not found"); 
                }

                return Ok(account);                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("users")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Create user accounts. User names must be unique.")]
        public ActionResult CreateUserAccounts(List<UserAccount> accounts)
        {
            Debug.WriteLine($"POST /users: {accounts.Count} users");
            try
            {
                var accountExists = UserAccountDb.GetList<UserAccount>().Where(x => accounts.Select(y => y.UserName).Contains(x.UserName));
                if (accountExists.Any())
                {
                    Debug.WriteLine($"Usernames {string.Join(", ", accountExists.Select(x => x.UserName))} already exist");
                    return BadRequest($"Usernames {string.Join(", ", accountExists.Select(x => x.UserName))} already exist");
                }
                UserAccountDb.Insert(accounts);
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("users")]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Delete a user account, by username or ID. Only one value is allowed.")]
        public ActionResult DeleteUserAccount(int? userId = null, string? userName = null)
        {
            Debug.WriteLine($"DELETE /users: {userId?.ToString() ?? userName}");
            if ((userId == null && userName == null) || (userId != null && userName != null))
            {
                return BadRequest("Either id or username must be set");
            }
            try
            {
                var account = (userId != null) ? (UserAccountDb.Get<UserAccount, int>((int)userId)) : (UserAccountDb.GetUserAccountByUserName(userName));
                if (account == null)
                {
                    Debug.WriteLine($"Account {userId?.ToString() ?? userName} not found");
                    return BadRequest("Account not found");
                }
                UserAccountDb.Delete(account);
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
