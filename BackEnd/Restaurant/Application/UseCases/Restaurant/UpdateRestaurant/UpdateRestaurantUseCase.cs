using Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.IRestaurant;
using MediatR;

namespace Application.UseCases.Restaurant.UpdateRestaurant
{
    public static class UpdateRestaurantUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty;

            public string Location { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;

            public string PhoneNumber { get; set; } = string.Empty;

            public string Menu { get; set; } = string.Empty;
        }

        public class Response
        {
            public string Name { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty;

            public string Location { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;

            public string PhoneNumber { get; set; } = string.Empty;

            public string Menu { get; set; } = string.Empty;
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
                var restaurantUpdate = await _restaurantRepository.GetByIdAsync(request.Id);

                if (restaurantUpdate is null)
                {
                    throw new EntityNotFoundException("Restaurant not found");
                }

                restaurantUpdate.Update(
                    request.Name,
                    request.Description,
                    request.Location,
                    request.Email,
                    request.CountryCode,
                    request.PhoneNumber,
                    request.Menu
                    );

                _unitOfWork.SaveChangesAsync();

                return new Response
                {
                    Name = restaurantUpdate.Name,
                    Description = restaurantUpdate.Description,
                    Location = restaurantUpdate.Location,
                    Email = restaurantUpdate.Email.ToString(),
                    CountryCode = restaurantUpdate.Phone.CountryCode,
                    PhoneNumber = restaurantUpdate.Phone.Number,
                    Menu = restaurantUpdate.Menu
                };
            }
        }
    }
}