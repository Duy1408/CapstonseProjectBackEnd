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
    public class PanoramaImageServices : IPanoramaImageServices
    {

        private readonly IPanoramaImageRepo _repo;
        public PanoramaImageServices(IPanoramaImageRepo repo)
        {
            _repo = repo; 
        }
        public void AddNew(PanoramaImage p)
        {
            _repo.AddNew(p);
        }

        public bool DeletePanoramaImage(PanoramaImage p)
        {
            return _repo.DeletePanoramaImage(p);
        }

        public List<PanoramaImage> GetPanoramaImage()
        {
            return _repo.GetPanoramaImage();
        }

        public PanoramaImage GetPanoramaImageById(Guid id)
        {
            return _repo.GetPanoramaImageById(id);
        }

        public void UpdatePanoramaImage(PanoramaImage p)
        {
            _repo.UpdatePanoramaImage(p);
        }
    }
}
