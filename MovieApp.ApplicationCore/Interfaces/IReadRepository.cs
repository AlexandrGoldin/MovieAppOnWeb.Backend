using Ardalis.Specification;

namespace MovieApp.Infrastructure.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
