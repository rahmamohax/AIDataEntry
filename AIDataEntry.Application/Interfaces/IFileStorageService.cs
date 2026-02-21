using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDataEntry.Application.Interfaces
{
    public interface IFileStorageService
    {
        public Task<string> SaveFileAsync(Stream fileStream, string fileName);
        Task DeleteFileAsync(string filePath);
    }
}
