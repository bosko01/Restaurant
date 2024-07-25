using Application.Interfaces;
using Common.Exceptions;
using Common.Infrastructure.File;
using Domain.Interfaces;
using Domain.Interfaces.IRestaurant;
using Mapster;
using MediatR;

namespace Application.UseCases.Restaurant.AddMenu
{
    public static class AddMenuUseCase
    {
        public class Request : IRequest<Response>
        {
            public Guid RestaurantId { get; set; }

            public MenuFileRequest File { get; set; } = default!;
        }

        public class MenuFileRequest
        {
            public string FileName { get; set; } = string.Empty;

            public string ContentType { get; set; } = string.Empty;

            public long Length { get; set; } = default;

            public byte[] Content { get; set; } = default!;
        }

        public class Response
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty;

            public string Location { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;

            public string Number { get; set; } = string.Empty;

            public string MenuUrl { get; set; } = string.Empty;

            public TimeOnly WorkingHoursFrom { get; set; }

            public TimeOnly WorkingHoursTo { get; set; }
        }

        public class UseCase : IRequestHandler<Request, Response>
        {
            private IRestaurantRepository _restaurantRepository;
            private IUnitOfWork _unitOfWork;
            private IFileManager _fileManager;
            private IRouteGenerator _routeGenerator { get; set; }

            public UseCase(IRestaurantRepository restaurantRepository, IUnitOfWork unitOfWork, IFileManager fileManager, IRouteGenerator routeGenerator)
            {
                _restaurantRepository = restaurantRepository;
                _unitOfWork = unitOfWork;
                _fileManager = fileManager;
                _routeGenerator = routeGenerator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                if (request.File.Length <= 0)
                {
                    throw new BussinessRuleValidationExeption("Invalid file");
                }

                var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

                if (restaurant is null)
                {
                    throw new EntityNotFoundException("Restaurant not found");
                }

                var menuPath = _routeGenerator.GenerateRestaurantMenuRoute(request.RestaurantId, request.File.FileName);

                var fileForFileManager = request.File.Adapt<FileForFileManager>();

                await _fileManager.SaveFileToFolderLocation(menuPath, fileForFileManager);

                restaurant.AddMenuUrl(menuPath);

                await _unitOfWork.SaveChangesAsync();

                return new Response
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    Location = restaurant.Location,
                    Email = restaurant.Email.ToString(),
                    CountryCode = restaurant.Phone.CountryCode,
                    Number = restaurant.Phone.Number,
                    MenuUrl = restaurant.MenuUrl,
                    WorkingHoursFrom = restaurant.WorkingHoursFrom,
                    WorkingHoursTo = restaurant.WorkingHoursTo
                };
            }
        }
    }
}