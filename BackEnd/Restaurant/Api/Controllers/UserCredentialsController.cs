using Application.UseCases.UserCredentials;
using Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/userCredentials")]
    public class UserCredentialsController : Controller
    {
        private IMediator _mediator;

        public UserCredentialsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400, Type = typeof(ErrorDetails))]
        [ProducesResponseType(404, Type = typeof(ErrorDetails))]
        [ProducesResponseType(409, Type = typeof(ErrorDetails))]
        public async Task<IActionResult> UpdatePassword([FromRoute] Guid id, string password)
        {
            var request = new UpdatePasswordUseCase.Request()
            {
                Id = id,
                Password = password
            };

            var response = await _mediator.Send(request);

            return Ok(response.Id);
        }
    }
}