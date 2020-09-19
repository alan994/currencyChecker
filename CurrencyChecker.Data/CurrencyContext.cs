using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyChecker.Data
{
	public class CurrencyContext : DbContext
	{
		public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
		{
		}

		public DbSet<Currency> Currencies { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Currency>().HasKey(x => x.Id);
			modelBuilder.Entity<Currency>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Currency>().Property(x => x.LastChanged).HasColumnType("Date");

		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
            this.ProcessAddedEntries();
            this.ProcessUpdatedEntries();
			return base.SaveChangesAsync(cancellationToken);
		}

        private void ProcessUpdatedEntries()
        {
            var changedEntries = this.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

            foreach (var entry in changedEntries)
            {
                if (entry.Entity is Currency item)
                {                    
                    item.UpdatedOn = DateTime.UtcNow;
                }
            }
        }

        private void ProcessAddedEntries()
        {
            var addedEntries = this.ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

            foreach (var entry in addedEntries)
            {
                if (entry.Entity is Currency item)
                {
                    item.UpdatedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
