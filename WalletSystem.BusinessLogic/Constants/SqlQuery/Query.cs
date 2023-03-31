using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletSystem.BusinessLogic.Constants.SqlQuery
{
    public static class Query
    {

        public static string AddAccountQuery()
        {
            string query = "INSERT INTO [WalletSystem].[dbo].[Account] (LoginName,AccountNumber,Password,Balance,RegisterDate) VALUES (@LoginName,@AccountNumber,@Password,@Balance, @RegisterDate)";
            return query;
        }

        public static string AddAccountHistoryQuery()
        {
            string query = "INSERT INTO [WalletSystem].[dbo].[TransactionHistory] (TransactionType,Amount,AccountNumber,DateOfTransaction,EndBalance) VALUES (@TransactionType,@Amount,@AccountNumber,@DateOfTransaction, @EndBalance)";
            return query;
        }

        public static string AddTransferHistoryQuery()
        {
            string query = "INSERT INTO [WalletSystem].[dbo].[TransactionHistory] (TransactionType,Amount,AccountNumber,DateOfTransaction,EndBalance) VALUES (@TransactionType,@Amount,@AccountNumber,@DateOfTransaction, @EndBalance)";
            return query;
        }
        public static string AddReceivedHistoryQuery()
        {
            string query = "INSERT INTO [WalletSystem].[dbo].[TransactionHistory] (TransactionType,Amount,AccountNumber,DateOfTransaction,EndBalance) VALUES (@TransactionType,@Amount,@AccountNumber,@DateOfTransaction, @EndBalance)";
            return query;
        }


        public static string TransferFundsQuery()
        {
            string query = "UPDATE [WalletSystem].[dbo].[Account] SET Balance = @Balance WHERE AccountNumber = @AccountNumber";
            ;
            return query;
        }
        public static string GetTransactionHistoryQuery(long AccountNumber)
        {
            string query = "SELECT * FROM [WalletSystem].[dbo].[TransactionHistory] WHERE AccountNumber ='" + AccountNumber + "' ORDER BY DateOfTransaction DESC";
            ;
            return query;
        }

        public static string GetLoginDetails(string LoginName, string Password)
        {
            string query = "SELECT * FROM [WalletSystem].[dbo].[Account] WHERE LoginName ='" + LoginName + "' AND Password ='" + Password + "'";
            ;
            return query;
        }


        public static string GetCurrentBalance(long AccountNumber)
        {
            string query = "SELECT TOP 1 * FROM [WalletSystem].[dbo].[Account] WHERE AccountNumber ='" + AccountNumber + "'";
            ;
            return query;
        }

        public static string GetExistingAccount(string LoginName)
        {
            string query = "SELECT TOP 1 * FROM [WalletSystem].[dbo].[Account] WHERE LoginName ='" + LoginName + "'";
            ;
            return query;
        }

        public static string DepositFunds()
        {
            string query = "UPDATE [WalletSystem].[dbo].[Account] SET Balance = @Balance WHERE AccountNumber = @AccountNumber";
            ;
            return query;
        }

    }
}
