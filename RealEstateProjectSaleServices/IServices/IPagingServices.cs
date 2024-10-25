using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IPagingServices
    {
        PagedResult<T> GetPagedList<T>(IQueryable<T> source, int page, int pageSize);

    }
}
