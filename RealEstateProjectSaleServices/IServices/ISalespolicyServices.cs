using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface ISalespolicyServices
    {
        bool ChangeStatus(Salespolicy p);


        List<Salespolicy> GetSalespolicys();
        void AddNew(Salespolicy p);


        Salespolicy GetSalespolicyById(Guid id);

        void UpdateSalespolicy(Salespolicy p);
        List<Salespolicy> GetSalespolicyByProjectID(Guid projectid);
        Salespolicy FindByProjectIdAndStatus(Guid projectId);

    }
}
