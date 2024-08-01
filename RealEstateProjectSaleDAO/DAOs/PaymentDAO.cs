using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PaymentDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public PaymentDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }



    }
}
