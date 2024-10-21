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
        string UploadSingleFile(IFormFile file, string containerName);
        string UploadSingleImage(IFormFile image, string containerName);
        List<string> UploadMultipleFiles();
    }
}
