using Market.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Market.Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<Operation>()
             .Property(o => o.TpOperation)
             .HasConversion(
                v => v.ToString(),
                v => (TipoOperacion)Enum.Parse(typeof(TipoOperacion), v)

        )
        .HasColumnName("TpOperationString");
        }

        public DbSet<Sentimiento> Sentimientos { get; set; }
        public DbSet<Mercado> Mercados { get; set; }
        public DbSet<Operation> Operations { get; set; }
         public DbSet<Cuenta> Cuentas { get; set; }
    }
}