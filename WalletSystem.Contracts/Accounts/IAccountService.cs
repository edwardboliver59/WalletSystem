using Microsoft.AspNetCore.Mvc;
using WalletSystem.Entities.ViewModels.Accounts;

namespace WalletSystem.Contracts.Accounts
{
    public interface IAccountService
    {
        JsonResult RegisterAccount(RegisterData value);
        JsonResult Login(string LoginName, string Password);
    }
}
