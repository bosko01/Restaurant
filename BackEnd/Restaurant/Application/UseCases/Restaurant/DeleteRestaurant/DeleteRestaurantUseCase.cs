using Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.IRestaurant;
using MediatR;

namespace Application.UseCases.Restaurant.DeleteRestaurant
{
    public static class DeleteRestaurantUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class Response
        {
            public Guid Id { get; set; }
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
                var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

                if (restaurant is null)
                {
                    throw new EntityNotFoundException("User not found");
                }

                await _restaurantRepository.DeleteAsync(restaurant);

                await _unitOfWork.SaveChangesAsync();

                return new Response
                {
                    Id = request.Id
                };
            }
        }
    }
}