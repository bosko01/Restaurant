using Api.Data.DTOs.RestaurantDto;
using Application.Queries.Restaurant;
using Application.UseCases.Restaurant.CreateRestaurant;
using Application.UseCases.Restaurant.DeleteRestaurant;
using Application.UseCases.Restaurant.ReadRestaurant;
using Application.UseCases.Restaurant.UpdateRestaurant;
using Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Queries.Restaurant.ReadAllRestaurants;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : Controller
    {
        private readonly IMediator _mediator;
        private IReadAllRestaurantsQuery _readAllRestaurantsQuery;

        public RestaurantController(IMediator mediator, IReadAllRestaurantsQuery readAllRestaurantsQuery)
        {
            _mediator = mediator;
            _readAllRestaurantsQuery = readAllRestaurantsQuery;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ReadRestaurantDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> AddRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            var request = createRestaurantDto.Adapt<CreateRestaurantUseCase.Request>();

            var result = await _mediator.Send(request);

            var response = result.Adapt<ReadRestaurantDto>();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReadRestaurantDto>))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))] // not found
        public async Task<IActionResult> GetAllRestaurantsPaginated([FromQuery] int skipNumber, [FromQuery] int pageSize)
        {
            var query = new ReadAllRestaurants.Request
            {
                PagesToSkip = skipNumber,
                ItemsPerPage = pageSize,
            };

            var result = await _readAllRestaurantsQuery.Execute(query);

            return Ok(result.Restaurants.Adapt<List<ReadRestaurantDto>>());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] Guid id)
        {
            var request = new DeleteRestaurantUseCase.Request { Id = id };

            var resoult = await _mediator.Send(request);

            return Ok(resoult.Id);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReadRestaurantDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> GetRestaurantById([FromRoute] Guid id)
        {
            var request = new ReadRestaurantByIdUseCase.Request { Id = id };

            var resoult = await _mediator.Send(request);

            var response = resoult.Adapt<ReadRestaurantDto>();

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ReadRestaurantDto))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))]
        [ProducesResponseType(409, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> UpdateRestaurant([FromBody] UpdateRestaurantDto updateRestausentDto, [FromRoute] Guid id)
        {
            var request = new UpdateRestaurantUseCase.Request()
            {
                Id = id,
                Name = updateRestausentDto.Name,
                Description = updateRestausentDto.Description,
                Location = updateRestausentDto.Location,
                Email = updateRestausentDto.Email,
                CountryCode = updateRestausentDto.CountryCode,
                PhoneNumber = updateRestausentDto.PhoneNumber,
                Menu = updateRestausentDto.Menu,
                WorkingHoursFrom = updateRestausentDto.WorkingHoursFrom,
                WorkingHoursTo = updateRestausentDto.WorkingHoursTo
            };

            var response = await _mediator.Send(request);

            return Ok(response.Adapt<ReadRestaurantDto>());
        }
    }
}