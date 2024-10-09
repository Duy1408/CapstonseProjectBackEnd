using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IBlockRepo
    {
         bool ChangeStatus(Block p);


         List<Block> GetBlocks();
         void AddNew(Block p);


         Block GetBlockById(Guid id);

         void UpdateBlock(Block p);
    }
}
