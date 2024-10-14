using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleRepository.IRepository
{
    public interface IUnitTypeRepo
    {
        List<UnitType> GetAllUnitType();
        UnitType GetUnitTypeByID(Guid id);
        void AddNewUnitType(UnitType type);
        void UpdateUnitType(UnitType type);
        bool ChangeStatusUnitType(UnitType type);

    }
}
