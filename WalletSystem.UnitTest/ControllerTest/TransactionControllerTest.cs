using NUnit.Framework;
using WalletSystem.BusinessLogic.Services;
using WalletSystem.Contracts.Accounts;
using WalletSystem.Entities.ViewModels.Accounts;
using WalletSystem.WebApi.Controllers;

namespace WalletSystem.UnitTest.ControllerTest
{
    [TestFixture]
    public class TransactionControllerTest
    {
        private TransactionsService _transactionsService;

        public void OneTimeSetUp()
        {
            // Method intentionally left empty.
        }


        [SetUp]
        public void Setup()
        {
            _transactionsService = new TransactionsService();
        }


        [Test]
        public void DepositTest()
        {
            var sampleData = new DepositData()
            {
                AccountNumber = 2023331124234,
                Amount = 1000
            };

            var actualResult = _transactionsService.Deposit(sampleData) != null;

            Assert.That(actualResult, Is.True);
        }

     
        [Test]
        public void TransferFundsTest()
        {
            var sampleData = new TransferFundsData()
            {
                FromAccountNumber = 2023331124234,
                ToAccountNumber = 2023331124228,
                Amount = 1000
            };
            
            var actualResult = _transactionsService.TransferFunds(sampleData) != null;

            Assert.That(actualResult, Is.True);
        }

        [Test]
        public void GetTransactionHistoryTest()
        {
            var actualResult = _transactionsService.GetTransactionHistory(2023331124234) != null;

            Assert.That(actualResult, Is.True);
        }
    }
}