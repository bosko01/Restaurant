using Api.Data.DTOs.RestaurantDto;
using Api.Data.DTOs.UserDTOs;
using Application.UseCases.Restaurant.CreateRestaurant;
using Application.UseCases.Restaurant.DeleteRestaurant;
using Application.UseCases.Restaurant.ReadRestaurant;
using Application.UseCases.Restaurant.UpdateRestaurant;
using Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : Controller
    {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<IActionResult> GetAllRestaurants()
        {
            var query = new ReadRestaurantsUseCase.Request();

            var result = await _mediator.Send(query);

            var resultDto = result.Restaurants.Adapt<List<ReadRestaurantDto>>();

            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var request = new DeleteRestaurantUseCase.Request { Id = id };

            var resoult = await _mediator.Send(request);

            return Ok(resoult.Id);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReadRestaurantDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
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
        public async Task<IActionResult> UpdateUser([FromBody] UpdateRestaurantDto updateRestausentDto, [FromRoute] Guid id)
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
                Menu = updateRestausentDto.Menu
            };

            var response = await _mediator.Send(request);

            return Ok(response.Adapt<ReadUserDto>());
        }
    }
}