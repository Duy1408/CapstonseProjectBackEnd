﻿using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IOpeningForSaleRepo
    {
        public bool ChangeStatus(OpeningForSale o);
        bool GetExistOpenStatusByProjectCategoryDetailID(Guid id);

        public List<OpeningForSale> GetOpeningForSales();
        public void AddNew(OpeningForSale o);

        OpeningForSale FindByDetailIdAndStatus(Guid detailId);
        OpeningForSale FindByProjectIdAndStatus(Guid projectId);
        public OpeningForSale GetOpeningForSaleById(Guid id);

        public void UpdateOpeningForSale(OpeningForSale o);

        public IQueryable<OpeningForSale> SearchOpeningForSale(string name);

        IQueryable<OpeningForSale> GetOpeningForSaleByProjectCategoryDetailID(Guid id);
    }
}
