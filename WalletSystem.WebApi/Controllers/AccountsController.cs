using Microsoft.AspNetCore.Mvc;
using WalletSystem.Contracts.Accounts;
using WalletSystem.Entities.ViewModels.Accounts;

namespace WalletSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        public JsonResult RegisterAccount([FromBody] RegisterData value)
        {

            if (!ModelState.IsValid)
                return new JsonResult(ModelState);
            return new JsonResult(_accountService.RegisterAccount(value).Value);

        }

        [HttpGet]
        public JsonResult Login(string LoginName, string Password)
        {

            if (!ModelState.IsValid)
                return new JsonResult(ModelState);
            return new JsonResult(_accountService.Login(LoginName, Password).Value);

        }

    }
}