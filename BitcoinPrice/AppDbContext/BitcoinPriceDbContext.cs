using BitcoinPrice.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitcoinPrice.AppDbContext
{
    public class BitcoinPriceDbContext : DbContext
    {
        public BitcoinPriceDbContext(DbContextOptions<BitcoinPriceDbContext> options)
        : base(options)
        {
        }

        public DbSet<BitcoinPriceRate> BitcoinRates => Set<BitcoinPriceRate>();
    }
}
