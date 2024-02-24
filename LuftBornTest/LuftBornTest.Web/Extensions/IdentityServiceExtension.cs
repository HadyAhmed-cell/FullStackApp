using LuftBornTest.Core.Entities;
using LuftBornTest.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LuftBornTest.Web.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.SignIn.RequireConfirmedAccount = false;
            })

                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ValidateLifetime = true,
                    };
                });
            return services;
        }
    }
}