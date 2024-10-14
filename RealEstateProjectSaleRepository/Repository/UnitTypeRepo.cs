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
    public class UnitTypeRepo : IUnitTypeRepo
    {
        UnitTypeDAO dao = new UnitTypeDAO();

        public void AddNewUnitType(UnitType type) => dao.AddNewUnitType(type);

        public bool ChangeStatusUnitType(UnitType type) => dao.ChangeStatusUnitType(type);

        public List<UnitType> GetAllUnitType() => dao.GetAllUnitType();

        public UnitType GetUnitTypeByID(Guid id) => dao.GetUnitTypeByID(id);

        public void UpdateUnitType(UnitType type) => dao.UpdateUnitType(type);

    }
}
