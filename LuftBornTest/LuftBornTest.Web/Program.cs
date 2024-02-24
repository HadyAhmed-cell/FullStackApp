using LuftBornTest.Core.Entities;
using LuftBornTest.Core.Interfaces;
using LuftBornTest.Infrastructure.Data;
using LuftBornTest.Infrastructure.Services;
using LuftBornTest.Web.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace LuftBornTest.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerDocuentation();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration
            .GetConnectionString("FullStackConnectionString"), b => b.MigrationsAssembly("LuftBornTest.Web")));
            builder.Services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(builder.Configuration
            .GetConnectionString("IdentityConnection"), b => b.MigrationsAssembly("LuftBornTest.Web")));

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
            //builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>()
            //    .AddDefaultTokenProviders();
            builder.Services.AddIdentityServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            app.UseAuthorization();
            ApplyMigration();

            app.MapControllers();

            app.Run();

            void ApplyMigration()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    if (_db.Database.GetPendingMigrations().Count() > 0)
                    {
                        _db.Database.Migrate();
                    }
                }
            }
        }
    }
}