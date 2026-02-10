using AIDataEntry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDataEntry.Infrastructure.Data.Configurations
{
    public class ExtractedFieldConfigurayion : IEntityTypeConfiguration<ExtractedField>
    {
        public void Configure(EntityTypeBuilder<ExtractedField> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.FieldName).HasMaxLength(255);

            builder.Property(f => f.FieldValue).HasMaxLength(500);

        }
    }
}
