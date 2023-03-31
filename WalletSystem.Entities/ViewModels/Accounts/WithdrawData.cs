using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletSystem.Entities.ViewModels.Accounts
{
    public class WithdrawData
    {
        public long AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
