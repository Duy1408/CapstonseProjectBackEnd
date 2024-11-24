
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class BlockDAO
    {
        private static BlockDAO instance;

        public static BlockDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BlockDAO();
                }
                return instance;
            }


        }

        public List<Block> GetAllBlock()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Blocks.Include(c => c.Zone).ToList();
        }




        public bool AddNew(Block b)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Blocks.SingleOrDefault(c => c.BlockID == b.BlockID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Blocks.Add(b);
                _context.SaveChanges();
                return true;

            }
        }

        public Block GetBlockByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Blocks.Include(c => c.Zone).SingleOrDefault(a => a.BlockID == id);
        }

        public List<Block> GetBlockByZoneID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.Blocks.Include(c => c.Zone)
                                         .Where(a => a.ZoneID == id)
                                         .ToList();
        }

        public bool UpdateBlock(Block b)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Blocks.SingleOrDefault(c => c.BlockID == b.BlockID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(b);
                _context.SaveChanges();
                return true;
            }
        }

        public bool ChangeStatus(Block p)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.Blocks.FirstOrDefault(c => c.BlockID.Equals(p.BlockID));


            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
