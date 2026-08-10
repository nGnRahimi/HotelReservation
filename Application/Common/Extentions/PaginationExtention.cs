using Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extentions
{
    public static class PaginationExtention
    {
        public static async Task<PaginatedList<TDestination>>PaginatedListAsync<TDestination>(
            this IQueryable<TDestination>queryable,
            int pageNumber,
            int pageSize,
            bool disablePaging = false)
        {
            return await PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, disablePaging);
        }


    }
}
