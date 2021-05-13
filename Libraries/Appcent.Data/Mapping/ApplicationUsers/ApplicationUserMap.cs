using Appcent.Core.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appcent.Data.Mapping.ApplicationUsers
{
    public partial class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(nameof(ApplicationUser));

            builder.HasKey(applicationUser => applicationUser.Id);

            builder.Property(applicationUser => applicationUser.Username).HasMaxLength(1000);
        }
    }
}