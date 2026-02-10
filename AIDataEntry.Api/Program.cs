
using AIDataEntry.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AIDataEntry.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });

            #endregion

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
