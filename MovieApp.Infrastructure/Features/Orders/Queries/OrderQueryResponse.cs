using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure.Features.Orders.Queries
{
    public class OrderQueryResponse
    {
        public int Id { get; set; } 
        public string? BuyerId { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public MovieQueryResponse MovieQueryResponse { get; set; }

        public OrderQueryResponse(int id, string? buyerId, DateTimeOffset orderDate,
            MovieQueryResponse movieQueryResponse)
        {
            Id = id;
            BuyerId = buyerId;
            OrderDate = orderDate;
            MovieQueryResponse = movieQueryResponse;
        }
    }
}
