using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IPanoramaImageRepo
    {
        bool DeletePanoramaImage(PanoramaImage p);


        List<PanoramaImage> GetPanoramaImage();
        void AddNew(PanoramaImage p);


        PanoramaImage GetPanoramaImageById(Guid id);

        void UpdatePanoramaImage(PanoramaImage p);
    }
}
