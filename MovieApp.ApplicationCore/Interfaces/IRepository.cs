using Ardalis.Specification;

namespace MovieApp.ApplicationCore.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
