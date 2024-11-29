using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSale.SwaggerResponses;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.BlockController
{
    [Route("api/blocks")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly IBlockService _block;
        private readonly IMapper _mapper;
        private readonly IFileUploadToBlobService _fileService;

        public BlocksController(IBlockService block, IMapper mapper, IFileUploadToBlobService fileService)
        {
            _block = block;
            _mapper = mapper;
            _fileService = fileService;

        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all Block")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách Block.", typeof(List<BlockVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Block không tồn tại.")]
        public IActionResult GetAllBlock()
        {
            try
            {
                if (_block.GetBlocks() == null)
                {
                    return NotFound(new
                    {
                        message = "Block không tồn tại."
                    });
                }
                var blocks = _block.GetBlocks();
                var response = _mapper.Map<List<BlockVM>>(blocks);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Block By ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về thông tin Block.", typeof(BlockVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Block không tồn tại.")]
        public IActionResult GetBlockByID(Guid id)
        {
            var block = _block.GetBlockById(id);

            if (block != null)
            {
                var responese = _mapper.Map<BlockVM>(block);
                return Ok(responese);
            }
            return NotFound(new
            {
                message = "Block không tồn tại."
            });
        }

        [HttpGet("zone/{zoneId}")]
        [SwaggerOperation(Summary = "Get Block By ZoneID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Trả về danh sách block.", typeof(List<BlockVM>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Block không tồn tại.")]
        public IActionResult GetBlockByZoneID(Guid zoneId)
        {
            var blocks = _block.GetBlockByZoneID(zoneId);

            if (blocks != null)
            {
                var responese = _mapper.Map<List<BlockVM>>(blocks);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Block không tồn tại."
            });

        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "UpdateBlock")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cập nhật Block thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Block không tồn tại.")]
        public IActionResult UpdateBlock([FromForm] BlockUpdateDTO block, Guid id)
        {
            try
            {
          
                var imageUrls = block.ImageBlock != null && block.ImageBlock.Count > 0
                  ? _fileService.UploadMultipleImages(block.ImageBlock.ToList(), "blockimage")
                     : new List<string>(); // Nếu không có hình ảnh, khởi tạo danh sách trống
                var existingBlock = _block.GetBlockById(id);
                if (existingBlock != null)
                {

                    if (!string.IsNullOrEmpty(block.BlockName))
                    {
                        existingBlock.BlockName = block.BlockName;
                    }
                    if (imageUrls.Count > 0)
                    {
                        existingBlock.ImageBlock = string.Join(",", imageUrls);
                    }
                    if (block.Status.HasValue)
                    {
                        existingBlock.Status = block.Status.Value;
                    }
                    if (block.ZoneID.HasValue)
                    {
                        existingBlock.ZoneID = block.ZoneID.Value;
                    }
                    _block.UpdateBlock(existingBlock);

                    return Ok(new
                    {
                        message = "Cập nhật Block thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Block không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Block")]
        [SwaggerResponse(StatusCodes.Status200OK, "Tạo Block thành công.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Yêu cầu không hợp lệ hoặc xảy ra lỗi xử lý.")]
        public IActionResult AddNewBlock([FromForm] BlockRequestDTO block)
        {
            try
            {
                var imageUrls = block.ImageBlock != null && block.ImageBlock.Count > 0
                   ? _fileService.UploadMultipleImages(block.ImageBlock.ToList(), "blockimage")
                      : new List<string>(); // Nếu không có hình ảnh, khởi tạo danh sách trống

                var newBlock = new BlockCreateDTO
                {
                    BlockID = Guid.NewGuid(),
                    BlockName = block.BlockName,
                    ImageBlock = block.ImageBlock != null && block.ImageBlock.Count > 0 ? block.ImageBlock.First() : null, // Lưu hình ảnh đầu tiên nếu có
                    Status = true,
                    ZoneID = block.ZoneID
                };

                var b = _mapper.Map<Block>(newBlock);
                //project.Image = blobUrl;
                b.ImageBlock = imageUrls.Count > 0 ? string.Join(",", imageUrls) : null;
                _block.AddNew(b);
                return Ok(new
                {
                    message = "Tạo Block thành công."
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Block")]
        [SwaggerResponse(StatusCodes.Status200OK, "Xóa Block thành công.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Block không tồn tại.")]
        public IActionResult DeleteAccount(Guid id)
        {

            var block = _block.GetBlockById(id);
            if (block == null)
            {
                return NotFound(new
                {
                    message = "Block không tồn tại."
                });
            }

            _block.ChangeStatus(block);

            return Ok(new
            {
                message = "Xóa Block thành công."
            });
        }



    }
}
