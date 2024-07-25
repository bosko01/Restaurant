using Application.Interfaces;
using Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.IUser;
using Domain.Interfaces.IUserCredentials;
using MediatR;

namespace Application.UseCases.Users.UpdateUser
{
    public static class UpdateUserUseCase
    {
        public class User
        {
            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;

            public string Number { get; set; } = string.Empty;
        }

        public class Request : IRequest<Response>
        {
            public Guid UserId { get; set; }

            public User User { get; set; } = new();
        }

        public class Response
        {
            public Guid Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string CountryCode { get; set; }

            public string Number { get; set; }

            public string ImageUrl { get; set; }

            public Response(Guid id, string firstName, string lastName, string countryCode, string number, string imageUrl)
            {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
                CountryCode = countryCode;
                Number = number;
                ImageUrl = imageUrl;
            }
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IUserRepository _userRepository;
            private IUnitOfWork _unitOfWork;
            private IPasswordService _passwordService;
            private IUserCredentialsRepository _userCredentialsRepository;

            public UseCase(IUserRepository userRepository, IUserCredentialsRepository userCredentialsRepository, IUnitOfWork unitOfWork, IPasswordService passwordService)
            {
                _userRepository = userRepository;
                _userCredentialsRepository = userCredentialsRepository;
                _unitOfWork = unitOfWork;
                _passwordService = passwordService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var userToUpdate = await _userRepository.GetByIdAsync(request.UserId);

                if (userToUpdate is null)
                {
                    throw new EntityNotFoundException("User not found");
                }

                userToUpdate.Update(request.User.FirstName, request.User.LastName, request.User.CountryCode, request.User.Number);

                await _unitOfWork.SaveChangesAsync();

                var userImage = userToUpdate.ImageUrl is null ? "" : userToUpdate.ImageUrl;

                return new Response(userToUpdate.Id, userToUpdate.FirstName, userToUpdate.LastName, userToUpdate.Phone.CountryCode.ToString(), userToUpdate.Phone.Number.ToString(), userImage);
            }
        }
    }
}