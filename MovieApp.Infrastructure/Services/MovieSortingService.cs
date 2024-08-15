using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace MovieApp.Infrastructure.Services
{
    internal class MovieSortingService : IMovieSortingService
    {
        public Task<List<MovieQueryResponse>> SortingMoviesAsync(MovieQueryParams queryParams, List<MovieQueryResponse> movieListRequest)
        {
            var movieList = GetSortedListOfMovies(queryParams, movieListRequest);
            return Task.FromResult(movieList!);
        }

        private static List<MovieQueryResponse>? GetSortedListOfMovies(MovieQueryParams queryParams, List<MovieQueryResponse>? movieListRequest)
        {
            string? sortColumn, sortOrder;
            GetMovieSortingDirection(queryParams, out sortColumn, out sortOrder);

            if (IsNotNull(movieListRequest))
                Console.WriteLine(movieListRequest.Count);

            movieListRequest = SortingMovies(movieListRequest!, sortColumn, sortOrder);

            return movieListRequest;
        }

        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        private static List<MovieQueryResponse>? SortingMovies(List<MovieQueryResponse>? movieListRequest, string sortColumn, string sortOrder)
        {
            if (sortOrder.ToLower() == "desc")
            {
                movieListRequest = movieListRequest?.AsQueryable().OrderByDescending(GetFieldToSortMovies(sortColumn))
                    .ToList();
            }
            else
            {
                movieListRequest = movieListRequest?.AsQueryable().OrderBy(GetFieldToSortMovies(sortColumn))
                    .ToList();
            }

            return movieListRequest;
        }

        private static void GetMovieSortingDirection(MovieQueryParams queryParams, out string sortColumn, out string sortOrder)
        {
            sortColumn = "rating";
            sortOrder = "desc";
            if (!string.IsNullOrWhiteSpace(queryParams.SortBy))
            {
                var sortParams = queryParams.SortBy.Split('.');
                if (sortParams.Length == 2)
                {
                    sortColumn = sortParams[0];
                    sortOrder = sortParams[1];
                }
            }
        }

        private static Expression<Func<MovieQueryResponse, object>> GetFieldToSortMovies(string sortColumn)
            => sortColumn.ToLower() switch
            {
                "release_date" => movie => movie.ReleaseDate!,
                "rating" => movie => movie.Rating,
                _ => movie => movie.Rating
            };
    }
}

