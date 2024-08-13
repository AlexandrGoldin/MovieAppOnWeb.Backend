using MovieApp.Infrastructure.Entities;
using MovieApp.Infrastructure.Features.Movies.Queries;
using MovieApp.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace MovieApp.Infrastructure.Services
{
    internal class MovieSortingService : IMovieSortingService
    {
        public Task<List<Movie>> SortingMoviesAsync(MovieQueryParams queryParams, List<Movie> movieListRequest)
        {
            var movieList = GetSortedListOfMovies(queryParams, movieListRequest);
            return Task.FromResult(movieList!);
        }

        private static List<Movie>? GetSortedListOfMovies(MovieQueryParams queryParams, List<Movie>? movieList)
        {
            string sortColumn, sortOrder;
            GetMovieSortingDirection(queryParams, out sortColumn, out sortOrder);

            movieList = SortingMovies(movieList, sortColumn, sortOrder);
            return movieList;
        }

        private static List<Movie>? SortingMovies(List<Movie>? movieList, string sortColumn, string sortOrder)
        {
            if (sortOrder.ToLower() == "desc")
            {
                movieList = movieList?.AsQueryable().OrderByDescending(GetFieldToSortMovies(sortColumn))
                    .ToList();
            }
            else
            {
                movieList = movieList?.AsQueryable().OrderBy(GetFieldToSortMovies(sortColumn))
                    .ToList();
            }

            return movieList;
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

        private static Expression<Func<Movie, object>> GetFieldToSortMovies(string sortColumn)
            => sortColumn.ToLower() switch
            {
                "release_date" => movie => movie.ReleaseDate!,
                "rating" => movie => movie.Rating,
                _ => movie => movie.Rating
            };
    }
}

