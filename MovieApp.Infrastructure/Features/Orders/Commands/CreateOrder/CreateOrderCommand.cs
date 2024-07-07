using MediatR;

namespace MovieApp.Infrastructure.Features.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(
        DateTimeOffset OrderDate,
        string? BuyerId,
        int MovieId) : IRequest<OrderCommandResponse>;

}
