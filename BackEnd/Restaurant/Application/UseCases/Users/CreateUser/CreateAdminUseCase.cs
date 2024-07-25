using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.IUser;
using Domain.Interfaces.IUserCredentials;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Users.CreateUser
{
    public class CreateAdminUseCase
    {
        public class Request : IRequest<Response>
        {
            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string Password { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;

            public string Number { get; set; } = string.Empty;

            public Request(string firstName, string lastName, string email, string password, PhoneNumber phoneNumber)
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                Password = password;
                CountryCode = phoneNumber.CountryCode.ToString();
                Number = phoneNumber.Number.ToString();
            }
        }

        public class Response
        {
            public Guid Id { get; set; }

            public Guid CredentialsId { get; set; }

            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;

            public string Number { get; set; } = string.Empty;

            public string? ImageUrl { get; set; } = string.Empty;
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IUserRepository _userRepository;
            private IUserCredentialsRepository _userCredentialsRepository;
            private IUnitOfWork _unitOfWork;
            private IPasswordService _passwordService;

            public UseCase(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserCredentialsRepository userCredentialsRepository, IPasswordService passwordService)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
                _userCredentialsRepository = userCredentialsRepository;
                _passwordService = passwordService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var salt = _passwordService.GenerateSalt();

                var hashedPassword = _passwordService.HashPassword(request.Password, salt);

                var userCredentials = Domain.Models.UserCredentials.Create(request.Email.ToString(), hashedPassword, salt);

                var user = Domain.Models.User.Create(userCredentials.Id.ToString(), request.FirstName, request.LastName, "Admin", request.CountryCode, request.Number);

                await _userCredentialsRepository.CreateNewAsync(userCredentials);

                await _userRepository.CreateNewAsync(user);

                await _unitOfWork.SaveChangesAsync();

                var returnValue = new Response
                {
                    Id = user.Id,
                    CredentialsId = userCredentials.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = userCredentials.Email.ToString(),
                    CountryCode = user.Phone.CountryCode,
                    Number = user.Phone.Number,
                    ImageUrl = user.ImageUrl,
                };

                return returnValue;
            }
        }
    }
}