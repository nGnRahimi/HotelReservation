using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Pagination
{
    //یه جنریکی از تایپ تی که هر تایپی که بهش دادمو بتونه روش پیجینیشن انجام بده
    public class PaginatedList<T>
    {
        public List<T> Items {  get; }
        public int PageNumber { get; }
        public int Totalpages { get; }
        public int TotalCount { get; }



        public PaginatedList(List<T> items , int count,int pageNumber,int pageSize)
        {
            PageNumber = pageNumber;
            Totalpages = (int)Math.Ceiling(count / (double)pageSize);

            //خودم اضافه کردم
            TotalCount = count;
            Items = items;

        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < Totalpages;


        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, bool disablepaging)

        {

            var count = await source.CountAsync();
            if (!disablepaging)

            {
                var items = await source.Skip((pageNumber - 1)*pageSize).Take(pageSize).ToListAsync();
                return new PaginatedList<T>(items,count, pageNumber, pageSize);
            }
            else
            {
                var itemsWithoutPagination = await source.ToListAsync();
                return new PaginatedList<T>( itemsWithoutPagination , count, pageNumber, pageSize); 

            }

        }

    }
}
