using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoWallet_3.Model
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext() : base("DBConnection")
        {

        }

        public DbSet<CurrenciesRate> CurrenciesRate { get; set; }
        public DbSet<UserCurrencies> UserCurrencies { get; set; }
        public DbSet<Acces> Access { get; set; }
        public DbSet<Currencies> Currencies { get; set; }
        public DbSet<Keys> Keys { get; set; }
        public DbSet<Operation> Operation { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Users> Users { get; set; }
    }    
}
