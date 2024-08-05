using Microsoft.EntityFrameworkCore;

namespace Core.DTOs.Paging
{
    public class PageList<T>: List<T>
    {
        public PageList(IEnumerable<T> items, int pageNumber, int pageSize, int count)
        {
            this.TotalPage = (int)Math.Ceiling(count / (double)pageSize); //20/10=2
            this.CurrentPage = pageNumber;
            this.PageSize = pageSize;
            this.TotalCount = count;
            this.AddRange(items);
        }


        public int CurrentPage { get; set; }

        public int TotalPage { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
        public static async Task<PageList<T>> CreateAsync(IQueryable<T> sourece, int pageNumber, int pageSize)
        {
            var count = await sourece.CountAsync();
            var items = await sourece.Skip((pageNumber-1 ) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<T>(items,pageNumber,pageSize,count);
        }
    }
}
