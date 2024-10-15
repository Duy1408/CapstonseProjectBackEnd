using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class UnitTypeDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public UnitTypeDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<UnitType> GetAllUnitType()
        {
            try
            {
                return _context.UnitTypes!.Include(a => a.Project)
                                          .Include(a => a.PropertyType)
                                          .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UnitType GetUnitTypeByID(Guid id)
        {
            try
            {
                var type = _context.UnitTypes!.Include(a => a.Project)
                                              .Include(a => a.PropertyType)
                                              .SingleOrDefault(c => c.UnitTypeID == id);
                return type;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewUnitType(UnitType type)
        {
            try
            {
                _context.Add(type);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void UpdateUnitType(UnitType type)
        {
            try
            {
                var a = _context.UnitTypes!.SingleOrDefault(c => c.UnitTypeID == type.UnitTypeID);

                _context.Entry(a).CurrentValues.SetValues(type);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusUnitType(UnitType type)
        {
            var _type = _context.UnitTypes!.FirstOrDefault(c => c.UnitTypeID.Equals(type.UnitTypeID));


            if (_type == null)
            {
                return false;
            }
            else
            {
                _type.Status = false;
                _context.Entry(_type).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }


    }
}
