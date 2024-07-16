using Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.IRestaurant;
using Domain.Interfaces.ITable;
using MediatR;

namespace Application.UseCases.Restaurant._Table.RemoveTable
{
    public static class RemoveTableUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }

            public Guid RestaurantId { get; set; } 
        }

        public class Response
        {
            public Guid Guid { get; set; }
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IRestaurantRepository _restaurantRepository;
            private ITableRepository _tableRepository;
            private IUnitOfWork _unitOfWork;

            public UseCase(IRestaurantRepository restaurantRepository, ITableRepository tableRepository, IUnitOfWork unitOfWork)
            {
                _restaurantRepository = restaurantRepository;
                _tableRepository = tableRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var table = await _tableRepository.GetByIdAsync(request.Id);

                if (table is null)
                {
                    throw new EntityNotFoundException("Table not found");
                }

                var restaurant = await _restaurantRepository.GetByIdAsync(table.RestaurantId);

                if (restaurant is null)
                {
                    throw new EntityNotFoundException("Restaurant not found");
                }

                if (restaurant.Id != request.RestaurantId)
                {
                    throw new BussinessRuleValidationExeption("Given Restaurant doesn't own given table");
                }

                restaurant.RemoveTable(table);

                await _tableRepository.DeleteAsync(table);

                await _unitOfWork.SaveChangesAsync();

                return new Response
                {
                    Guid = request.Id
                };
            }
        }
    }
}