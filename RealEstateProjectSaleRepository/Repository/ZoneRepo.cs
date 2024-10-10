using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleDAO.DAOs;
using RealEstateProjectSaleRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.Repository
{
    public class ZoneRepo : IZoneRepo
    {
        private ZoneDAO _dao;
        public ZoneRepo()
        {
            _dao = new ZoneDAO();
        }
        public void AddNew(Zone p)
        {
            _dao.AddNew(p);
        }

        public bool ChangeStatus(Zone p)
        {
            return _dao.ChangeStatus(p);
        }

        public Zone GetZoneById(Guid id)
        {
            return _dao.GetZoneByID(id);
        }

        public List<Zone> GetZones()
        {
            return _dao.GetAllZone();
        }

        public void UpdateZone(Zone p)
        {
            _dao.UpdateZone(p);
        }
    }
}
