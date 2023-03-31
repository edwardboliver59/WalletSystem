using Microsoft.AspNetCore.Mvc;
using WalletSystem.Contracts.Accounts;
using WalletSystem.Entities.ViewModels.Accounts;

namespace WalletSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }


        [HttpPost]
        public JsonResult Deposit([FromBody] DepositData value)
        {

            if (!ModelState.IsValid)
                return new JsonResult(ModelState);
            return new JsonResult(_transactionsService.Deposit(value).Value);

        }

        [HttpPost]
        public JsonResult Withdraw([FromBody] WithdrawData value)
        {

            if (!ModelState.IsValid)
                return new JsonResult(ModelState);
            return new JsonResult(_transactionsService.Withdraw(value).Value);

        }

        [HttpPost]
        public JsonResult TransferFunds([FromBody] TransferFundsData value)
        {

            if (!ModelState.IsValid)
                return new JsonResult(ModelState);
            return new JsonResult(_transactionsService.TransferFunds(value).Value);

        }


        [HttpGet]
        public JsonResult GetTransactionHistory(long AccountNumber)
        {

            if (!ModelState.IsValid)
                return new JsonResult(ModelState);
            return new JsonResult(_transactionsService.GetTransactionHistory(AccountNumber).Value);

        }
    }
}