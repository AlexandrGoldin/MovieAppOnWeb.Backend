using Ardalis.Specification;
using MovieApp.ApplicationCore.Entities;

namespace MovieApp.Infrastructure.Specifications
{
    public class OrderWithIMovieByIdSpec : Specification<Order>
    {
        public OrderWithIMovieByIdSpec(int orderId)
        {
            Query
                .Where(order => order.Id == orderId)
                .Include(o => o.Movie)
                  .ThenInclude(m => m.Genre)
                .Include(o => o.Movie)
                  .ThenInclude(m => m.Country);
        }
    }
}
