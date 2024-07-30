using Api.Data.DTOs.Reservation;
using Api.Data.DTOs.RestaurantDto;
using Application.UseCases.Reservation;
using Application.UseCases.Restaurant.CreateRestaurant;
using Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Queries.Restaurant.ReadAllRestaurants;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    public class ReservationController : Controller
    {
        private readonly IMediator _mediator;
        private IReadAllRestaurantsQuery _readAllRestaurantsQuery;

        public ReservationController(IMediator mediator, IReadAllRestaurantsQuery readAllRestaurantsQuery)
        {
            _mediator = mediator;
            _readAllRestaurantsQuery = readAllRestaurantsQuery;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ReadReservationDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> AddReservation([FromBody] CreateReservationDto createReservationDto)
        {
            var request = createReservationDto.Adapt<CreateReservationUseCase.Request>();

            var result = await _mediator.Send(request);

            var response = result.Adapt<ReadReservationDto>();

            return Ok(response);
        }
    }
}
