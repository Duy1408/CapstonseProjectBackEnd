using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleDAO.DAOs
{
    public class ContractPaymentDetailDAO
    {
        private readonly RealEstateProjectSaleSystemDBContext _context;
        public ContractPaymentDetailDAO()
        {
            _context = new RealEstateProjectSaleSystemDBContext();
        }

        public List<ContractPaymentDetail> GetAllContractPaymentDetail()
        {
            try
            {
                return _context.ContractPaymentDetails!.Include(c => c.Contract)
                                                       .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewContractPaymentDetail(ContractPaymentDetail detail)
        {
            try
            {
                _context.Add(detail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ContractPaymentDetail GetContractPaymentDetailByID(Guid id)
        {
            try
            {
                var detail = _context.ContractPaymentDetails!.Include(a => a.Contract)
                                                               .SingleOrDefault(c => c.ContractPaymentDetailID == id);
                return detail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateContractPaymentDetail(ContractPaymentDetail detail)
        {
            try
            {
                var a = _context.ContractPaymentDetails!.SingleOrDefault(c => c.ContractPaymentDetailID == detail.ContractPaymentDetailID);

                _context.Entry(a).CurrentValues.SetValues(detail);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteContractPaymentDetailByID(Guid id)
        {
            var detail = _context.ContractPaymentDetails!.SingleOrDefault(lo => lo.ContractPaymentDetailID == id);
            if (detail != null)
            {
                _context.Remove(detail);
                _context.SaveChanges();
            }
        }

    }
}
