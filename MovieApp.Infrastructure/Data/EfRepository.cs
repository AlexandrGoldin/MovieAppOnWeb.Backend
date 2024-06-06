using Ardalis.Specification.EntityFrameworkCore;
using MovieApp.Infrastructure.Interfaces;

namespace MovieApp.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
