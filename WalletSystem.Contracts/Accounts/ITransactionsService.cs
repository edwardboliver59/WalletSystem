using Microsoft.AspNetCore.Mvc;
using WalletSystem.Entities.ViewModels.Accounts;

namespace WalletSystem.Contracts.Accounts
{
    public interface ITransactionsService
    {
        JsonResult Deposit(DepositData value);
        JsonResult Withdraw(WithdrawData value);
        JsonResult TransferFunds(TransferFundsData value);
        JsonResult GetTransactionHistory(long AccountNumber);

    }
}
