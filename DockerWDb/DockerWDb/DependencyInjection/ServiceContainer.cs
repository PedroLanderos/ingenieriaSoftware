using DockerWDb.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DockerWDb.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddServices<TContext>(this IServiceCollection services, IConfiguration config) where TContext : DbContext
        {
            //agregar el string para la conexion a la base de datos
            services.AddDbContext<TContext>(option => option.UseSqlServer(
               config.GetConnectionString("dbconnection"), sqlServerOption => sqlServerOption.EnableRetryOnFailure()));


            return services;
        }

        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            //add the database string 
            AddServices<AppDbContext>(services, config);
            //implements the interface and the repository to use the controller in cors 
            //services.AddScoped<IUser, AuthenticationRepository>();

            return services;
        }
    }
}
