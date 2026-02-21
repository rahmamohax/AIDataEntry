using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDataEntry.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DocumentStatus DocumentStatus { get; set; } = DocumentStatus.Uploaded;
        public DateTime UploadedAt { get; set; }

        private readonly List<ExtractedField> _extractedFields = new();
        public IReadOnlyCollection<ExtractedField> ExtractedFields => _extractedFields;
        public bool isValid { get; set; } = false;

        public Document() { }

        public Document(string name, string path)
        {
            Id = Guid.NewGuid();
            FileName = name;
            FilePath = path;
            UploadedAt = DateTime.Now;
        }

        public void MarkExtracted()
        {
            if (DocumentStatus != DocumentStatus.Uploaded)
                throw new InvalidOperationException($"Cannot move to PendingReview from {DocumentStatus}");

            DocumentStatus = DocumentStatus.PendingReview;
        }
        public void Confirm()
        {
            if (_extractedFields.Any(f => !f.IsConfirmed))
                throw new InvalidOperationException("All fields must be confirmed.");

            DocumentStatus = DocumentStatus.Confirmed;
            isValid = true;
        }
    }
}
