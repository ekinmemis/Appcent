using Appcent.Core.Domain;
using Appcent.Data.Mapping.ApplicationUsers;
using Appcent.Data.Mapping.Jobs;

using Microsoft.EntityFrameworkCore;

namespace Appcent.Data
{
    public class EfDataContext : DbContext
    {
        public EfDataContext(DbContextOptions<EfDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserMap());
            modelBuilder.ApplyConfiguration(new JobMap());

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<Job> Job { get; set; }
    }
}