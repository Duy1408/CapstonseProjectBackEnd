using AutoMapper;
using Azure;
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
    [Route("api/document-templates")]
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
                    return NotFound(new
                    {
                        message = "Mẫu tài liệu không tồn tại."
                    });
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


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get DocumentTemplate By ID")]
        public IActionResult GetDocumentTemplateByID(Guid id)
        {
            var doc = _doc.GetDocumentById(id);

            if (doc != null)
            {
                var responese = _mapper.Map<DocumentTemplateVM>(doc);
                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Mẫu tài liệu không tồn tại."
            });
        }

        [HttpGet("file/{id}")]
        [SwaggerOperation(Summary = "Get Document File By ID")]
        public IActionResult GetDocumentFileByID(Guid id)
        {
            var doc = _doc.GetDocumentById(id);

            if (doc != null)
            {
                return Content(doc.DocumentFile, "text/plain");
            }

            return NotFound(new
            {
                message = "Mẫu tài liệu không tồn tại."
            });
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete DocumentTemplate By ID")]
        public IActionResult DeleteDocumentTemplate(Guid id)
        {
            if (_doc.GetDocuments() == null)
            {
                return NotFound(new
                {
                    message = "Mẫu tài liệu không tồn tại."
                });
            }
            var doc = _doc.GetDocumentById(id);
            if (doc == null)
            {
                return NotFound(new
                {
                    message = " Mẫu tài liệu không tồn tại."
                });
            }

            _doc.ChangeStatus(doc);


            return Ok(new
            {
                message = "Xóa mẫu tài liệu thành công"
            });
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new document")]
        public IActionResult AddNew(DocumentTemplateCreateDTO doc)
        {
            try
            {

                var newDoc = new DocumentTemplateCreateDTO
                {
                    DocumentTemplateID = Guid.NewGuid(),
                    DocumentName = doc.DocumentName,
                    DocumentFile = doc.DocumentFile,
                    Status = true,
                };

                var document = _mapper.Map<DocumentTemplate>(newDoc);
                _doc.AddNew(document);


                return Ok(new
                {
                    message = "Tạo mẫu tài liệu thành công"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Doc by ID")]
        public IActionResult UpdateComment([FromForm] DocumentTemplateUpdateDTO doc, Guid id)
        {
            try
            {
                var existingDoc = _doc.GetDocumentById(id);
                if (existingDoc != null)
                {

                    if (!string.IsNullOrEmpty(doc.DocumentName))
                    {
                        existingDoc.DocumentName = doc.DocumentName;
                    }
                    if (!string.IsNullOrEmpty(doc.DocumentFile))
                    {
                        existingDoc.DocumentFile = doc.DocumentFile;
                    }
                    if (doc.Status.HasValue)
                    {
                        existingDoc.Status = doc.Status.Value;
                    }

                    _doc.UpdateDocument(existingDoc);

                    return Ok(new
                    {
                        message = "Cập nhật mẫu tài liệu thành công"
                    });

                }
                return NotFound(new
                {
                    message = "Mẫu tài liệu không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
