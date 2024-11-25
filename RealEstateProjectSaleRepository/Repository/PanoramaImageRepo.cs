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
    public class PanoramaImageRepo : IPanoramaImageRepo
    {
        private PanoramaImageDAO _dao;
        public PanoramaImageRepo()
        {
            _dao = new PanoramaImageDAO();
        }
        public void AddNew(PanoramaImage p)
        {
            _dao.AddNewPanoramaImage(p);
        }

        public bool DeletePanoramaImage(PanoramaImage p)
        {
           return _dao.DeletePanoramaImage(p);
        }

        public List<PanoramaImage> GetPanoramaImage()
        {
            return _dao.GetAllPanorama();
        }

        public PanoramaImage GetPanoramaImageById(Guid id)
        {
            return _dao.GetPanoramaImageByID(id);
        }

        public List<PanoramaImage> GetPanoramaImageByProjectId(Guid id)
        {
            return _dao.GetPanoramaByProjectID(id);
        }

        public void UpdatePanoramaImage(PanoramaImage p)
        {
            _dao.UpdatePanoramaImage(p);
        }
    }
}
