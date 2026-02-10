using AIDataEntry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AIDataEntry.Infrastructure.Data.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<ExtractedField> ExtractedFields { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
