using Application.Interfaces;
using Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.IUser;
using Domain.Interfaces.IUserCredentials;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.UserCredentials
{
    public static class UpdatePasswordUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }

            public string Password { get; set; } = string.Empty;
        }

        public class Response
        {
            public Guid Id { get; set; }
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IUserCredentialsRepository _userCredentialsRepository;
            private IUserRepository _userRepository;
            private IUnitOfWork _unitOfWork;
            private IPasswordService _passwordService;

            public UseCase(IUserCredentialsRepository userCredentialsRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordService passwordService)
            {
                _userCredentialsRepository = userCredentialsRepository;
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
                _passwordService = passwordService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    throw new EntityNotFoundException("User not found");
                }

                var credentials = await _userCredentialsRepository.GetByIdAsync(user.CredentialsId);

                if (credentials is null)
                {
                    throw new EntityNotFoundException("Credentials not found");
                }

                if (_passwordService.VerifyPassword(request.Password, credentials.Password))
                {
                    throw new BussinessRuleValidationExeption("New password must not be same as old password.");
                }

                var newSalt = _passwordService.GenerateSalt();
                var newPasswordHash = _passwordService.HashPassword(request.Password, newSalt);
                credentials.Update(newPasswordHash, newSalt);

                await _unitOfWork.SaveChangesAsync();

                return new Response
                {
                    Id = request.Id,
                };
            }
        }
    }
}