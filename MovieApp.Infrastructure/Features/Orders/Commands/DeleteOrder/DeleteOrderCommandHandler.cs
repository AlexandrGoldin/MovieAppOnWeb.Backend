using Ardalis.GuardClauses;
using MediatR;
using MovieApp.ApplicationCore.Entities;
using MovieApp.ApplicationCore.Interfaces;

namespace MovieApp.Infrastructure.Features.Orders.Commands.DeleteOrder
{
    internal sealed class DeleteOrderCommandHandler
        : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public DeleteOrderCommandHandler(IRepository<Order> orderRepository) 
        {
            _orderRepository = orderRepository;


        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDelete = await _orderRepository.GetByIdAsync(request.Id);

            if (orderDelete is null)
                throw new NotFoundException(nameof(orderDelete), request.Id.ToString());

            await _orderRepository.DeleteAsync(orderDelete, cancellationToken);
        }
    }
}

//internal sealed class DeleteMovieCommandHandler
//        : IRequestHandler<DeleteMovieCommand>
//{
//    private readonly IRepository<Movie> _movieRepository;

//    public DeleteMovieCommandHandler(IRepository<Movie> movieRepository,
//        IUriComposer uriComposer)
//    {
//        _movieRepository = movieRepository;
//    }

//    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
//    {
//        //22 Movies are not available for deletion
//        if (request.Id > 0 && request.Id < 23)
//        {
//            throw new DuplicateException($"Existing movie with Id: {request.Id} is not available for deletion");
//        }
//        var movieDelete = await _movieRepository.GetByIdAsync(request.Id);

//        if (movieDelete is null)
//            throw new NotFoundException(nameof(movieDelete), request.Id.ToString());

//        await _movieRepository.DeleteAsync(movieDelete, cancellationToken);
//    }
//}
