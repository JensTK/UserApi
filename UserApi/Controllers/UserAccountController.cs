using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApi.Model;

namespace UserApi.Controllers
{
    [ApiController]
    public class UserAccountController : Controller
    {
        [HttpGet]
        [Route("users")]
        [Produces("application/json")]
        // [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        public ActionResult GetUserAccounts()
        {
            try
            {
                return Ok(UserAccountDb.GetList<UserAccount>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("users/getUser")]
        [Produces("application/json")]
        // [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        public ActionResult GetUserAccount(string userName)
        {
            try
            {
                var account = UserAccountDb.Get<UserAccount, string>(userName);
                return account != null ? Ok(account) : BadRequest("Account not found");                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("users")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public ActionResult CreateUserAccount(UserAccount account)
        {
            try
            {
                var accountExists = UserAccountDb.Get<UserAccount, string>(account.UserName) != null;
                if (!accountExists)
                {
                    UserAccountDb.Insert(account);
                    return Ok();
                }
                return BadRequest($"Account with username {account.UserName} already exists");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("users")]
        [Consumes("application/json")]
        public ActionResult DeleteUserAccount(string userName)
        {
            try
            {
                var account = UserAccountDb.Get<UserAccount, string>(userName);

                if (account != null)
                {
                    UserAccountDb.Delete(account);
                    return Ok();
                }
                else
                {
                    return BadRequest("Account not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
