using Common.Exceptions;

namespace Domain.Models
{
    public class Review
    {
        public Guid Id { get; init; }

        public Guid UserId { get; init; }

        public Guid RestaurantId { get; init; }

        public string Title { get; private set; }

        public string Text { get; private set; }

        public int Grade { get; private set; }

        private Review(Guid id, Guid userId, Guid restaurantId, string title, string text, int grade)
        {
            Id = id;
            UserId = userId;
            RestaurantId = restaurantId;
            Title = title;
            Text = text;
            Grade = grade;
        }

        // Static znaci da se moze pozvati nad klasom, bez da smo pravili instancu.
        public static Review Create(User user, Restaurant restaurant, string title, string text, int grade)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new BussinessRuleValidationExeption("Title is required for review");
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new BussinessRuleValidationExeption("Text is required for review");
            }

            if (string.IsNullOrWhiteSpace(grade.ToString()))
            {
                throw new BussinessRuleValidationExeption("Grade is required for review");
            }

            return new Review(Guid.NewGuid(), user.Id, restaurant.Id, title, text, grade);
        }
    }
}