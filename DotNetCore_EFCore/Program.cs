using DotNetCore_EFCore_CQRS.Commands;
using DotNetCore_EFCore_CQRS.Repositories;
using DotNetCore_EFCore_CQRS.Services;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore_EFCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<AppDBContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies().EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine));
            builder.Services.AddScoped<IEmployeeCommands, EmployeeCommands>();
            builder.Services.AddScoped<IEmployeeCommandRepositoriesService, EmployeeCommandRepositoriesService>();

            builder.Services.AddScoped<IEmployeeQuery, EmployeeQuery>();
            builder.Services.AddScoped<IEmployeeQueryRepositoriesService, EmployeeQueryRepositoriesService>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Ef core API");
                c.RoutePrefix = "swagger";
            });
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
