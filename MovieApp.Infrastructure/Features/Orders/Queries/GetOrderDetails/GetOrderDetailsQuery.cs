using MediatR;

namespace MovieApp.Infrastructure.Features.Orders.Queries.GetOrderDetails
{
    public record GetOrderDetailsQuery(string UserName, int OrderId )
        : IRequest<OrderQueryResponse>;
}
