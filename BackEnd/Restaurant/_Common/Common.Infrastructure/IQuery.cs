namespace Common.Infrastructure
{
    public interface IQuery<TResponse>
    {
        Task<TResponse> Execute(CancellationToken cancellationToken = default);
    }

    public interface IQuery<in TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken = default);
    }
}