using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class PanoramaImageDAO
    {

        private static PanoramaImageDAO instance;

        public static PanoramaImageDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PanoramaImageDAO();
                }
                return instance;
            }


        }

        public List<PanoramaImage> GetAllPanorama()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PanoramaImages.Include(c => c.Project).ToList();
        }


        public bool AddNewPanoramaImage(PanoramaImage p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PanoramaImages.SingleOrDefault(c => c.PanoramaImageID == p.PanoramaImageID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.PanoramaImages.Add(p);
                _context.SaveChanges();
                return true;

            }
        }


        public PanoramaImage GetPanoramaImageByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PanoramaImages.Include(c => c.Project).SingleOrDefault(a => a.PanoramaImageID == id);
        }

        public bool UpdatePanoramaImage(PanoramaImage p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PanoramaImages.SingleOrDefault(c => c.PanoramaImageID == p.PanoramaImageID);

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

        public List<PanoramaImage> GetPanoramaByProjectID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.PanoramaImages.Include(c => c.Project).Where(a=>a.ProjectID==id)
                          
                                    .ToList();
        }

        public bool DeletePanoramaImage(PanoramaImage p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.PanoramaImages.FirstOrDefault(c => c.PanoramaImageID.Equals(p.PanoramaImageID));


            if (a == null)
            {
                return false;
            }
            else
            {
                _context.PanoramaImages.Remove(a);
                _context.SaveChanges();
                return true;
            }
        }

    }
}
