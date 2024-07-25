namespace Application.Interfaces
{
    public interface IRouteGenerator
    {
        public string GenerateRestaurantMenuRoute(Guid restaurantId, string fileName);

        public string GenerateUserImageRoute(Guid userId, string fileName);

        public string GenerateRestaurantMenuRoute(Guid restaurantId);

        public string GenerateUserImageRoute(Guid userId);
    }
}