using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleDAO.DAOs;
using RealEstateProjectSaleRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.Repository
{
    public class OpenForSaleDetailRepo : IOpenForSaleDetailRepo
    {
        OpenForSaleDetailDAO dao = new OpenForSaleDetailDAO();

        public void AddNewOpenForSaleDetail(OpenForSaleDetail detail) => dao.AddNewOpenForSaleDetail(detail);

        public void DeleteOpenForSaleDetailByID(Guid propertyId, Guid openId) => dao.DeleteOpenForSaleDetailByID(propertyId, openId);

        public List<OpenForSaleDetail> GetAllOpenForSaleDetail() => dao.GetAllOpenForSaleDetail();

        public OpenForSaleDetail GetDetailByPropertyIdOpenId(Guid propertyId, Guid openId) => dao.GetDetailByPropertyIdOpenId(propertyId, openId);

        public List<OpenForSaleDetail> GetOpenForSaleDetailByOpeningForSaleID(Guid id) => dao.GetOpenForSaleDetailByOpeningForSaleID(id);

        public void UpdateOpenForSaleDetail(OpenForSaleDetail detail) => dao.UpdateOpenForSaleDetail(detail);

    }
}
