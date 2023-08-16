
namespace StiQr_SIMTEL.Server
{

    using Microsoft.EntityFrameworkCore;
    using StiQr_SIMTEL.Server.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StiqrDbContext>(opciones =>
            {
                opciones.UseSqlServer(builder.Configuration.GetConnectionString("SQLString"));
            });

            builder.Services.AddCors(opciones =>
            {
                opciones.AddPolicy("newPolicy", app =>
                {
                    app.AllowAnyOrigin();
                    app.AllowAnyHeader();
                    app.AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("newPolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}