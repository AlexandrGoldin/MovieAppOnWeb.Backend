using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using MovieApp.ApplicationCore.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;
using MovieApp.Infrastructure.Specifications;

namespace MovieApp.Infrastructure.Features.Orders.Queries.GetOrderDetails
{
    internal class GetOrderDetailsQueryHandler
        : IRequestHandler<GetOrderDetailsQuery, OrderQueryResponse?>
    {
        private readonly IReadRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderDetailsQueryHandler(IReadRepository<Order> orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderQueryResponse?> Handle(GetOrderDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var spec = new OrderWithIMovieByIdSpec(request.OrderId);
            var order = await _orderRepository.FirstOrDefaultAsync(spec,
                cancellationToken);

            if (order is null) return null;
            //throw new NotFoundException(nameof(order), request.OrderId.ToString());

            return new OrderQueryResponse(
               order.Id,
               order.BuyerId,
               order.OrderDate,
               _mapper.Map<MovieQueryResponse>(order.Movie));
        }       
    }
}
