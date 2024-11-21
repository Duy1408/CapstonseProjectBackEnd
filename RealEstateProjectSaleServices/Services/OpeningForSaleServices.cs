using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class OpeningForSaleServices : IOpeningForSaleServices
    {
        private IOpeningForSaleRepo _open;
        public OpeningForSaleServices(IOpeningForSaleRepo open)
        {
            _open = open;
        }
        public void AddNew(OpeningForSale o)
        {
            _open.AddNew(o);
        }

        public bool ChangeStatus(OpeningForSale o)
        {
            return _open.ChangeStatus(o);
        }

        public OpeningForSale FindByDetailIdAndStatus(Guid detailId)
        {
            return _open.FindByDetailIdAndStatus(detailId);
        }

        public OpeningForSale GetOpeningForSaleById(Guid id)
        {
            return _open.GetOpeningForSaleById(id);
        }

        public List<OpeningForSale> GetOpeningForSales()
        {
            return _open.GetOpeningForSales();
        }

        public IQueryable<OpeningForSale> GetOpeningForSaleByProjectCategoryDetailID(Guid id)
        {
            return _open.GetOpeningForSaleByProjectCategoryDetailID(id);
        }

        public IQueryable<OpeningForSale> SearchOpeningForSale(string name)
        {
            return _open.SearchOpeningForSale(name);
        }

        public void UpdateOpeningForSale(OpeningForSale o)
        {
            _open.UpdateOpeningForSale(o);
        }

        public OpeningForSale FindByProjectIdAndStatus(Guid projectId)
        {
            return _open.FindByProjectIdAndStatus(projectId);
        }

        public string GetOpenForSaleStatusByProjectCategoryDetailID(Guid projectCategoryDetailID)
        {
            var openings = _open.GetOpeningForSaleByProjectCategoryDetailID(projectCategoryDetailID);
            foreach (var opening in openings)
            {
                if (DateTime.Now < opening.StartDate || DateTime.Now > opening.EndDate)
                {
                    return OpeningForSaleStatus.ChuaMoBan.GetEnumDescription();
                }
                else if (opening.StartDate <= DateTime.Now && DateTime.Now < opening.CheckinDate)
                {
                    return OpeningForSaleStatus.GiuCho.GetEnumDescription();
                }
                else if (opening.CheckinDate <= DateTime.Now && DateTime.Now < opening.CheckinDate.AddDays(1))
                {
                    return OpeningForSaleStatus.CheckIn.GetEnumDescription();
                }
                else if (opening.CheckinDate.AddDays(1) <= DateTime.Now && DateTime.Now <= opening.EndDate)
                {
                    return OpeningForSaleStatus.MuaTrucTiep.GetEnumDescription();
                }
            }

            return OpeningForSaleStatus.ChuaMoBan.GetEnumDescription();

        }

        public bool GetExistOpenStatusByProjectCategoryDetailID(Guid id)
        {
            return _open.GetExistOpenStatusByProjectCategoryDetailID(id);
        }
    }
}
