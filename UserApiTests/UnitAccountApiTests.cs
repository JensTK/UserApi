using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Linq;
using UserApi.Controllers;
using UserApi.Model;
using UserApi.DB;

namespace UserApiTests
{
    public class Tests
    {
        private UserAccountController userAccountController;
        private UserAccountDbMock dbTest;

        private List<UserAccount> testAccounts = new() {
                new() { UserId = 0, UserName = "Jensern", Email = "jens@jens.jens" },
                new() { UserId = 1, UserName = "Jens2" },
                new() { UserId = 1, UserName = "Jenstre" },
            };

        [SetUp]
        public void Setup()
        {
            dbTest = new UserAccountDbMock();
            userAccountController = new UserAccountController(dbTest);
        }

        [Test]
        public void GetShouldReturnAll()
        {
            dbTest.userAccounts = testAccounts;
            var accounts = userAccountController.GetUserAccounts().Result;
            Assert.IsInstanceOf<OkObjectResult>(accounts);
            var results = (accounts as OkObjectResult).Value as List<UserAccount>;
            foreach (var  account in testAccounts)
            {
                Assert.That(results.Contains(account));
            }
        }
    }
}