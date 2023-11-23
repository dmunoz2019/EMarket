using System.Collections.Generic;

namespace API.Dtos
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data, int pageCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;

            PageCount = pageCount;
   

        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }


        public IReadOnlyList<T> Data { get; set; }

        public int PageCount { get; set;}

    }
}
