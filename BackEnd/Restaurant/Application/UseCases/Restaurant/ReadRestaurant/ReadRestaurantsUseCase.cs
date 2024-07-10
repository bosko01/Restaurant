using Common.Exceptions;
using Domain.Interfaces.IRestaurant;
using Mapster;
using MediatR;

namespace Application.UseCases.Restaurant.ReadRestaurant
{
    public static class ReadRestaurantsUseCase
    {
        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<RestaurantResponse> Restaurants { get; set; } = new();

            public Response(List<RestaurantResponse> res)
            {
                Restaurants = res;
            }
        }

        public class RestaurantResponse
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
                var restaurants = await _restaurantRepository.GetAllAsync();

                if (restaurants is null)
                {
                    throw new EntityNotFoundException("Restaurants not found");
                }

                var returnValue = restaurants.Adapt<List<RestaurantResponse>>();

                return new Response(returnValue);
            }
        }
    }
}