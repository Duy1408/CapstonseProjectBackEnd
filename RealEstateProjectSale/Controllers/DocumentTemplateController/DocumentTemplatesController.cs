using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.DocumentTemplateController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTemplatesController : ControllerBase
    {
        private readonly IDocumentTemplateService _doc;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;

        public DocumentTemplatesController(IDocumentTemplateService doc, IMapper mapper, BlobServiceClient blobServiceClient)
        {
            _doc = doc;
            _mapper = mapper; ;
            _blobServiceClient = blobServiceClient;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all Document")]

        public IActionResult GetAllDocument()
        {
            try
            {
                if (_doc.GetDocuments() == null)
                {
                    return NotFound();
                }
                var docs = _doc.GetDocuments();
                var response = _mapper.Map<List<DocumentTemplateVM>>(docs);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetDocumentTemplatebyID/{id}")]
        public IActionResult GetDocumentTemplateByID(Guid id)
        {
            var doc = _doc.GetDocumentById(id);

            if (doc != null)
            {
                var responese = _mapper.Map<DocumentTemplateVM>(doc);
                return Ok(responese);
            }
            return NotFound();
        }

        [HttpDelete("DeleteDocumentTemplate/{id}")]
        public IActionResult DeleteDocumentTemplate(Guid id)
        {
            if (_doc.GetDocuments() == null)
            {
                return NotFound();
            }
            var doc = _doc.GetDocumentById(id);
            if (doc == null)
            {
                return NotFound();
            }

            _doc.ChangeStatus(doc);


            return Ok("Delete Successfully");
        }



        [HttpPost]
        public ActionResult<DocumentTemplate> PostDocument([FromForm] DocumentTemplateRequestDTO doc)
        {

            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("documenttemplatefile");
                string? bloUrl = null;
                if (doc.DocumentFile != null)
                {
                    var blobName = $"{Guid.NewGuid()}_{doc.DocumentFile.FileName}";
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    blobInstance.Upload(doc.DocumentFile.OpenReadStream());
                    var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/documenttemplatefile";
                    bloUrl = $"{storageAccountUrl}/{blobName}";
                }
                var newDoc = new DocumentTemplateCreateDTO
                {

                    DocumentTemplateID = Guid.NewGuid(),
                    DocumentName = doc.DocumentName,
                    DocumentFile  = doc.DocumentFile,
                    Status = true,

                };
                var _document = _mapper.Map<DocumentTemplate>(newDoc);
                _document.DocumentFile = bloUrl;
                _doc.AddNew(_document);
                return Ok("create document successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateDocument([FromForm] DocumentTemplateUpdateDTO doc, Guid id)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("documenttemplatefile");
                string? bloUrl = null;
                if (doc.DocumentFile != null)
                {
                    var blobName = $"{Guid.NewGuid()}_{doc.DocumentFile.FileName}";
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    blobInstance.Upload(doc.DocumentFile.OpenReadStream());
                    var storageAccountUrl = "https://realestatesystem.blob.core.windows.net/documenttemplatefile";
                    bloUrl = $"{storageAccountUrl}/{blobName}";
                }
                var docupdate = _doc.GetDocumentById(id);
                if (docupdate != null)
                {
                    if (!string.IsNullOrEmpty(doc.DocumentName))
                    {
                        docupdate.DocumentName = doc.DocumentName;
                    }
                    if (bloUrl != null)
                    {
                        docupdate.DocumentFile = bloUrl;
                    }           
                    if (doc.Status.HasValue)
                    {
                        docupdate.Status = doc.Status.Value;
                    }
                    _doc.UpdateDocument(docupdate);
                    return Ok("Update successfully");
                }
                return NotFound("Doc not found");


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
