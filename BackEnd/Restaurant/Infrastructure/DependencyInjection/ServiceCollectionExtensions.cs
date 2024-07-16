using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.IRestaurant;
using Domain.Interfaces.ITable;
using Domain.Interfaces.IUser;
using Domain.Interfaces.IUserCredentials;
using Infrastructure.Database;
using Infrastructure.Helper;
using Infrastructure.Queries.Table;
using Infrastructure.Repositories.RestaurantRepository;
using Infrastructure.Repositories.TableRepository;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Repositories.UserCredentialsRepository;
using Infrastructure.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Application.Queries.Table.ReadAllRestaurantTables;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordService, PasswordService>();

            services.AddDatabase(configuration);
            services.AddQueries();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<ITableRepository, TableRepository>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DatabaseConnection"),
                b => b.MigrationsAssembly("Infrastructure")
            ));

            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddScoped<IReadAllRestaurantTablesQuery, ReadAllRestaurantTablesQuery>();

            return services;
        }
    }
}