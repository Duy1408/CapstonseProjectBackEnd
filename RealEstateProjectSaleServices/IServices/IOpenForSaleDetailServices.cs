using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IOpenForSaleDetailServices
    {
        List<OpenForSaleDetail> GetAllOpenForSaleDetail();
        void AddNewOpenForSaleDetail(OpenForSaleDetail detail);
        OpenForSaleDetail GetOpenForSaleDetailByID(Guid id);
        OpenForSaleDetail GetDetailByPropertyIdOpenId(Guid propertyId, Guid openId);
        void UpdateOpenForSaleDetail(OpenForSaleDetail detail);
        void DeleteOpenForSaleDetailByID(Guid id);

    }
}
