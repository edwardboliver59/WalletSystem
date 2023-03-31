namespace WalletSystem.Entities.ViewModels.Accounts
{
    public class TransferFundsData
    {
        public long FromAccountNumber { get; set; }
        public long ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
