using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.BlockController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly IBlockService _block;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;

        public BlocksController(IBlockService block, IMapper mapper, BlobServiceClient blobServiceClient)
        {
            _block = block;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;
            
        }

        [HttpGet]
        [Route("GetAllBlock")]
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

        [HttpDelete("DeleteBlock/{id}")]
        public IActionResult DeleteBlock(Guid id)
        {
            if (_block.GetBlocks == null)
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
