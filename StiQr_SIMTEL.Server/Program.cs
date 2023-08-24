
namespace StiQr_SIMTEL.Server
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using StiQr_SIMTEL.Server.Context;
    using StiQr_SIMTEL.Server.Data;
    using StiQr_SIMTEL.Server.Services;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("SQLDB");

            // Add services to the container.
            builder.Services.AddDbContext<StiQrDbContext>(option => option.UseSqlServer(connectionString));
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<StiQrDbContext>();

            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "API StiQR",
                        Version = "v1",
                        Description ="API Description"

                    });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Authorization header using the Bearer scheme. Example \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                swagger.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema,Array.Empty<string>()}
                } );
            });


            builder.Services.AddAuthentication(f =>
            {
                f.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(k =>
            {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                k.SaveToken = true;
                k.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddScoped<ILabelQrService, LabelQrService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();

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
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}