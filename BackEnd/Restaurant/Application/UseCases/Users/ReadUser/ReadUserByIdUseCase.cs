using Common.Exceptions;
using Domain.Interfaces.IUser;
using MediatR;

namespace Application.UseCases.Users.ReadUser
{
    public static class ReadUserByIdUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class Response
        {
            public Guid Id { get; set; }

            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public string Phone { get; set; } = string.Empty;

            public string? ImageUrl { get; set; } = string.Empty;

        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IUserRepository _userRepository;

            public UseCase(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user is null)
                {
                    throw new EntityNotFoundException("Entity with {id} not found");
                }

                return new Response
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.Phone.ToString(),
                    ImageUrl = user.ImageUrl,
                };
            }
        }
    }
}