using Application.Interfaces;
using Common.Infrastructure;
using Domain.Interfaces;
using Domain.Interfaces.IReservation;
using Domain.Interfaces.IRestaurant;
using Domain.Interfaces.ITable;
using Domain.Interfaces.IUser;
using Domain.Interfaces.IUserCredentials;
using Infrastructure.Database;
using Infrastructure.Helper;
using Infrastructure.Queries.RestaurantQueries;
using Infrastructure.Queries.Table;
using Infrastructure.Queries.User;
using Infrastructure.Repositories.ReservationRepository;
using Infrastructure.Repositories.RestaurantRepository;
using Infrastructure.Repositories.TableRepository;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Repositories.UserCredentialsRepository;
using Infrastructure.Repositories.UserRepository;
using Infrastructure.Services;
using Infrastructure.Services.FileManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Application.Queries.Restaurant.ReadAllRestaurants;
using static Application.Queries.Table.ReadAllRestaurantTables;
using static Application.Queries.UserQueries.ReadUsersPaginated;

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
            services.BindConfiguration(configuration);

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IRouteGenerator, RouteGenerator>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

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
            services.AddScoped<IReadUsersPaginatedQuery, ReadUsersPaginatedQuery>();
            services.AddScoped<IReadAllRestaurantsQuery, ReadAllRestaurantQuery>();
            services.AddScoped<IFileManager, FileManager>();

            return services;
        }

        private static IServiceCollection BindConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var fileStorageRoot = new FileStorageLocations();

            configuration.GetSection(FileStorageLocations.ConfigurationSettingName).Bind(fileStorageRoot);

            services.Configure<FileStorageLocations>(options =>
            {
                options.Root = fileStorageRoot.Root;
                options.RestaurantsFolder = fileStorageRoot.RestaurantsFolder;
                options.UsersFolder = fileStorageRoot.UsersFolder;
                options.UserDefaultPhoto = fileStorageRoot.UserDefaultPhoto;
            });

            return services;
        }
    }
}