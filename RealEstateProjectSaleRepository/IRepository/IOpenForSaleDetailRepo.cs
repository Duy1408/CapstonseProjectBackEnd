using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IOpenForSaleDetailRepo
    {
        List<OpenForSaleDetail> GetAllOpenForSaleDetail();
        void AddNewOpenForSaleDetail(OpenForSaleDetail detail);
        OpenForSaleDetail GetOpenForSaleDetailByID(Guid id);
        void UpdateOpenForSaleDetail(OpenForSaleDetail detail);
        void DeleteOpenForSaleDetailByID(Guid id);

    }
}
