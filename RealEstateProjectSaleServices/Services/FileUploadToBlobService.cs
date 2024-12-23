﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class FileUploadToBlobService : IFileUploadToBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        public FileUploadToBlobService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public string UploadSingleFile(Stream fileStream, string fileName, string containerName)
        {
            var containerInstance = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobName = $"{Guid.NewGuid()}_{fileName}";
            var blobInstance = containerInstance.GetBlobClient(blobName);
            blobInstance.Upload(fileStream, new BlobHttpHeaders { ContentType = "application/pdf" });
            var storageAccountUrl = _configuration["AzureStorage:Url"] + containerName;
            var blobUrl = $"{storageAccountUrl}/{blobName}";

            return blobUrl;

        }

        public string UploadSingleImage(IFormFile image, string containerName)
        {
            var containerInstance = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobName = $"{Guid.NewGuid()}_{image.FileName}";
            var blobInstance = containerInstance.GetBlobClient(blobName);
            blobInstance.Upload(image.OpenReadStream(), new BlobHttpHeaders { ContentType = "image/png" });
            var storageAccountUrl = _configuration["AzureStorage:Url"] + containerName;
            var blobUrl = $"{storageAccountUrl}/{blobName}";

            return blobUrl;
        }

        public List<string> UploadMultipleImages(List<IFormFile> images, string containerName)
        {
            var imageUrls = new List<string>();
            if (images != null && images.Count > 0)
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient(containerName);
                foreach (var image in images)
                {
                    var blobName = $"{Guid.NewGuid()}_{image.FileName}";
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    try
                    {
                        blobInstance.Upload(image.OpenReadStream(), new BlobHttpHeaders { ContentType = "image/png" });
                        var storageAccountUrl = _configuration["AzureStorage:Url"] + containerName;
                        var blobUrl = $"{storageAccountUrl}/{blobName}";
                        imageUrls.Add(blobUrl);

                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException($"Error uploading image {image.FileName} to blob storage", ex);
                    }
                }
            }

            return imageUrls;
        }



    }
}
