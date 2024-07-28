﻿using MediatR;
using MovieApp.Infrastructure.Features.Movies.Queries;

namespace MovieApp.Infrastructure.Movies.Queries.GetMovieList
{
    public record GetMovieListQuery(  
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize,
    int? MinDate = null,
    int? MaxDate = null) : IRequest<PagedList<MovieQueryResponse>>;

}
