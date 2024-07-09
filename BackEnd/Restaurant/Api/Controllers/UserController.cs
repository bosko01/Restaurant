using Api.Data.DTOs.UserDTOs;
using Application.UseCases.Users.CreateUser;
using Application.UseCases.Users.DeleteUser;
using Application.UseCases.Users.ReadUser;
using Application.UseCases.Users.UpdateUser;
using Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReadUserDto>))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))] // not found
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new ReadUserUseCase.Request();

            var result = await _mediator.Send(query);

            var resultDto = result.Users.Adapt<List<ReadUserDto>>();

            return Ok(resultDto);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ReadUserDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto createUserDto)
        {
            var user = createUserDto.Adapt<CreateUserUseCase.Request>();

            var result = await _mediator.Send(user);

            var response = result.Adapt<ReadUserDto>();

            return Ok(response);
        }

        [HttpPost("Admin")]
        [ProducesResponseType(200, Type = typeof(ReadUserDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> AddAdmin([FromBody] CreateUserDto createUserDto)
        {
            var user = createUserDto.Adapt<CreateAdminUseCase.Request>();

            var result = await _mediator.Send(user);

            var response = result.Adapt<ReadUserDto>();

            return Ok(response);
        }

        [HttpPost("Manager")]
        [ProducesResponseType(200, Type = typeof(ReadUserDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> AddManager([FromBody] CreateUserDto createUserDto)
        {
            var user = createUserDto.Adapt<CreateManagerUseCase.Request>();

            var result = await _mediator.Send(user);

            var response = result.Adapt<ReadUserDto>();

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReadUserDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var request = new ReadUserByIdUseCase.Request { Id = id };

            var resoult = await _mediator.Send(request);

            var response = resoult.Adapt<ReadUserDto>();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var request = new DeleteUserUseCase.Request { Id = id };

            var resoult = await _mediator.Send(request);

            return Ok(resoult.DeletedId);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ReadUserDto))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))]
        [ProducesResponseType(409, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto, [FromRoute] Guid id)
        {
            var request = new UpdateUserUseCase.Request()
            {
                UserId = id,
                User = updateUserDto.Adapt<UpdateUserUseCase.User>()
            };

            var response = await _mediator.Send(request);

            return Ok(response.Adapt<ReadUserDto>());
        }
    }
}