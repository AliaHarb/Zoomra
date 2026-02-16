
using Microsoft.AspNetCore.Identity;
using Zoomra.Domain.Entities;
using Zoomra.Infrastructure.Extensions;

namespace Zoomra.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            
            builder.Services.AddInfrastructureServices(builder.Configuration);

            var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // --- Data Seeding 
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    await Zoomra.Infrastructure.Data.DataSeeder.SeedRolesAsync(roleManager);
                    await Zoomra.Infrastructure.Data.DataSeeder.SeedAdminAsync(userManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
           
            app.Run();

            
        }
    }
}
