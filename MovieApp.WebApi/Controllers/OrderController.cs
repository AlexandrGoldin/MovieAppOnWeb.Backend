using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.ApplicationCore.Constants;
using MovieApp.Infrastructure.Features.Orders.Queries.GetOrderDetails;
using MovieApp.Infrastructure.Features.Orders.Queries.GetOrderList;

namespace MovieApp.WebApi.Controllers
{
    [Authorize(Roles = Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
            var myOrders = await _mediator.Send(new GetOrderListQuery(User.Identity.Name));

            return Ok(myOrders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Detail(int orderId)
        {
            Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
            var orderDetail = await _mediator.Send(new GetOrderDetailsQuery(User.Identity.Name, orderId));

            if (orderDetail is null)
            {
                return BadRequest("No such order found for this user.");
            }

            return Ok(orderDetail);
        }
    }
}
