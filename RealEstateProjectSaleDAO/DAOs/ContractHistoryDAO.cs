using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class ContractHistoryDAO
    {

        private static ContractHistoryDAO instance;

        public static ContractHistoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContractHistoryDAO();
                }
                return instance;
            }


        }

        //public List<ContractHistory> GetAllContractHistory()
        //{
        //    var _context = new RealEstateProjectSaleSystemDBContext();
        //    return _context.Con.Include(c => c.Zone).ToList();
        //}
    }
}
