using Carter;
using MediatR;
using MovieApp.Infrastructure.Features.Countries.Queries.GetCountryList;

namespace MovieApp.WebApi.Endpoints
{
     public class Countries : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/countries", async (ISender sender) =>
            {
                var query = new GetCountryListQuery();
                var countryListResponse = await sender.Send(query);

                return Results.Ok(countryListResponse);
            });
        }
    }
}
