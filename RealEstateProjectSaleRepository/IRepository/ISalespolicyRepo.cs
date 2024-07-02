using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface ISalespolicyRepo
    {
        public bool ChangeStatus(Salespolicy p);


        public List<Salespolicy> GetSalespolicys();
        public void AddNew(Salespolicy p);


        public Salespolicy GetSalespolicyById(Guid id);

        public void UpdateSalespolicy(Salespolicy p);

        
    }
}
