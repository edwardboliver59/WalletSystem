namespace WalletSystem.Entities.ViewModels.Accounts
{
    public class LoginDetailsData
    {
        public string? LoginName { get; set; }
        public long AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
