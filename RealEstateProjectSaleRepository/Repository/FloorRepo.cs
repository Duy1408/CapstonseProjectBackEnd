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
            _dao.AddNew(p);
        }

        public bool ChangeStatus(Floor p)
        {
            return _dao.ChangeStatus(p);
        }

        public Floor GetFloorById(Guid id)
        {
            return _dao.GetFloorByID(id);
        }

        public List<Floor> GetFloors()
        {
            return _dao.GetAllFloor();
        }

        public void UpdateFloor(Floor p)
        {
            _dao.UpdateFloor(p);
        }
    }
}
