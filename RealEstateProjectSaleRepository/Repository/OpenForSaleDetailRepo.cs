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

        public void DeleteOpenForSaleDetailByID(Guid id) => dao.DeleteOpenForSaleDetailByID(id);

        public List<OpenForSaleDetail> GetAllOpenForSaleDetail() => dao.GetAllOpenForSaleDetail();

        public OpenForSaleDetail GetDetailByPropertyIdOpenId(Guid propertyId, Guid openId) => dao.GetDetailByPropertyIdOpenId(propertyId, openId);

        public OpenForSaleDetail GetOpenForSaleDetailByID(Guid id) => dao.GetOpenForSaleDetailByID(id);

        public void UpdateOpenForSaleDetail(OpenForSaleDetail detail) => dao.UpdateOpenForSaleDetail(detail);

    }
}
