using GeneticAlgorithmSchedule.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Database.Contexts.Shared
{
    public class BaseDbContext<T> : DbContext where T : DbContext
    {
        public BaseDbContext(DbContextOptions<T> option) : base(option)
        { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.DetectChanges();
            var now = DateTime.UtcNow;

            foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added))
            {
                item.Property("AddedDate").CurrentValue = now;
                item.Property("ModifiedDate").CurrentValue = now;
            }

            foreach (var item in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Modified))
            {
                item.Property("ModifiedDate").CurrentValue = now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
