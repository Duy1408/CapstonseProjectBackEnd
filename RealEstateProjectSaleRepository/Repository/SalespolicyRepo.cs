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
    public class SalespolicyRepo : ISalespolicyRepo
    {
        private SalespolicyDAO _dao;
        public SalespolicyRepo()
        {
            _dao = new SalespolicyDAO();
        }
        public void AddNew(Salespolicy p)
        {
            _dao.AddNew(p);
        }

        public bool ChangeStatus(Salespolicy p)
        {
            return _dao.ChangeStatus(p);
        }

        public Salespolicy GetSalespolicyById(Guid id)
        {
            return _dao.GetSalespolicyByID(id);
        }

        public List<Salespolicy> GetSalespolicys()
        {
            return _dao.GetAllSalespolicy();
        }

        public void UpdateSalespolicy(Salespolicy p)
        {
            _dao.UpdateSalespolicy(p);
        }
    }
}
