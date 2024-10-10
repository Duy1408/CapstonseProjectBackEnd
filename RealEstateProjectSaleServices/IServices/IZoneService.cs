using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IZoneService
    {
        bool ChangeStatus(Zone p);


        List<Zone> GetZones();
        void AddNew(Zone p);


        Zone GetZoneById(Guid id);

        void UpdateZone(Zone p);
    }
}
