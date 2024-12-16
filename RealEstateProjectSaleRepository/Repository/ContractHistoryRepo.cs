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
    public class ContractHistoryRepo : IContractHistoryRepo
    {
        private ContractHistoryDAO _dao;
        public ContractHistoryRepo()
        {
            _dao = new ContractHistoryDAO();        }
        public void AddNewContractHistory(ContractHistory p)
        {
            _dao.AddNewContractHistory(p);
        }

        public void DeleteContractHistory(Guid id)
        {
            _dao.DeleteContractHistory(id);
        }

        public List<ContractHistory> GetContractHistoryByContractID(Guid id)
        {
            return _dao.GetBlockByContractID(id);
        }

        public ContractHistory GetContractHistoryById(Guid id)
        {
            return _dao.GetContractHistoryByID(id);
        }

        public List<ContractHistory> GetContractHistorys()
        {
            return _dao.GetAllContractHistory();
        }

        public void UpdateContractHistory(ContractHistory p)
        {
            _dao.UpdateContractHistory(p);
        }
    }
}
