using MediatR;
using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Interfaces;

namespace MovieApp.Infrastructure.Features.Countries.Queries.GetCountryList
{
    internal class GetCountryListQueryHandler
        : IRequestHandler<GetCountryListQuery, List<CountryQueryResponse>>
    {
        private readonly IReadRepository<Country> _countryRepository;

        public GetCountryListQueryHandler(IReadRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<CountryQueryResponse>> Handle(GetCountryListQuery request, 
            CancellationToken cancellationToken)
        {
            await Task.Delay(500);

            var countryListWithoutDuplicate = (await _countryRepository.ListAsync()).Distinct();

            var countryList = new List<CountryQueryResponse>();

            countryList = countryListWithoutDuplicate.Select(c => new CountryQueryResponse
            {
                CountryId = c.Id,
                CountryName = c.CountryName,
                CountryValue = c.CountryName
            }).ToList();

            return countryList;
        }
    }
}
