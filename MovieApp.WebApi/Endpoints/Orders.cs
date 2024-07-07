using Azure.Core;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MovieApp.ApplicationCore.Constants;
using MovieApp.Infrastructure.Features.Orders.Commands.CreateOrder;
using MovieApp.Infrastructure.Features.Orders.Commands.DeleteOrder;

namespace MovieApp.WebApi.Endpoints
{
    public class Orders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/orders",
               [Authorize(Roles = Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (CreateOrderCommand comand, ISender sender) =>
            {             
                var orderResponse = await sender.Send(comand);

                return Results.Ok(orderResponse.OrderId);
            })
             .DisableAntiforgery();


            app.MapDelete("/api/orders/{id}",
                 [Authorize(Roles = Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (int id, ISender sender) =>
                {
                    await sender.Send(new DeleteOrderCommand(id));

                    return Results.NoContent();
                });
        }
    }
}
