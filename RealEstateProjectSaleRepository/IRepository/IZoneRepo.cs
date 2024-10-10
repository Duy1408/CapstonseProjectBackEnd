using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IZoneRepo
    {
        bool ChangeStatus(Zone p);


        List<Zone> GetZones();
        void AddNew(Zone p);


        Zone GetZoneById(Guid id);

        void UpdateZone(Zone p);
    }
}
