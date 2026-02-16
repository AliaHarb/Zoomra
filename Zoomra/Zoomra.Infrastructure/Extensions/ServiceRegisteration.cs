using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Zoomra.Application.Helpers; 
using Zoomra.Application.Interfaces;
using Zoomra.Application.Profiles;
using Zoomra.Application.Services;
using Zoomra.Application.Profiles;
using Zoomra.Domain.Entities;
using Zoomra.Domain.Interfaces;
using Zoomra.Infrastructure.Data;
using Zoomra.Infrastructure.Repositories;


namespace Zoomra.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. الداتا بيز (خدنا بالنا إن اسمها ApplicationDbContext)
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                }
               ));

            // 2. إعدادات اليوزر (Identity)
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 8;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // 3. إعدادات الـ JWT التوكن
            var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key ?? ""))
                };
            });

            // 4. حقن الاعتماديات (Dependency Injection)
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>(); 
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IEmergencyService, EmergencyService>();
            services.AddScoped<IDonorService, DonorService>();

            // 5. إعدادات عامة
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddAutoMapper(typeof(AuthProfile).Assembly);

            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddMemoryCache();


            return services;
        }
    }
}