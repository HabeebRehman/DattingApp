
using API.Data;
using API.Interface;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,IConfiguration config)
        {
          
            services.AddScoped<ITokenService,TokenService>();
            services.AddDbContext<DataContext>(optios=>{
                optios.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}