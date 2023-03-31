using NUnit.Framework;
using WalletSystem.BusinessLogic.Services;
using WalletSystem.Contracts.Accounts;
using WalletSystem.Entities.ViewModels.Accounts;
using WalletSystem.WebApi.Controllers;

namespace WalletSystem.UnitTest
{
    [TestFixture]
    public class AccountsControllerTest
    {
        private AccountService _accountService;
      
        public void OneTimeSetUp()
        {
            // Method intentionally left empty.
        }

        [SetUp]
        public void Setup()
        {
            _accountService = new AccountService();
        }

        [Test]
        public void RegisterAccountTest()
        {
            var sampleData = new RegisterData()
            {
                LoginName = "admin",
                Password = "password"
            };

            var actualResult = _accountService.RegisterAccount(sampleData) != null;

            Assert.That(actualResult, Is.True);
        }

        [Test]
        public void LoginTest()
        {
            var actualResult = _accountService.Login("admin", "password") != null;

            Assert.That(actualResult, Is.True);
        }
  
        
    }
}