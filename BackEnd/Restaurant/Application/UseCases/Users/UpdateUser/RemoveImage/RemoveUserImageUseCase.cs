using Application.Interfaces;
using Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.IUser;
using MediatR;

namespace Application.UseCases.Users.UpdateUser.RemoveImage
{
    public static class RemoveUserImageUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid UserId { get; set; }
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
            private IUnitOfWork _unitOfWork;
            private IFileManager _fileManager;
            private IRouteGenerator _routeGenerator;

            public UseCase(IUnitOfWork unitOfWork, IUserRepository userRepository, IFileManager fileManager, IRouteGenerator routeGenerator)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
                _fileManager = fileManager;
                _routeGenerator = routeGenerator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user is null)
                {
                    throw new EntityNotFoundException("User not found");
                }

                var userImageRoute = _routeGenerator.GenerateUserImageRoute(request.UserId);

                var completedDeleteProces = await _fileManager.DeleteEverythingFromFolder(userImageRoute);

                if (completedDeleteProces)
                {
                    user.RemoveImage();

                    await _unitOfWork.SaveChangesAsync();

                    return new Response
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Phone = user.Phone.ToString(),
                        ImageUrl = user.ImageUrl
                    };
                }
                else
                {
                    throw new BussinessRuleValidationExeption("Something went wrong");
                }

            }
        }
    }
}