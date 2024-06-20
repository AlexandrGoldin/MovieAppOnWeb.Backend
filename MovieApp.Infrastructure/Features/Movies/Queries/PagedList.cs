namespace MovieApp.Infrastructure.Movies.Queries
{
    public class PagedList<T>
    {
        private PagedList(List<T> items, int page, int totalPages, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalCount = totalCount;
        }

        public List<T> Items { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => PageSize > 1;

        public static PagedList<T> CreateAsync(List<T> query, int page, int pageSize)
        {
            int totalCount = query.Count();
            int totalPages = int.Parse(Math.Ceiling(((decimal)totalCount / pageSize)).ToString());
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new(items, page, totalPages, pageSize, totalCount);
        }
    }
}
