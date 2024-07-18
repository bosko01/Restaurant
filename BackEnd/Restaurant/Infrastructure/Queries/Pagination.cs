namespace Infrastructure.Queries
{
    public class Pagination
    {
        public int SkipNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public Pagination(int pagesToSkip, int itemsPerPage)
        {
            SkipNumber = (pagesToSkip - 1) * itemsPerPage;
            ItemsPerPage = itemsPerPage;
        }
    }
}