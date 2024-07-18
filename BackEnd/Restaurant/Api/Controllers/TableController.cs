using Api.Data.DTOs.Table;
using Application.Queries.Table;
using Application.UseCases.Restaurant._Table.AddTable;
using Application.UseCases.Restaurant._Table.RemoveTable;
using Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Queries.Table.ReadAllRestaurantTables;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/tables")]
    public class TableController : Controller
    {
        private readonly IMediator _mediator;
        private IReadAllRestaurantTablesQuery _readAllRestaurantTablesQuery;

        public TableController(IMediator mediator, IReadAllRestaurantTablesQuery readAllRestaurantTablesQuery)
        {
            _mediator = mediator;
            _readAllRestaurantTablesQuery = readAllRestaurantTablesQuery;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReadTableDto>))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))] // not found
        public async Task<IActionResult> GetAllTablesFromRestaurant([FromRoute] Guid restaurantId, [FromQuery] int pagesToSkip, [FromQuery] int itemsPerPage)
        {
            var request = new ReadAllRestaurantTables.Request()
            {
                RestaurantId = restaurantId,
                PagesToSkip = pagesToSkip,
                ItemsPerPage = itemsPerPage
            };

            var result = await _readAllRestaurantTablesQuery.Execute(request);

            var returnValue = result.Tables.Adapt<List<ReadTableDto>>();

            return Ok(returnValue);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ReadTableDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> AddTableToRestaurant([FromBody] CreateTableDto createTableDto, [FromRoute] Guid restaurantId)
        {
            var request = new AddTableUseCase.Request
            {
                RestaurantId = restaurantId,
                NumberOfSeats = createTableDto.NumberOfSeats,
            };

            var result = await _mediator.Send(request);

            var response = result.Adapt<ReadTableDto>();

            return Ok(response);
        }

        [HttpDelete("{tableId}")]
        [ProducesResponseType(200, Type = typeof(Guid))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> RemoveTableFromRestaurant([FromRoute] Guid tableId, [FromRoute] Guid restaurantId)
        {
            var request = new RemoveTableUseCase.Request
            {
                Id = tableId,
                RestaurantId = restaurantId
            };

            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}