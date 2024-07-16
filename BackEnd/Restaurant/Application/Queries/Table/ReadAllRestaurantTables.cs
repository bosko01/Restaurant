using Common.Infrastructure;

namespace Application.Queries.Table
{
    public static class ReadAllRestaurantTables
    {
        public class Request
        {
            public Guid RestaurantId { get; set; }
        }

        public class Response
        {
            public List<TableResponse> Tables { get; set; } = new();
        }

        public class TableResponse
        {
            public Guid Id { get; set; }

            public Guid RestaurantId { get; set; }

            public int Seats { get; set; }
        }

        public interface IReadAllRestaurantTablesQuery : IQuery<Request, Response> { }


    }
}
