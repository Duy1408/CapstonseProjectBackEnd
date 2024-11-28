using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IDashboardService
    {
        List<object> GetMonthlyTotalPrices();
        double CalculateTotalPrice();
        int CalculateProperty();
        int SumProperty();
        int CalculateCustomer();
        double CalculateOutstandingAmount();




    }
}
