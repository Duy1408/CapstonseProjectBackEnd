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

        public IActionResult GetAllBlock()
        {
            try
            {
                if (_block.GetBlocks() == null)
                {
                    return NotFound();
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

        [HttpGet("GetBlockbyID/{id}")]
        public IActionResult GetBlockByID(Guid id)
        {
            var block = _block.GetBlockById(id);

            if (block != null)
            {
                var responese = _mapper.Map<BlockVM>(block);
                return Ok(responese);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "UpdateBlock")]
        public IActionResult UpdateBlock([FromForm] BlockUpdateDTO block, Guid id)
        {
            try
            {
                var imageUrls = block.ImageBlock != null ? _fileService.UploadMultipleImages(block.ImageBlock.ToList(), "blockimage") : new List<string>();

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
                        message = "Update Block Successfully"
                    });

                }

                return NotFound(new
                {
                    message = "Block not found."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Block")]
        public IActionResult AddNewBlock([FromForm] BlockRequestDTO block, Guid zoneId)
        {
            try
            {
                var imageUrls = _fileService.UploadMultipleImages(block.ImageBlock.ToList(), "blockimage");

                var newBlock = new BlockCreateDTO
                {
                    BlockID = Guid.NewGuid(),
                    BlockName = block.BlockName,
                    ImageBlock = block.ImageBlock.Count > 0 ? block.ImageBlock.First() : null, // Store first image for reference
                    Status = true,
                    ZoneID = zoneId,
                };

                var b = _mapper.Map<Block>(newBlock);
                //project.Image = blobUrl;
                b.ImageBlock = string.Join(",", imageUrls); // Store all image URLs as a comma-separated string
                _block.AddNew(b);
                return Ok(new
                {
                    message = "Create Block Successfully"
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("DeleteBlock/{id}")]
        public IActionResult DeleteBlock(Guid id)
        {
            if (_block.GetBlocks() == null)
            {
                return NotFound();
            }
            var block = _block.GetBlockById(id);
            if (block == null)
            {
                return NotFound();
            }

            _block.ChangeStatus(block);


            return Ok("Delete Successfully");
        }


    }
}
