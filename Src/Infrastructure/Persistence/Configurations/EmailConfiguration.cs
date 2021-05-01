using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasKey(c => c.EmailId);

            builder.Property(c => c.To).IsRequired();
            builder.Property(c => c.From).IsRequired();
            builder.Property(c => c.Message).IsRequired();
        }
    }
}
