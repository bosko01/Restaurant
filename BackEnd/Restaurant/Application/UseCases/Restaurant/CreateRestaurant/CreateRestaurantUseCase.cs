﻿using Domain.Interfaces;
using Domain.Interfaces.IRestaurant;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Restaurant.CreateRestaurant
{
    public static class CreateRestaurantUseCase
    {
        public class Request : IRequest<Response>
        {
            public string Name { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty;

            public string Location { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;

            public string Number { get; set; } = string.Empty;

            public TimeOnly WorkingHoursFrom { get; set; }

            public TimeOnly WorkingHoursTo { get; set; }
        }

        public class Response
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty;

            public string Location { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;

            public string Number { get; set; } = string.Empty;

            public TimeOnly WorkingHoursFrom { get; set; }

            public TimeOnly WorkingHoursTo { get; set; }
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IRestaurantRepository _restaurantRepository;
            private IUnitOfWork _unitOfWork;

            public UseCase(IRestaurantRepository restaurantRepository, IUnitOfWork unitOfWork)
            {
                _restaurantRepository = restaurantRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                Domain.Models.Restaurant restaurant = Domain.Models.Restaurant.Create(request.Name, request.Description, request.Location, Email.Create(request.Email).ToString(), request.CountryCode, request.Number, request.WorkingHoursFrom, request.WorkingHoursTo);

                await _restaurantRepository.CreateNewAsync(restaurant);

                await _unitOfWork.SaveChangesAsync();

                return new Response
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    Location = restaurant.Location,
                    Email = restaurant.Email.ToString(),
                    CountryCode = restaurant.Phone.CountryCode,
                    Number = restaurant.Phone.Number,
                    WorkingHoursFrom = restaurant.WorkingHoursFrom,
                    WorkingHoursTo = restaurant.WorkingHoursTo
                };
            }
        }
    }
}