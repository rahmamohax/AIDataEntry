using AIDataEntry.Application.Interfaces;
using AIDataEntry.Domain.Entities;
using AIDataEntry.Infrastructure.Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using System.Reflection.Metadata;

namespace AIDataEntry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly string[] _allowedExtentions = { ".pdf", ".png", ".jpg", ".csv" };
        private readonly IFileStorageService _storageService;
        private readonly AppDbContext _dbContext;
        private const long maxSize = 10 * 1024 * 1024; //10 MB

        public DocumentsController(IFileStorageService storageService, AppDbContext dbContext)
        {
            this._storageService = storageService;
            this._dbContext = dbContext;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            if (file.Length > maxSize)
                return BadRequest("File size exceeds 10MB limit.");

            var extention = Path.GetExtension(file.FileName).ToLowerInvariant();
            if(!_allowedExtentions.Contains(extention)) 
                return BadRequest("Invalid file type. Allowed: .pdf, .jpg, .png, .csv");

            // 2. Save File to Disk via Infrastructure Service
            string savedPath;
            try
            {
                using var stream = file.OpenReadStream();
                savedPath = await _storageService.SaveFileAsync(stream, file.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal storage error: {ex.Message}");
            }

            var document = new Document(file.FileName, savedPath);
            _dbContext.Documents.Add(document);
            await _dbContext.SaveChangesAsync();

            return Ok(new { documentId = document.Id, status = document.DocumentStatus.ToString() });

        }
    }
}
