using Microsoft.EntityFrameworkCore;

namespace MovieApp.Infrastructure.Movies.Queries
{
    public class PagedList<T>
    {
        private PagedList(List<T> items, int page, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public List<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => PageSize > 1;

        //public static async Task<PagedList<T>> CreateAsync(IQueryable <T> query, int page, int pageSize)
        public static PagedList<T> CreateAsync(List<T> query, int page, int pageSize)
        {
            //var totalCount =await query.CountAsync();
            int totalCount = query.Count();
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //var items =await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new(items, page, pageSize, totalCount);
        }
    }
}
