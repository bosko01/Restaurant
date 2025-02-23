﻿using Api.Data.DTOs.ComonDto;
using Api.Data.DTOs.UserDTOs;
using Application.Queries.UserQueries;
using Application.UseCases.Users.CreateUser;
using Application.UseCases.Users.DeleteUser;
using Application.UseCases.Users.ReadUser;
using Application.UseCases.Users.UpdateUser;
using Application.UseCases.Users.UpdateUser.AddImage;
using Application.UseCases.Users.UpdateUser.RemoveImage;
using Common.Exceptions;
using Common.Infrastructure.File;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Queries.UserQueries.ReadUsersPaginated;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private IReadUsersPaginatedQuery _readUsersPaginatedQuery;

        public UserController(IMediator mediator, IReadUsersPaginatedQuery readUsersPaginatedQuery)
        {
            _mediator = mediator;
            _readUsersPaginatedQuery = readUsersPaginatedQuery;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReadUserDto>))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))] // not found
        public async Task<IActionResult> GetAllUsersPagianted([FromQuery] int pagesToSkip, [FromQuery] int itemsPerPage)
        {
            var query = new ReadUsersPaginated.Request()
            {
                PagesToSkip = pagesToSkip,
                ItemsPerPage = itemsPerPage
            };

            var result = await _readUsersPaginatedQuery.Execute(query);

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

        [HttpPatch("{userId}/image")]
        [ProducesResponseType(200, Type = typeof(ReadUserDto))] // ok
        [ProducesResponseType(400, Type = typeof(ErrorDetails))] // bad req
        [ProducesResponseType(409, Type = typeof(ErrorDetails))] // conflict
        public async Task<IActionResult> AddUserImage([FromForm] FileRequest? formFile, [FromRoute] Guid userId)
        {
            if (formFile is null || formFile.File is null || formFile.File.Length <= 0)
            {
                var request = new RemoveUserImageUseCase.Request
                {
                    UserId = userId
                };

                var result = await _mediator.Send(request);

                return Ok(result.Adapt<ReadUserDto>());
            }
            else
            {
                var formFileDto = new AddUserImageUseCase.UserFileRequest
                {
                    FileName = formFile.File!.FileName,
                    ContentType = formFile.File.ContentType,
                    Length = formFile.File.Length,
                    Content = await FileContentConverter.ConvertToByteArrayAsync(formFile.File)
                };

                var request = new AddUserImageUseCase.Request
                {
                    File = formFileDto,
                    UserId = userId
                };

                var result = await _mediator.Send(request);

                return Ok(result.Adapt<ReadUserDto>());
            }
        }
    }
}