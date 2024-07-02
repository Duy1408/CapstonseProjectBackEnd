﻿using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IOpeningForSaleServices
    {
         bool ChangeStatus(OpeningForSale o);


         List<OpeningForSale> GetOpeningForSales();
         void AddNew(OpeningForSale o);


         OpeningForSale GetOpeningForSaleById(Guid id);

         void UpdateOpeningForSale(OpeningForSale o);

         IQueryable<OpeningForSale> SearchOpeningForSale(string name);
    }
}
