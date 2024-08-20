using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Interfaces;

namespace MovieApp.ApplicationCore.Entities
{
    public class Order : BaseEntity, IAggregateRoot
    {
        private Order() { }

        public Order(DateTimeOffset orderDate, string buyerId, int movieId)
        {
            OrderDate = orderDate;
            BuyerId = buyerId;
            MovieId = movieId;
        }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public string BuyerId { get; set; } = null!;

        public int MovieId { get; set; }

        public Movie Movie { get; set; } = null!; 
    }
}
       