using Api.Data.DTOs.ComonDto;
using Api.Data.DTOs.UserDTOs;
using Application.UseCases.Users.CreateUser;
using Application.UseCases.Users.UpdateUser;
using Domain.Models;
using Domain.ValueObjects;
using Mapster;

namespace Api.Data.Mapster
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<CreateUserDto, User>.NewConfig()
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName)
            .Map(dest => dest.Phone, src => src.PhoneNumber.Adapt<PhoneNumber>());

            TypeAdapterConfig<PhoneNumberRequestDto, PhoneNumber>.NewConfig()
                .ConstructUsing(src => PhoneNumber.Create(src.CountryCode, src.Number));

            TypeAdapterConfig<CreateUserDto, CreateUserUseCase.Request>.NewConfig()
                .ConstructUsing(src => new CreateUserUseCase.Request(
                    src.FirstName,
                    src.LastName,
                    src.Email,
                    src.Password,
                    src.PhoneNumber.Adapt<PhoneNumber>()
                    ));

            TypeAdapterConfig<UpdateUserUseCase.Response, ReadUserDto>.NewConfig()
                .ConstructUsing(src => new ReadUserDto
                {
                    Id = src.Id,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    Phone = PhoneNumber.Create(src.CountryCode, src.Number).ToString()
                });

            TypeAdapterConfig<CreateUserUseCase.Response, ReadUserDto>.NewConfig()
                .ConstructUsing(src => new ReadUserDto
                {
                    Id = src.Id,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    Phone = PhoneNumber.Create(src.CountryCode, src.Number).ToString()
                });

            TypeAdapterConfig<CreateAdminUseCase.Response, ReadUserDto>.NewConfig()
                .ConstructUsing(src => new ReadUserDto
                {
                    Id = src.Id,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    Phone = PhoneNumber.Create(src.CountryCode, src.Number).ToString()
                });

            TypeAdapterConfig<CreateUserDto, CreateAdminUseCase.Request>.NewConfig()
                .ConstructUsing(src => new CreateAdminUseCase.Request(
                    src.FirstName,
                    src.LastName,
                    src.Email,
                    src.Password,
                    PhoneNumber.Create(src.PhoneNumber.CountryCode, src.PhoneNumber.Number)
                    ));

            TypeAdapterConfig<CreateManagerUseCase.Response, ReadUserDto>.NewConfig()
                .ConstructUsing(src => new ReadUserDto
                {
                    Id = src.Id,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    Phone = PhoneNumber.Create(src.CountryCode, src.Number).ToString()
                });

            TypeAdapterConfig<CreateUserDto, CreateManagerUseCase.Request>.NewConfig()
                .ConstructUsing(src => new CreateManagerUseCase.Request(
                    src.FirstName,
                    src.LastName,
                    src.Email,
                    src.Password,
                    PhoneNumber.Create(src.PhoneNumber.CountryCode, src.PhoneNumber.Number)
                    ));
        }
    }
}