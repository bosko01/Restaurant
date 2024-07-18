using Common.Infrastructure;

namespace Application.Queries.UserQueries
{
    public static class ReadUsersPaginated
    {
        public class Request()
        {
            public int PagesToSkip { get; set; }

            public int ItemsPerPage { get; set; }
        }

        public class Response
        {
            public List<UserResponse> Users { get; set; }

            public Response(List<UserResponse> users)
            {
                Users = users;
            }
        }

        public class UserResponse
        {
            public Guid Id { get; set; }

            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public string UserType { get; set; } = string.Empty;

            public string Phone { get; set; } = string.Empty;

            public UserResponse(Guid id, string firstName, string lastName, string userType, string phone)
            {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
                UserType = userType;
                Phone = phone;
            }
        }

        public interface IReadUsersPaginatedQuery : IQuery<Request, Response>
        { }
    }
}