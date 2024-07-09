using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class ContractDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public ContractDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<Contract> GetAllContract()
        {
            try
            {
                return _context.Contracts!.Include(c => c.Booking)
                                          .Include(c => c.PaymentProcess)
                                          .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewContract(Contract contract)
        {
            try
            {
                _context.Add(contract);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Contract GetContractByID(Guid id)
        {
            try
            {
                var contract = _context.Contracts!.Include(a => a.Booking)
                                                  .Include(a => a.PaymentProcess)
                                                  .SingleOrDefault(c => c.ContractID == id);
                return contract;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateContract(Contract contract)
        {
            try
            {
                var a = _context.Contracts!.SingleOrDefault(c => c.ContractID == contract.ContractID);

                _context.Entry(a).CurrentValues.SetValues(contract);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusContract(Contract contract)
        {
            var _contract = _context.Contracts!.FirstOrDefault(c => c.ContractID.Equals(contract.ContractID));


            if (_contract == null)
            {
                return false;
            }
            else
            {
                //_contract.Status = false;
                _context.Entry(_contract).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
