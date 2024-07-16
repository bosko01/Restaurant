namespace Common.Exceptions
{
    public class EntityNotFoundException : CustomException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
