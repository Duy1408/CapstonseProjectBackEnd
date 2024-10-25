using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class PagingServices : IPagingServices
    {
        public PagedResult<T> GetPagedList<T>(IQueryable<T> source, int page, int pageSize)
        {
            var totalItems = source.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var skip = (page - 1) * pageSize;
            var items = source.Skip(skip).Take(pageSize).ToList();

            return new PagedResult<T>
            {
                Items = items,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }

    }
}
