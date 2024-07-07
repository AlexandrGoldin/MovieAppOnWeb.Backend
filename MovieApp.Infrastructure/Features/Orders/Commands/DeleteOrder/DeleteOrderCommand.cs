using MediatR;

namespace MovieApp.Infrastructure.Features.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(int Id) : IRequest;
}
