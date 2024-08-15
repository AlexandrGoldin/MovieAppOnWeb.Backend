using MediatR;
using MovieApp.Infrastructure.Features.Countries.Queries.GetCountryList;

namespace MovieApp.WebApi.Endpoints
{
    public static class CountriesEndpoints
    {
        public static void MapCountryEndpoints(this IEndpointRouteBuilder app)
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
