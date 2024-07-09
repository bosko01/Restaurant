namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync(); // this method will save all the changes made to the database
    }
}