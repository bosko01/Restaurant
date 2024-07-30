using Domain.Interfaces.IRestaurant;
using Domain.Interfaces;
using MediatR;
using Domain.Interfaces.IUser;
using Domain.Interfaces.ITable;
using Common.Exceptions;
using Domain.Interfaces.IReservation;
using Domain.ValueObjects;

namespace Application.UseCases.Reservation
{
    public static class CreateReservationUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid UserId { get; set;}

            public Guid RestaurantId { get; set; }

            public Guid TableId { get; set; }

            public int NumberOfPeople { get; set; }

            public TimeOnly DurationFrom { get; set; }

            public TimeOnly DurationTo { get; set; }
        }

        public class Response
        {
            public Guid ReservationId { get; set; }

            public string UserFullName { get; set; } = string.Empty;

            public string Phone { get; set; } = string.Empty;

            public string RestaurantName { get; set; } = string.Empty;

            public string RestaurantEmail { get; set; } = string.Empty;

            public string Price { get; set; } = string.Empty;

            public int NumberOfPeople { get; set; } 

            public TimeOnly From { get; set; }

            public TimeOnly To { get; set; }    
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IRestaurantRepository _restaurantRepository;
            private IUnitOfWork _unitOfWork;
            private IUserRepository _userRepository;
            private ITableRepository _tableRepository;
            private IReservationRepository _reservationRepository;
            
            public UseCase(IRestaurantRepository restaurantRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, ITableRepository tableRepository, IReservationRepository reservationRepository)
            {
                _restaurantRepository = restaurantRepository;
                _unitOfWork = unitOfWork;
                _userRepository = userRepository;
                _tableRepository = tableRepository;
                _reservationRepository = reservationRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user is null)
                {
                    throw new EntityNotFoundException("User not found");
                }

                var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

                if(restaurant is null)
                {
                    throw new EntityNotFoundException("Restaurant not found");
                }

                var table = await _tableRepository.GetByIdAsync(request.TableId);

                if (table is null)
                {
                    throw new EntityNotFoundException("Table not found");
                }

                if(table.Seats < request.NumberOfPeople)
                {
                    throw new BussinessRuleValidationExeption("Table doesnt have enough seats for specified number of people");
                }

                if (request.DurationFrom < restaurant.WorkingHoursFrom)
                {
                    throw new BussinessRuleValidationExeption("Reservation cant start before restaurant opens");
                }

                if(request.DurationTo > restaurant.WorkingHoursTo)
                {
                    throw new BussinessRuleValidationExeption("Reservation must finish before restaurant closes");
                }

                var reservation = Domain.Models.Reservation.Create(user.Id, restaurant.Id, table.Id, request.NumberOfPeople, request.DurationFrom, request.DurationTo);

                await _reservationRepository.CreateNewAsync(reservation);

                await _unitOfWork.SaveChangesAsync();

                return new Response
                {
                    ReservationId = reservation.Id,
                    UserFullName = user.FirstName + " " + user.LastName,
                    Phone = user.Phone.ToString(),
                    RestaurantName = restaurant.Name,
                    RestaurantEmail = restaurant.Email.ToString(),
                    Price = reservation.Price.ToString(),
                    NumberOfPeople = reservation.NumberOfPeople,
                    From = reservation.DurationFrom,
                    To = reservation.DurationTo,
                };
            }
        }

    }
}
