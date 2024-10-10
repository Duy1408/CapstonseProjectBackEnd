using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class ZoneService : IZoneService
    {
        private readonly IZoneRepo _repo;
        public ZoneService(IZoneRepo repo)
        {
            _repo = repo;
        }
        public void AddNew(Zone p)
        {
            _repo.AddNew(p);
        }

        public bool ChangeStatus(Zone p)
        {
            return _repo.ChangeStatus(p);
        }

        public Zone GetZoneById(Guid id)
        {
            return _repo.GetZoneById(id);
        }

        public List<Zone> GetZones()
        {
            return _repo.GetZones();
        }

        public void UpdateZone(Zone p)
        {
            _repo.UpdateZone(p);
        }
    }
}
