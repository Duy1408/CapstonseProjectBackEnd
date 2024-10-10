using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class ZoneDAO
    {

        private static ZoneDAO instance;

        public static ZoneDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ZoneDAO();
                }
                return instance;
            }

        }

        public List<Zone> GetAllZone()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Zones.Include(c => c.Project).ToList();
        }

        public bool AddNew(Zone z)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Zones.SingleOrDefault(c => c.ZoneID == z.ZoneID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Zones.Add(z);
                _context.SaveChanges();
                return true;

            }
        }

        public Zone GetZoneByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Zones.Include(c => c.Project).SingleOrDefault(a => a.ZoneID == id);
        }

        public bool UpdateZone(Zone b)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Zones.SingleOrDefault(c => c.ZoneID == b.ZoneID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(b);
                _context.SaveChanges();
                return true;
            }
        }

        public bool ChangeStatus(Zone p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Zones.FirstOrDefault(c => c.ZoneID.Equals(p.ZoneID));


            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
