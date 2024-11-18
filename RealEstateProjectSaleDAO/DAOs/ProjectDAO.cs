using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class ProjectDAO
    {
        private static ProjectDAO instance;

        public static ProjectDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectDAO();
                }
                return instance;
            }


        }

        public List<Project> GetAllProject()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Projects.Include(p => p.PaymentPolicy).ToList();
        }

        public bool AddNew(Project p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Projects.SingleOrDefault(c => c.ProjectID == p.ProjectID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Projects.Add(p);
                _context.SaveChanges();
                return true;

            }
        }

        public bool Update(Project p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Projects.SingleOrDefault(c => c.ProjectID == p.ProjectID);

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

        public bool ChangeStatus(Project p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Projects.FirstOrDefault(c => c.ProjectID.Equals(p.ProjectID));


            if (a == null)
            {
                return false;
            }
            else
            {
                a.Status = ProjectStatus.DaHuy.GetEnumDescription();
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }



        public Project GetProjectByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Projects.Include(p => p.PaymentPolicy).SingleOrDefault(a => a.ProjectID == id);
        }
        public IQueryable<Project> SearchProjectByName(string searchvalue)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Projects.Include(p => p.PaymentPolicy).Where(a => a.ProjectName.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }

        public List<Project> GetProjectByPaymentPolicyID(Guid paymentPolicyId)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Projects.Include(c => c.PaymentPolicy)
                                         .Where(a => a.PaymentPolicyID == paymentPolicyId)
                                         .ToList();
        }
    }
}
