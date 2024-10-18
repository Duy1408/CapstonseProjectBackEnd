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
         bool ChangeStatus(Salespolicy p);


        List<Salespolicy> GetSalespolicys();
         void AddNew(Salespolicy p);


         Salespolicy GetSalespolicyById(Guid id);

         void UpdateSalespolicy(Salespolicy p);
        Salespolicy GetSalespolicyByProjectID(Guid projectid);



    }
}
