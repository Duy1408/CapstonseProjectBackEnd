using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class SalespolicyDAO
    {
        private static SalespolicyDAO instance;

        public static SalespolicyDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SalespolicyDAO();
                }
                return instance;
            }


        }

        public List<Salespolicy> GetAllSalespolicy()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Salespolicies.Include(c => c.Project).ToList();
        }

        public bool AddNew(Salespolicy p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Salespolicies.SingleOrDefault(c => c.SalesPolicyID == p.SalesPolicyID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Salespolicies.Add(p);
                _context.SaveChanges();
                return true;

            }
        }

        public bool UpdateSalespolicy(Salespolicy p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Salespolicies.SingleOrDefault(c => c.SalesPolicyID == p.SalesPolicyID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(p);
                _context.SaveChanges();
                return true;
            }
        }

        public bool ChangeStatus(Salespolicy p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Salespolicies.FirstOrDefault(c => c.SalesPolicyID.Equals(p.SalesPolicyID));


            if (a == null)
            {
                return false;
            }
            else
            {
                a.Status = false;
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }



        public Salespolicy GetSalespolicyByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Salespolicies.Include(c => c.Project).SingleOrDefault(a => a.SalesPolicyID == id);
        }

        public List<Salespolicy> GetSalespolicyByProjectID(Guid projectid)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Salespolicies.Include(c => c.Project)
                                         .Where(a => a.ProjectID == projectid)
                                         .ToList();
        }

    }
}
