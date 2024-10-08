﻿using MediatR;
using MovieApp.ApplicationCore.Entities;
using MovieApp.ApplicationCore.Interfaces;

namespace MovieApp.Infrastructure.Features.Genres.Queries.GetGenreList.GetGenreListQuery
{
    public class GetGenreListQueryHandler :
        IRequestHandler<GetGenresListQuery, List<GenreQueryResponse>>
    {
        private readonly IReadRepository<Genre> _genreRepository;

        public GetGenreListQueryHandler(IReadRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreQueryResponse>> Handle(GetGenresListQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(500);

            var genreListWithoutDuplicate = (await _genreRepository.ListAsync()).Distinct();

            var genreList = new List<GenreQueryResponse>();

            genreList = genreListWithoutDuplicate.Select(g => new GenreQueryResponse
            {
                GenreId = g.Id,
                GenreName = g.GenreName,
                GenreValue = g.GenreName
            }).ToList();

            return genreList;
        }     
    }
}
