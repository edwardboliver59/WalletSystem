using Microsoft.EntityFrameworkCore;

namespace WalletSystem.Data.DataContexts
{
    public class WalletSystemDbContext : DbContext
    {
        public WalletSystemDbContext(DbContextOptions options)
         : base(options)
        {
        }
    }
}
