using System;
using System.Collections.Generic;

namespace Shared.DTO
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(IEnumerable<T> source, int pageNumber, int pageSize, int totalCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            this.AddRange(source);

            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPageCount { get; private set; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPageCount;
    }
}
