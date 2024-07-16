using Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.IRestaurant;
using Domain.Interfaces.ITable;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Restaurant._Table.AddTable
{
    public static class AddTableUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid RestaurantId { get; set; }

            public int NumberOfSeats { get; set; }
        }

        public class Response
        {
            public Guid RestaurantId { get; set; }

            public Guid TableId { get; set; }

            public int NumberOfSeats { get; set; }
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private ITableRepository _tableRepository;
            private IRestaurantRepository _restaurantRepository;
            private IUnitOfWork _unitOfWork;

            public UseCase(ITableRepository tableRepository, IRestaurantRepository restaurantRepository, IUnitOfWork unitOfWork)
            {
                _restaurantRepository = restaurantRepository;
                _tableRepository = tableRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

                if (restaurant is null)
                {
                    throw new EntityNotFoundException("Restaurant not found");
                }

                var table = Table.Create(request.RestaurantId, request.NumberOfSeats);

                restaurant.AddTable(table);
                await _tableRepository.CreateNewAsync(table);

                _unitOfWork.SaveChangesAsync();

                return new Response
                {
                    RestaurantId = restaurant.Id,
                    TableId = table.Id,
                    NumberOfSeats = request.NumberOfSeats,
                };
            }
        }
    }
}