using Application.Interfaces;
using Common.Exceptions;
using Common.Infrastructure.File;
using Domain.Interfaces;
using Domain.Interfaces.IUser;
using Mapster;
using MediatR;

namespace Application.UseCases.Users.UpdateUser.AddImage
{
    public static class AddUserImageUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid UserId { get; set; }

            public UserFileRequest File { get; set; } = default!;
        }

        public class UserFileRequest
        {
            public string FileName { get; set; } = string.Empty;

            public string ContentType { get; set; } = string.Empty;

            public long Length { get; set; } = default;

            public byte[] Content { get; set; } = default!;
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
            private IFileManager _FileManager;
            private IRouteGenerator _routeGenerator;

            public UseCase(IUnitOfWork unitOfWork, IUserRepository userRepository, IFileManager fileManager, IRouteGenerator routeGenerator)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
                _FileManager = fileManager;
                _routeGenerator = routeGenerator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user is null)
                {
                    throw new EntityNotFoundException("User not found");
                }

                var imageRoute = _routeGenerator.GenerateUserImageRoute(request.UserId, request.File.FileName);

                var fileForFileManager = request.File.Adapt<FileForFileManager>();

                await _FileManager.SaveFileToFolderLocation(imageRoute, fileForFileManager);

                user.AddImage(imageRoute);

                await _unitOfWork.SaveChangesAsync();

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