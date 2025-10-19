using Microsoft.EntityFrameworkCore;
using CurrencyDashboard.Models;

namespace CurrencyDashboard.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}
