using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDataEntry.Domain.Entities
{
    public class ExtractedField
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string? FieldValue { get; set; } = string.Empty;
        public float Confidence { get; set; }
        public bool IsConfirmed { get; set; }

        public ExtractedField() { }
        public ExtractedField(Guid docId, string name, string? value, float confidence)
        {
            if (confidence < 0 || confidence > 1)
                throw new IndexOutOfRangeException();

            Id = Guid.NewGuid();
            DocumentId = docId;
            FieldName = name;
            FieldValue = value ?? null;
            Confidence = confidence;
            IsConfirmed = false;
        }

        public void Confirm(string value)
        {
            FieldValue = value;
            IsConfirmed = true;
        }
    }
}
