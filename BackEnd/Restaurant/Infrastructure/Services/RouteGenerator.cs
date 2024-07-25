using Application.Interfaces;
using Common.Infrastructure;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class RouteGenerator : IRouteGenerator
    {
        private FileStorageLocations _fileStorageLocations;

        public RouteGenerator(IOptions<FileStorageLocations> fileStorageLocations)
        {
            _fileStorageLocations = fileStorageLocations.Value;
        }

        public string GenerateRestaurantMenuRoute(Guid restaurantId, string fileName)
        {
            return _fileStorageLocations.Root + $"/Restaurants/{restaurantId}/Menu/Menu{Path.GetExtension(fileName)}";
        }

        public string GenerateRestaurantMenuRoute(Guid restaurantId)
        {
            return _fileStorageLocations.Root + $"/Restaurants/{restaurantId}/Menu";
        }

        public string GenerateUserImageRoute(Guid userId, string fileName)
        {
            return _fileStorageLocations.Root + $"/Users/{userId}/image/Image{Path.GetExtension(fileName)}";
        }

        public string GenerateUserImageRoute(Guid userId)
        {
            return _fileStorageLocations.Root + $"/Users/{userId}/Image";
        }
    }
}