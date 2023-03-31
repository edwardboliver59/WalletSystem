
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WalletSystem.Contracts.Accounts;
using WalletSystem.Entities.ViewModels.Accounts;

namespace WalletSystem.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
      
        public JsonResult RegisterAccount(RegisterData value)
        {
            try
            {
                using SqlConnection connection = new(Constants.ConnectionString.Connection.GetConnection());
                connection.Open();


                //Get GetExisting Account
                SqlCommand command = connection.CreateCommand();
                string ExistingLoginName = "";
                string CurrentBalanceQuery = string.Format(Constants.SqlQuery.Query.GetExistingAccount(value.LoginName ?? ""));
                command.CommandText = CurrentBalanceQuery;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ExistingLoginName = reader.IsDBNull(0) ? "" : reader.GetString(0);
                }
                if (ExistingLoginName == value.LoginName)
                {
                    return new JsonResult("Login Name Already Exist!");
                }

                //Add Account
                string AddAccount = string.Format(Constants.SqlQuery.Query.AddAccountQuery());

                var generateAccount = GenerateAccount();

                using SqlCommand AddAccountCmd = new(AddAccount, connection);
                AddAccountCmd.Parameters.AddWithValue("@LoginName", value.LoginName);
                AddAccountCmd.Parameters.AddWithValue("@AccountNumber", generateAccount);
                AddAccountCmd.Parameters.AddWithValue("@Password", value.Password);
                AddAccountCmd.Parameters.AddWithValue("@Balance", 0);
                AddAccountCmd.Parameters.AddWithValue("@RegisterDate", DateTime.Now);

                //AddAccountCreation History
                string AddAccountHistory = string.Format(Constants.SqlQuery.Query.AddAccountHistoryQuery());
                using SqlCommand addHistoryCmd = new(AddAccountHistory, connection);
                addHistoryCmd.Parameters.AddWithValue("@TransactionType", "AccountCreation");
                addHistoryCmd.Parameters.AddWithValue("@Amount", 0);
                addHistoryCmd.Parameters.AddWithValue("@AccountNumber", generateAccount);
                addHistoryCmd.Parameters.AddWithValue("@DateOfTransaction", DateTime.Now);
                addHistoryCmd.Parameters.AddWithValue("@EndBalance", 0);


                AddAccountCmd.ExecuteNonQuery();
                addHistoryCmd.ExecuteNonQuery();
                connection.Dispose();
                connection.Close();


                return new JsonResult("Acount Successfully Added!");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult Login(string LoginName, string Password)
        {
            try
            {
                SqlConnection con = new(Constants.ConnectionString.Connection.GetConnection());
                con.Open();
                SqlCommand command = con.CreateCommand();

                var LoginDetailsList = new List<LoginDetailsData>();

                string sql = string.Format(Constants.SqlQuery.Query.GetLoginDetails(LoginName, Password));
                command.CommandText = sql;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var LoginDetails = new LoginDetailsData
                    {
                        LoginName = reader.IsDBNull(0) ? "" : reader.GetString(0),
                        AccountNumber = reader.IsDBNull(1) ? 0 : reader.GetInt64(1),
                        Balance = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                        RegisterDate = reader.IsDBNull(4) ? new DateTime() : reader.GetDateTime(4)
                    };

                    LoginDetailsList.Add(LoginDetails);
                }
                command.Dispose();
                con.Close();
                return new JsonResult(LoginDetailsList);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public long GenerateAccount()
        {
            var generatedAccountNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

            generatedAccountNumber.Substring(0, 11);

            return long.Parse(generatedAccountNumber);
        }
    }
}
