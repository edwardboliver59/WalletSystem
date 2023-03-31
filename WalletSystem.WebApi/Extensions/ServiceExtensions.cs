using WalletSystem.BusinessLogic.Services;
using WalletSystem.Contracts.Accounts;

namespace WalletSystem.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionsService, TransactionsService>();
        }
    }
}
