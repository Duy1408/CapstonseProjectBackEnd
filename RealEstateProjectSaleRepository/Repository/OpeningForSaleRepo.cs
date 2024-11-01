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
    public class OpeningForSaleRepo : IOpeningForSaleRepo
    {

        private OpeningForSaleDAO _open;
        public OpeningForSaleRepo()
        {
            _open = new OpeningForSaleDAO();
        }
        public void AddNew(OpeningForSale o)
        {
            _open.AddNew(o);
        }

        public bool ChangeStatus(OpeningForSale o)
        {
            return _open.ChangeStatus(o);
        }

        public OpeningForSale FindByDetailIdAndStatus(Guid projectId)
        {
            return _open.FindByDetailIdAndStatus(projectId);
        }

        public OpeningForSale GetOpeningForSaleById(Guid id)
        {
            return _open.GetOpeningForSaleByID(id);
        }

        public List<OpeningForSale> GetOpeningForSales()
        {
            return _open.GetAllOppeningForSale();
        }

        public IQueryable<OpeningForSale> GetOpeningForSaleByProjectCategoryDetailID(Guid id)
        {
            return _open.GetOpeningForSaleByProjectCategoryDetailID(id);
        }

        public IQueryable<OpeningForSale> SearchOpeningForSale(string name)
        {
            return _open.SearchOpeningForSaleByName(name);
        }

        public void UpdateOpeningForSale(OpeningForSale o)
        {
            _open.UpdateOpeningForSale(o);
        }

        public OpeningForSale FindByProjectIdAndStatus(Guid projectId)
        {
            return _open.FindByProjectIdAndStatus(projectId);
        }
    }
}
