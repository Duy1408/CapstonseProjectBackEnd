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
    public class FloorService : IFloorService
    {
        private readonly IFloorRepo _repo;
        public FloorService(IFloorRepo repo)
        {
            _repo = repo;
        }
        public void AddNew(Floor p)
        {
            _repo.AddNew(p);
        }

        public bool ChangeStatus(Floor p)
        {
            return _repo.ChangeStatus(p);
        }

        public List<Floor> GetFloorByBlockID(Guid id)
        {
            return _repo.GetFloorByBlockID(id);
        }

        public Floor GetFloorById(Guid id)
        {
            return _repo.GetFloorById(id);
        }

        public List<Floor> GetFloors()
        {
            return _repo.GetFloors();
        }

        public void UpdateFloor(Floor p)
        {
            _repo.UpdateFloor(p);
        }
    }
}
