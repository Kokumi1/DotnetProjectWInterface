using BackEnd.Entities;
using BackEnd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace BackEnd.Datas
{
    public class BackEndDbContext : DbContext
    {
        public BackEndDbContext() : base()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBBackEnd;");
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Anomaly> Annomalies { get; set; }
    }
}
