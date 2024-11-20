using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.UnitTypeController
{
    [Route("api/unit-types")]
    [ApiController]
    public class UnitTypeController : ControllerBase
    {
        private readonly IUnitTypeServices _typeService;
        private readonly IFileUploadToBlobService _fileService;
        private readonly IMapper _mapper;

        public UnitTypeController(IUnitTypeServices typeService, IFileUploadToBlobService fileService, IMapper mapper)
        {
            _typeService = typeService;
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All UnitType")]
        public IActionResult GetAllUnitType()
        {
            try
            {
                var types = _typeService.GetAllUnitType();

                if (types == null || !types.Any())
                {
                    return NotFound();
                }

                var response = types.Select(type => new UnitTypeVM
                {
                    UnitTypeID = type.UnitTypeID,
                    BathRoom = type.BathRoom,
                    BedRoom = type.BedRoom,
                    KitchenRoom = type.KitchenRoom,
                    LivingRoom = type.LivingRoom,
                    NumberFloor = type.NumberFloor,
                    Basement = type.Basement,
                    NetFloorArea = type.NetFloorArea,
                    GrossFloorArea = type.GrossFloorArea,
                    PropertyTypeID = type.PropertyTypeID,
                    PropertyTypeName = type.PropertyType?.PropertyTypeName,
                    Image = type.Image?.Split(',').ToList() ?? new List<string>(),
                    Status = type.Status
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get UnitType By ID")]
        public IActionResult GetUnitTypeByID(Guid id)
        {
            var type = _typeService.GetUnitTypeByID(id);

            if (type != null)
            {
                var response = new UnitTypeVM
                {
                    UnitTypeID = type.UnitTypeID,
                    BathRoom = type.BathRoom,
                    BedRoom = type.BedRoom,
                    KitchenRoom = type.KitchenRoom,
                    LivingRoom = type.LivingRoom,
                    NumberFloor = type.NumberFloor,
                    Basement = type.Basement,
                    NetFloorArea = type.NetFloorArea,
                    GrossFloorArea = type.GrossFloorArea,
                    PropertyTypeID = type.PropertyTypeID,
                    PropertyTypeName = type.PropertyType?.PropertyTypeName,
                    Image = type.Image?.Split(',').ToList() ?? new List<string>(),
                    Status = type.Status
                };

                return Ok(response);
            }

            return NotFound(new
            {
                message = "Chi tiết căn phòng không tồn tại."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new UnitType")]
        public IActionResult AddNewUnitType([FromForm] UnitTypeRequestDTO type)
        {
            try
            {
                var imageUrls = _fileService.UploadMultipleImages(type.Image.ToList(), "unittypeimage");

                var newCmt = new UnitTypeCreateDTO
                {
                    UnitTypeID = Guid.NewGuid(),
                    BathRoom = type.BathRoom,
                    BedRoom = type.BedRoom,
                    KitchenRoom = type.KitchenRoom,
                    LivingRoom = type.LivingRoom,
                    NumberFloor = type.NumberFloor,
                    Basement = type.Basement,
                    NetFloorArea = type.NetFloorArea,
                    GrossFloorArea = type.GrossFloorArea,
                    Status = true,
                    PropertyTypeID = type.PropertyTypeID,
                    Image = type.Image.Count > 0 ? type.Image.First() : null
                };

                var unitType = _mapper.Map<UnitType>(newCmt);

                unitType.Image = string.Join(",", imageUrls);

                _typeService.AddNewUnitType(unitType);

                return Ok(new
                {
                    message = "Tạo chi tiết căn phòng thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update UnitType")]
        public IActionResult UpdateUnitType([FromForm] UnitTypeUpdateDTO type, Guid id)
        {
            try
            {
                var imageUrls = type.Image != null ? _fileService.UploadMultipleImages(type.Image.ToList(), "unittypeimage") : new List<string>();

                var existingType = _typeService.GetUnitTypeByID(id);
                if (existingType != null)
                {

                    if (type.BathRoom.HasValue)
                    {
                        existingType.BathRoom = type.BathRoom.Value;
                    }
                    if (type.BedRoom.HasValue)
                    {
                        existingType.BedRoom = type.BedRoom.Value;
                    }
                    if (type.KitchenRoom.HasValue)
                    {
                        existingType.KitchenRoom = type.KitchenRoom.Value;
                    }
                    if (type.LivingRoom.HasValue)
                    {
                        existingType.LivingRoom = type.LivingRoom.Value;
                    }
                    if (type.NumberFloor.HasValue)
                    {
                        existingType.NumberFloor = type.NumberFloor.Value;
                    }
                    if (type.Basement.HasValue)
                    {
                        existingType.Basement = type.Basement.Value;
                    }
                    if (type.NetFloorArea.HasValue)
                    {
                        existingType.NetFloorArea = type.NetFloorArea.Value;
                    }
                    if (type.GrossFloorArea.HasValue)
                    {
                        existingType.GrossFloorArea = type.GrossFloorArea.Value;
                    }
                    if (imageUrls.Count > 0)
                    {
                        existingType.Image = string.Join(",", imageUrls);
                    }
                    if (type.Status.HasValue)
                    {
                        existingType.Status = type.Status.Value;
                    }
                    if (type.PropertyTypeID.HasValue)
                    {
                        existingType.PropertyTypeID = type.PropertyTypeID.Value;
                    }

                    _typeService.UpdateUnitType(existingType);

                    return Ok(new
                    {
                        message = "Cập nhật chi tiết căn phòng thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Chi tiết căn phòng không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete UnitType by ID")]
        public IActionResult DeleteUnitType(Guid id)
        {
            if (_typeService.GetUnitTypeByID(id) == null)
            {
                return NotFound(new
                {
                    message = "Chi tiết căn phòng không tồn tại."
                });
            }
            var type = _typeService.GetUnitTypeByID(id);
            if (type == null)
            {
                return NotFound(new
                {
                    message = "Chi tiết căn phòng không tồn tại."
                });
            }

            _typeService.ChangeStatusUnitType(type);


            return Ok(new
            {
                message = "Xóa chi tiết căn phòng thành công."
            });
        }

    }
}
