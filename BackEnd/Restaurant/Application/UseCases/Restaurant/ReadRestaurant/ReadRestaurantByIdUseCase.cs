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

            public string Email { get; set; } = string.Empty;
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
                var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

                if (restaurant is null)
                {
                    throw new EntityNotFoundException("User not found");
                }

                return new Response
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Email = restaurant.Email.ToString()
                };
            }
        }
    }
}