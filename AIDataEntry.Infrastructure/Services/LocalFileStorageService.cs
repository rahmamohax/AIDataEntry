using AIDataEntry.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Runtime.Serialization.Formatters;

namespace AIDataEntry.Infrastructure.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        //private readonly IConfiguration _configuration;
        private readonly string _basePath;

        public LocalFileStorageService(IConfiguration configuration)
        {
            var folderName = configuration["FileStorage:BaseFolder"] ?? "Uploads";
            _basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", folderName);
        }

        public Task DeleteFileAsync(string filePath)
        {
            if(File.Exists(filePath)) 
                File.Delete(filePath);
            return Task.CompletedTask;
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                if (!Directory.Exists(_basePath))
                    Directory.CreateDirectory(_basePath);

                var extension = Path.GetExtension(fileName);
                var uniqueFileName = $"{Guid.NewGuid()}.{extension}";
                var fullPath = Path.Combine(_basePath, uniqueFileName);

                using (var destinationStream = new FileStream(fullPath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(destinationStream);
                }
                return fullPath;
            }
            catch (IOException ex)
            {
                throw new Exception($"Failed to save file to local storage: {ex.Message}", ex);
            }
        }
    }
}
