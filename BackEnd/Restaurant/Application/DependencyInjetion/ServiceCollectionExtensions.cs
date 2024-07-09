using Application.UseCases.Users.CreateUser;
using Application.UseCases.Users.DeleteUser;
using Application.UseCases.Users.ReadUser;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjetion
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(ReadUserUseCase.UseCase).Assembly,
                typeof(CreateUserUseCase.UseCase).Assembly,
                typeof(ReadUserByIdUseCase.UseCase).Assembly,
                typeof(DeleteUserUseCase.UseCase).Assembly
                ));

            return services;
        }
    }
}