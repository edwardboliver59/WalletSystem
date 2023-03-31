using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletSystem.BusinessLogic.Constants.ConnectionString
{
    public static class Connection
    {
        public static string GetConnection() 
        {
            string LocalConnection = "Data Source=.;Database=WalletSystem;Trusted_Connection=True;MultipleActiveResultSets=true";
            return LocalConnection;
        }
    }
}
