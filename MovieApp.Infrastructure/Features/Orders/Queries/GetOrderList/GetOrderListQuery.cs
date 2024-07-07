using MediatR;

namespace MovieApp.Infrastructure.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<IEnumerable<OrderQueryResponse>>
    {
        public string UserName { get; set; }

        public GetOrderListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
