namespace WalletSystem.Entities.ViewModels.Accounts
{
    public class TransactionHistoryData
    {
        public string? TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public decimal EndBalance { get; set; }
    }
}
