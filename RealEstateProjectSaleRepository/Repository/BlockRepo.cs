using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleDAO.DAOs;
using RealEstateProjectSaleRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.Repository
{
    public class BlockRepo : IBlockRepo
    {
        private BlockDAO _dao;
        public BlockRepo()
        {
            _dao = new BlockDAO();
        }
        public void AddNew(Block p)
        {
            _dao.AddNew(p);
        }

        public bool ChangeStatus(Block p)
        {
            return _dao.ChangeStatus(p);
        }

        public Block GetBlockById(Guid id)
        {
            return _dao.GetBlockByID(id);
        }

        public List<Block> GetBlocks()
        {
            return _dao.GetAllBlock();
        }

        public void UpdateBlock(Block p)
        {
            _dao.UpdateBlock(p);
        }
    }
}
