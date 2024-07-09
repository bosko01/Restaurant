using Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.IUser;
using Domain.Interfaces.IUserCredentials;
using MediatR;

namespace Application.UseCases.Users.DeleteUser
{
    public static class DeleteUserUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class Response
        {
            public Guid DeletedId { get; set; }
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IUserRepository _userRepository;
            private IUnitOfWork _unitOfWork;
            private IUserCredentialsRepository _userCredentialsRepository;

            public UseCase(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserCredentialsRepository userCredentialsRepository)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
                _userCredentialsRepository = userCredentialsRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user is null)
                {
                    throw new EntityNotFoundException("User with Id doesnt exist");
                }

                await _userRepository.DeleteAsync(user);

                await _unitOfWork.SaveChangesAsync();

                return new Response
                {
                    DeletedId = request.Id
                };
            }
        }
    }
}