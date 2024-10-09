using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class BlockService : IBlockService
    {
        private readonly IBlockRepo _block;
        public BlockService(IBlockRepo block)
        {
            _block = block;
        }
        public void AddNew(Block p)
        {
            _block.AddNew(p);
        }

        public bool ChangeStatus(Block p)
        {
            return _block.ChangeStatus(p);
        }

        public Block GetBlockById(Guid id)
        {
            return _block.GetBlockById(id);
        }

        public List<Block> GetBlocks()
        {
            return _block.GetBlocks();
        }

        public void UpdateBlock(Block p)
        {
            _block.UpdateBlock(p);
        }
    }
}
