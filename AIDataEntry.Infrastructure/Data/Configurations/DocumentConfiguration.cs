using AIDataEntry.Domain.Entities;
using AIDataEntry.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDataEntry.Infrastructure.Data.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(d => d.FileName)
              .IsRequired()
              .HasMaxLength(255);

            builder.Property(d => d.FilePath)
              .IsRequired()
              .HasMaxLength(500);

            builder.Property(d => d.DocumentStatus)
              .IsRequired()
              .HasConversion<string>();

            builder.HasMany<ExtractedField>("_extractedFields")
                .WithOne()
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation("_extractedFields")
                  .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
