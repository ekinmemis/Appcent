using Appcent.Core.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appcent.Data.Mapping.Jobs
{
    public partial class JobMap : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable(nameof(Job));

            builder.HasKey(job => job.Id);

            builder.Property(job => job.Description).HasMaxLength(255).IsRequired();
        }
    }
}