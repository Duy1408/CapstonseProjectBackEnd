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
        public List<T> GetPagedList<T>(IQueryable<T> source, int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return source.Skip(skip).Take(pageSize).ToList();
        }

    }
}
