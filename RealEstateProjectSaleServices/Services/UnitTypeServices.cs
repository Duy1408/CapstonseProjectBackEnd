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
    public class UnitTypeServices : IUnitTypeServices
    {
        private readonly IUnitTypeRepo _typeRepo;
        public UnitTypeServices(IUnitTypeRepo typeRepo)
        {
            _typeRepo = typeRepo;
        }
        public void AddNewUnitType(UnitType type) => _typeRepo.AddNewUnitType(type);

        public bool ChangeStatusUnitType(UnitType type) => _typeRepo.ChangeStatusUnitType(type);

        public List<UnitType> GetAllUnitType() => _typeRepo.GetAllUnitType();

        public UnitType GetUnitTypeByID(Guid id) => _typeRepo.GetUnitTypeByID(id);

        public void UpdateUnitType(UnitType type) => _typeRepo.UpdateUnitType(type);

    }
}
