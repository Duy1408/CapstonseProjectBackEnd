using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
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
                var contracts = _context.Contracts!.Include(c => c.Booking)
                                          .Include(c => c.DocumentTemplate)
                                          .Include(c => c.PaymentProcess)
                                          .Include(c => c.PromotionDetail)
                                          .Include(c => c.Customer)
                                          .ToList();

                var sortedContracts = contracts.OrderByDescending(c =>
                {
                    var contractNumber = c.ContractCode.Split('/')[0];  // Lấy phần số trước dấu "/"
                    return int.TryParse(contractNumber, out int number) ? number : 0;
                }).ToList();

                return sortedContracts;
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
                                                  .Include(a => a.DocumentTemplate)
                                                  .Include(a => a.PaymentProcess)
                                                  .Include(c => c.PromotionDetail)
                                                  .Include(c => c.Customer)
                                                  .SingleOrDefault(c => c.ContractID == id);
                return contract;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Contract> GetContractByCustomerID(Guid id)
        {
            try
            {
                var contracts = _context.Contracts!.Include(a => a.Booking)
                                                   .ThenInclude(a => a.ProjectCategoryDetail)
                                                   .ThenInclude(a => a.Project)
                                                   .Include(b => b.Booking)
                                                    .ThenInclude(b => b.Property)
                                                   .Include(a => a.DocumentTemplate)
                                                   .Include(a => a.PaymentProcess)
                                                   .Include(c => c.PromotionDetail)
                                                   .Include(c => c.Customer)
                                                   .Where(c => c.CustomerID == id && c.Status != ContractStatus.DaHuy.GetEnumDescription())
                                                   .ToList();
                return contracts;
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
                _contract.Status = ContractStatus.DaHuy.GetEnumDescription();
                _context.Entry(_contract).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

        public Contract GetLastContract()
        {
            return _context.Contracts
                           .OrderByDescending(c => c.CreatedTime)
                           .FirstOrDefault();
        }

    }
}
