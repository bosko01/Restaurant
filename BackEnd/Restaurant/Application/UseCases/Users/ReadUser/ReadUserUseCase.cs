using Common.Exceptions;
using Domain.Interfaces.IUser;
using Mapster;
using MediatR;

namespace Application.UseCases.Users.ReadUser
{
    public static class ReadUserUseCase
    {
        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<UserResponse> Users { get; set; }

            public Response(List<UserResponse> users)
            {
                Users = users;
            }
        }

        public class UserResponse
        {
            public Guid Id { get; set; }

            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string UserType { get; set; } = string.Empty;

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
                var users = await _userRepository.GetAllAsync();

                if (users is null)
                {
                    throw new EntityNotFoundException("Users not found");
                }
                var returnValue = users.Adapt<List<UserResponse>>();

                return new Response(returnValue);
            }
        }
    }
}