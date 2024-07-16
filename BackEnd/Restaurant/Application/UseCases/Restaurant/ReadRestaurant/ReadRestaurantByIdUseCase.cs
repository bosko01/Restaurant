using Common.Exceptions;
using Domain.Interfaces.IRestaurant;
using MediatR;

namespace Application.UseCases.Restaurant.ReadRestaurant
{
    public static class ReadRestaurantByIdUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
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

            public string Menu { get; set; } = string.Empty;

            public TimeOnly WorkingHoursFrom { get; set; }

            public TimeOnly WorkingHoursTo { get; set; }
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IRestaurantRepository _restaurantRepository;

            public UseCase(IRestaurantRepository restaurantRepository)
            {
                _restaurantRepository = restaurantRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                Domain.Models.Restaurant? restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

                if (restaurant is null)
                {
                    throw new EntityNotFoundException("User not found");
                }

                return new Response
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    Location = restaurant.Location,
                    Email = restaurant.Email.ToString(),
                    CountryCode = restaurant.Phone.CountryCode,
                    Number = restaurant.Phone.Number,
                    Menu = restaurant.Menu,
                    WorkingHoursFrom = restaurant.WorkingHoursFrom,
                    WorkingHoursTo = restaurant.WorkingHoursTo
                };
            }
        }
    }
}