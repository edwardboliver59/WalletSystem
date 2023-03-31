using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WalletSystem.Contracts.Accounts;
using WalletSystem.Entities.ViewModels.Accounts;

namespace WalletSystem.BusinessLogic.Services
{
    public class TransactionsService : ITransactionsService
    {


        public JsonResult Deposit(DepositData value)
        {
            try
            {
                using SqlConnection connection = new(Constants.ConnectionString.Connection.GetConnection());
                connection.Open();

                //Get Account Current Balance
                SqlCommand GetCurrentBalanceCmd = connection.CreateCommand();
                decimal CurrentBalance = 0;
                string CurrentBalanceQuery = string.Format(Constants.SqlQuery.Query.GetCurrentBalance(value.AccountNumber));
                GetCurrentBalanceCmd.CommandText = CurrentBalanceQuery;
                SqlDataReader reader = GetCurrentBalanceCmd.ExecuteReader();
                while (reader.Read())
                {
                    CurrentBalance = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                }

                //Update Current Balance
                string TransferFunds = string.Format(Constants.SqlQuery.Query.DepositFunds());
                using SqlCommand TransferFundsCmd = new(TransferFunds, connection);
                TransferFundsCmd.Parameters.AddWithValue("@Balance", CurrentBalance + value.Amount);
                TransferFundsCmd.Parameters.AddWithValue("@AccountNumber", value.AccountNumber);
                TransferFundsCmd.ExecuteNonQuery();


                //Add Diposit History
                string AddDepositHistory = string.Format(Constants.SqlQuery.Query.AddTransferHistoryQuery());
                using SqlCommand AddDepositHistoryCmd = new(AddDepositHistory, connection);
                AddDepositHistoryCmd.Parameters.AddWithValue("@TransactionType", "Deposit");
                AddDepositHistoryCmd.Parameters.AddWithValue("@Amount", value.Amount);
                AddDepositHistoryCmd.Parameters.AddWithValue("@AccountNumber", value.AccountNumber);
                AddDepositHistoryCmd.Parameters.AddWithValue("@DateOfTransaction", DateTime.Now);
                AddDepositHistoryCmd.Parameters.AddWithValue("@EndBalance", CurrentBalance + value.Amount);
                AddDepositHistoryCmd.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();

                

                return new JsonResult("Deposit Funds Successfully");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult Withdraw(WithdrawData value)
        {
            try
            {
                using SqlConnection connection = new(Constants.ConnectionString.Connection.GetConnection());
                connection.Open();

                //Get Account Current Balance
                SqlCommand GetCurrentBalanceCmd = connection.CreateCommand();
                decimal CurrentBalance = 0;
                string CurrentBalanceQuery = string.Format(Constants.SqlQuery.Query.GetCurrentBalance(value.AccountNumber));
                GetCurrentBalanceCmd.CommandText = CurrentBalanceQuery;
                SqlDataReader reader = GetCurrentBalanceCmd.ExecuteReader();
                while (reader.Read())
                {
                    CurrentBalance = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                }

                //Update Current Balance
                string TransferFunds = string.Format(Constants.SqlQuery.Query.DepositFunds());
                using SqlCommand TransferFundsCmd = new(TransferFunds, connection);
                TransferFundsCmd.Parameters.AddWithValue("@Balance", CurrentBalance - value.Amount);
                TransferFundsCmd.Parameters.AddWithValue("@AccountNumber", value.AccountNumber);
                TransferFundsCmd.ExecuteNonQuery();


                //Add Withdraw History
                string AddDepositHistory = string.Format(Constants.SqlQuery.Query.AddTransferHistoryQuery());
                using SqlCommand AddDepositHistoryCmd = new(AddDepositHistory, connection);
                AddDepositHistoryCmd.Parameters.AddWithValue("@TransactionType", "Withdraw");
                AddDepositHistoryCmd.Parameters.AddWithValue("@Amount", value.Amount);
                AddDepositHistoryCmd.Parameters.AddWithValue("@AccountNumber", value.AccountNumber);
                AddDepositHistoryCmd.Parameters.AddWithValue("@DateOfTransaction", DateTime.Now);
                AddDepositHistoryCmd.Parameters.AddWithValue("@EndBalance", CurrentBalance - value.Amount);
                AddDepositHistoryCmd.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();



                return new JsonResult("Deposit Funds Successfully");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult TransferFunds(TransferFundsData value)
        {
            try
            {
                using SqlConnection connection = new(Constants.ConnectionString.Connection.GetConnection());
                connection.Open();

                //Get Current Balance Of Sender
                SqlCommand GetSenderBalanceCmd = connection.CreateCommand();
                decimal SenderBalance = 0;
                string CurrentSenderBalanceQuery = string.Format(Constants.SqlQuery.Query.GetCurrentBalance(value.FromAccountNumber));
                GetSenderBalanceCmd.CommandText = CurrentSenderBalanceQuery;
                SqlDataReader readerSender = GetSenderBalanceCmd.ExecuteReader();
                while (readerSender.Read())
                {
                    SenderBalance = readerSender.IsDBNull(3) ? 0 : readerSender.GetDecimal(3);
                }
                if (SenderBalance < value.Amount)
                {
                    return new JsonResult("Insufficient Balance");
                }

                //Update Sender Balance
                string TransferFunds = string.Format(Constants.SqlQuery.Query.TransferFundsQuery());
                using SqlCommand TransferFundsCmd = new(TransferFunds, connection);
                TransferFundsCmd.Parameters.AddWithValue("@Balance", SenderBalance - value.Amount);
                TransferFundsCmd.Parameters.AddWithValue("@AccountNumber", value.FromAccountNumber);


                //Get Current Balance Of Receiver
                SqlCommand GetReceiverBalanceCmd = connection.CreateCommand();
                decimal ReceiverBalance = 0;
                string ReceiverBalanceQuery = string.Format(Constants.SqlQuery.Query.GetCurrentBalance(value.ToAccountNumber));
                GetReceiverBalanceCmd.CommandText = ReceiverBalanceQuery;
                SqlDataReader readerReceiver = GetReceiverBalanceCmd.ExecuteReader();
                while (readerReceiver.Read())
                {
                    ReceiverBalance = readerReceiver.IsDBNull(3) ? 0 : readerReceiver.GetDecimal(3);
                }

                //Update Receiver Balance
                string ReceivedFunds = string.Format(Constants.SqlQuery.Query.TransferFundsQuery());
                using SqlCommand ReceivedFundsCmd = new(ReceivedFunds, connection);
                ReceivedFundsCmd.Parameters.AddWithValue("@Balance", ReceiverBalance + value.Amount);
                ReceivedFundsCmd.Parameters.AddWithValue("@AccountNumber", value.ToAccountNumber);


                //Add FundTransfer History
                string AddTransferHistory = string.Format(Constants.SqlQuery.Query.AddTransferHistoryQuery());
                using SqlCommand AddTransferHistoryCmd = new(AddTransferHistory, connection);
                AddTransferHistoryCmd.Parameters.AddWithValue("@TransactionType", "FundTransfer");
                AddTransferHistoryCmd.Parameters.AddWithValue("@Amount", value.Amount);
                AddTransferHistoryCmd.Parameters.AddWithValue("@AccountNumber", value.FromAccountNumber);
                AddTransferHistoryCmd.Parameters.AddWithValue("@DateOfTransaction", DateTime.Now);
                AddTransferHistoryCmd.Parameters.AddWithValue("@EndBalance", SenderBalance - value.Amount);


                //Add Fund Received History
                string AddReceivedHistory = string.Format(Constants.SqlQuery.Query.AddReceivedHistoryQuery());
                using SqlCommand AddReceivedHistoryCmd = new(AddReceivedHistory, connection);
                AddReceivedHistoryCmd.Parameters.AddWithValue("@TransactionType", "FundReceived");
                AddReceivedHistoryCmd.Parameters.AddWithValue("@Amount", value.Amount);
                AddReceivedHistoryCmd.Parameters.AddWithValue("@AccountNumber", value.ToAccountNumber);
                AddReceivedHistoryCmd.Parameters.AddWithValue("@DateOfTransaction", DateTime.Now);
                AddReceivedHistoryCmd.Parameters.AddWithValue("@EndBalance", ReceiverBalance + value.Amount);

                TransferFundsCmd.ExecuteNonQuery();
                ReceivedFundsCmd.ExecuteNonQuery();
                AddTransferHistoryCmd.ExecuteNonQuery();
                AddReceivedHistoryCmd.ExecuteNonQuery();
                connection.Dispose();
                connection.Close();

                return new JsonResult("Transfer Funds Successfully");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult GetTransactionHistory(long AccountNumber)
        {
            try
            {
                SqlConnection connection = new(Constants.ConnectionString.Connection.GetConnection());
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                var TransactionHistoryList = new List<TransactionHistoryData>();

                string TransactionHistoryQuery = string.Format(Constants.SqlQuery.Query.GetTransactionHistoryQuery(AccountNumber));
                command.CommandText = TransactionHistoryQuery;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var TransactionHistory = new TransactionHistoryData
                    {
                        TransactionType = reader.IsDBNull(0) ? "" : reader.GetString(0),
                        Amount = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1),
                        DateOfTransaction = reader.IsDBNull(3) ? new DateTime() : reader.GetDateTime(3),
                        EndBalance = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4)
                    };

                    TransactionHistoryList.Add(TransactionHistory);
                }
                command.Dispose();
                connection.Close();
                return new JsonResult(TransactionHistoryList);
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
