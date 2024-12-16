using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class ContractHistoryDAO
    {

        private static ContractHistoryDAO instance;

        public static ContractHistoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContractHistoryDAO();
                }
                return instance;
            }


        }

        public List<ContractHistory> GetAllContractHistory()
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.ContractHistories.Include(c => c.Contract).Include(c => c.Customer).ToList();
        }

        public bool AddNewContractHistory(ContractHistory c)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.ContractHistories.SingleOrDefault(c => c.ContractHistoryID == c.ContractHistoryID);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.ContractHistories.Add(c);
                _context.SaveChanges();
                return true;

            }
        }

        public ContractHistory GetContractHistoryByID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.ContractHistories.Include(c => c.Contract)
                .Include(c=> c.Customer).SingleOrDefault(a => a.ContractHistoryID == id);

        }

        public List<ContractHistory> GetBlockByContractID(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            return _context.ContractHistories.Include(c => c.Contract)
                                         .Where(a => a.ContractID == id)
                                         .ToList();
        }

        public bool UpdateContractHistory(ContractHistory c)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var a = _context.ContractHistories.SingleOrDefault(c => c.ContractHistoryID == c.ContractHistoryID);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(c);
                _context.SaveChanges();
                return true;
            }
        }


        public void DeleteContractHistory(Guid id)
        {
            var _context = new RealEstateProjectSaleSystemDBContext();
            var contracthistory = _context.ContractHistories
                       .SingleOrDefault(lo => lo.ContractHistoryID == id);
            if (contracthistory != null)
            {
                _context.Remove(contracthistory);
                _context.SaveChanges();
            }
        }
    }
    }

