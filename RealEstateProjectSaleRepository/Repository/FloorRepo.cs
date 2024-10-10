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
    public class FloorRepo : IFloorRepo
    {

        private FloorDAO _dao;
        public FloorRepo()
        {
            _dao = new FloorDAO();
        }


        public void AddNew(Floor p)
        {
            throw new NotImplementedException();
        }

        public bool ChangeStatus(Floor p)
        {
            throw new NotImplementedException();
        }

        public Floor GetFloorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Floor> GetFloors()
        {
            throw new NotImplementedException();
        }

        public void UpdateFloor(Floor p)
        {
            throw new NotImplementedException();
        }
    }
}
