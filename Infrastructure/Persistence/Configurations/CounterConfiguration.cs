using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CounterConfiguration : IEntityTypeConfiguration<Counter>
    {
        public void Configure(EntityTypeBuilder<Counter> builder)
        {
            builder.HasKey(c => c.CounterId);

            builder.Property(c => c.CounterId).ValueGeneratedNever();
            builder.Property(c => c.Count).IsRequired();
            builder.Property(c => c.Name).IsRequired();
        }
    }
}
