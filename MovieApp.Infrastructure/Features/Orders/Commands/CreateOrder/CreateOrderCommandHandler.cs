using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Http;
using MovieApp.ApplicationCore.Entities;
using MovieApp.ApplicationCore.Interfaces;

namespace MovieApp.Infrastructure.Features.Orders.Commands.CreateOrder
{
    internal sealed class CreateOrderCommandHandler :
        IRequestHandler<CreateOrderCommand, OrderCommandResponse>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateOrderCommandHandler(IRepository<Order> orderRepository,
            IHttpContextAccessor httpContextAccessor) 
        {
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OrderCommandResponse> Handle(CreateOrderCommand request, 
            CancellationToken cancellationToken)
        {
            var order = new Order(
                    DateTimeOffset.Now,
                    GetOrderUserName(), //This property BuyerId,
                    request.MovieId);

            var newOrder = await _orderRepository.AddAsync(order, cancellationToken);

            return new OrderCommandResponse(newOrder.Id);
        }

        private string GetOrderUserName()
        {
            Guard.Against.Null(_httpContextAccessor.HttpContext.Request, nameof(_httpContextAccessor.HttpContext.Request));
            string? userName = "";

#pragma warning disable CS8602
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                Guard.Against.Null(_httpContextAccessor.HttpContext.User.Identity.Name,
               nameof(_httpContextAccessor.HttpContext.User.Identity.Name));

                userName = _httpContextAccessor.HttpContext.User.Identity.Name!;

                return userName;
            }
            return "";
        }
    }
}
