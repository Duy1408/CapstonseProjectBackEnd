using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PanoramaImageController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanoramaImageController : ControllerBase
    {
        private readonly IPanoramaImageServices _panorama;
        private readonly IMapper _mapper;
        private readonly IFileUploadToBlobService _fileService;

        public PanoramaImageController(IPanoramaImageServices panorama, IMapper mapper, IFileUploadToBlobService fileService)
        {
            _panorama = panorama;
            _fileService = fileService;
            _mapper = mapper;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Get all PanoramaImage")]

        public IActionResult GetAllPanoramaImage()
        {
            try
            {
                if (_panorama.GetPanoramaImage() == null)
                {
                    return NotFound();
                }
                var panoramaimages = _panorama.GetPanoramaImage();
                var response = _mapper.Map<List<PanoramaImageVM>>(panoramaimages);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetPanoramaImagebyID/{id}")]
        public IActionResult GetPanoramaImageByID(Guid id)
        {
            var panorama = _panorama.GetPanoramaImageById(id);

            if (panorama != null)
            {
                var responese = _mapper.Map<PanoramaImageVM>(panorama);
                return Ok(responese);
            }
            return NotFound();
        }

        [HttpDelete("DeletePanoramaImage/{id}")]
        public IActionResult DeletePanoramaImage(Guid id)
        {
            if (_panorama.GetPanoramaImage() == null)
            {
                return NotFound();
            }
            var panorama = _panorama.GetPanoramaImageById(id);
            if (panorama == null)
            {
                return NotFound();
            }

            _panorama.DeletePanoramaImage(panorama);


            return Ok("Xóa hình panorama thành công.");
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create a new PanoramaImage")]
        public IActionResult AddNewPanoramaImage([FromForm] PanoramaImageRequestDTO panorama, Guid projectid)
        {
            try
            {
                var imageUrls = _fileService.UploadMultipleImages(panorama.Image.ToList(), "panoramaimage");


                var newPanoramaImage = new PanoramaImageCreateDTO
                {
                    PanoramaImageID = Guid.NewGuid(),
                    Title = panorama.Title,
                   
                    Image = panorama.Image.Count > 0 ? panorama.Image.First():null,
                    ProjectID = projectid,
                };

                var b = _mapper.Map<PanoramaImage>(newPanoramaImage);
               
                b.Image = string.Join(",", imageUrls); // Store all image URLs as a comma-separated string
                _panorama.AddNew(b);
               
                return Ok(new
                {
                    message = "Tạo hình panorama thành công."
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "UpdatePanoramaImage")]
        public IActionResult UpdatePanoramaImage([FromForm] PanadoraImageUpdateDTO panogama, Guid id)
        {
            try
            {
                var imageUrls = panogama.Image!=null ?  _fileService.UploadMultipleImages(panogama.Image.ToList(), "panoramaimage") : new List<string>();

                var existingPanoram = _panorama.GetPanoramaImageById(id);
                if (existingPanoram != null)
                {

                    if (!string.IsNullOrEmpty(panogama.Title))
                    {
                        existingPanoram.Title = panogama.Title;
                    }
                    if (imageUrls.Count > 0)
                    {
                        existingPanoram.Image = string.Join(",", imageUrls);
                    }
                 
                    if (panogama.ProjectID.HasValue)
                    {
                        existingPanoram.ProjectID = panogama.ProjectID.Value;
                    }
                    _panorama.UpdatePanoramaImage(existingPanoram);
             

                    return Ok(new
                    {
                        message = "Cập nhật hình panorama thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Hình panorama không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
