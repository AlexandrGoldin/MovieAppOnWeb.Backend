using Ardalis.Specification;

namespace MovieApp.Infrastructure.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
