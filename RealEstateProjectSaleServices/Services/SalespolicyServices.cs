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
    public class SalespolicyServices : ISalespolicyServices
    {
        private ISalespolicyRepo _repo;
        public SalespolicyServices(ISalespolicyRepo repo)
        {
            _repo = repo;
        }
        public void AddNew(Salespolicy p)
        {
            _repo.AddNew(p);
        }

        public bool ChangeStatus(Salespolicy p)
        {
          return   _repo.ChangeStatus(p);
        }

        public Salespolicy GetSalespolicyById(Guid id)
        {
            return _repo.GetSalespolicyById(id);
        }

        public Salespolicy GetSalespolicyByProjectID(Guid projectid)
        {
            return _repo.GetSalespolicyByProjectID(projectid);
        }

        public List<Salespolicy> GetSalespolicys()
        {
            return _repo.GetSalespolicys();
        }

        public void UpdateSalespolicy(Salespolicy p)
        {
            _repo.UpdateSalespolicy(p);
        }
    }
}
