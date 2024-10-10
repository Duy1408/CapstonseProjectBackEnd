using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class FloorDAO
    {

        private static FloorDAO instance;

        public static FloorDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FloorDAO();
                }
                return instance;
            }
        }




        public List<Floor> GetAllFloor()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Floors.Include(c=>c.Block).ToList();
        }

        public bool AddNew(Floor z)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Floors.SingleOrDefault(c => c.FloorID == z.FloorID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Floors.Add(z);
                _context.SaveChanges();
                return true;

            }
        }

        public Floor GetFloorByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Floors.Include(c => c.Block).SingleOrDefault(a => a.FloorID == id);
        }

        public bool UpdateFloor(Floor b)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Floors.SingleOrDefault(c => c.FloorID == b.FloorID);

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

        public bool ChangeStatus(Floor p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Floors.FirstOrDefault(c => c.FloorID.Equals(p.FloorID));


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
