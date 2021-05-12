using Appcent.Core.Domain;
using Appcent.Data.Mapping.ApplicationUsers;

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
            modelBuilder.ApplyConfiguration<ApplicationUser>(new ApplicationUserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}