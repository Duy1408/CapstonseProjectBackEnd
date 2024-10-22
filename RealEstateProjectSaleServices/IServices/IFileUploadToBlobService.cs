using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IFileUploadToBlobService
    {
        string UploadSingleFile(Stream fileStream, string fileName, string containerName);
        string UploadSingleImage(IFormFile image, string containerName);
        List<string> UploadMultipleImages(List<IFormFile> images, string containerName);

    }
}
