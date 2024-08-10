using MediatR;

namespace MovieApp.Infrastructure.Features.Countries.Queries.GetCountryList
{
    public class GetCountryListQuery : IRequest<List<CountryQueryResponse>>
    {
    }
}
