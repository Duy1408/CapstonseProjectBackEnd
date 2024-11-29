using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IFloorRepo
    {
        bool ChangeStatus(Floor p);
        List<Floor> GetFloors();
        void AddNew(Floor p);
        Floor GetFloorById(Guid id);
        List<Floor> GetFloorByBlockID(Guid id);
        void UpdateFloor(Floor p);
        Floor CheckExistFloorByNum(int num, Guid blockid);
    }
}
