using Common.Infrastructure;

namespace Application.Queries.Restaurant
{
    public static class ReadAllRestaurants
    {
        public class Request
        {
            public int PagesToSkip { get; set; }

            public int ItemsPerPage { get; set; }
        }

        public class Response
        {
            public List<RestaurantResponse> Restaurants { get; set; } = new();

            public Response(List<RestaurantResponse> res)
            {
                Restaurants = res;
            }
        }

        public class RestaurantResponse
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string Description { get; set; } = string.Empty;

            public string Location { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string CountryCode { get; set; } = string.Empty;

            public string Number { get; set; } = string.Empty;

            public string Menu { get; set; } = string.Empty;

            public TimeOnly WorkingHoursFrom { get; set; }

            public TimeOnly WorkingHoursTo { get; set; }
        }

        public interface IReadAllRestaurantsQuery : IQuery<Request, Response>
        {
        }
    }
}