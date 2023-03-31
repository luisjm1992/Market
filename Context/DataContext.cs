using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
    }
}