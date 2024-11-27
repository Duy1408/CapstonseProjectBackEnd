using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;
using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;
using Contract = RealEstateProjectSaleBusinessObject.BusinessObject.Contract;

namespace RealEstateProjectSale.Controllers.PropertyController
{
    [Route("api/propertys")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyServices _pro;
        private readonly IPagingServices _pagingServices;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IHubContext<PropertyHub> _hubContext;
        private readonly IContractServices _contractServices;
        private readonly IFileUploadToBlobService _fileService;
        private readonly IBookingServices _booking;
        private readonly ICustomerServices _customerService;
        private readonly IOpenForSaleDetailServices _openDetailService;
        private readonly IDocumentTemplateService _documentService;

        public static int PAGE_SIZE { get; set; } = 5;

        public PropertyController(IHubContext<PropertyHub> hubContext, IPropertyServices pro,
            IPagingServices pagingServices, BlobServiceClient blobServiceClient, IMapper mapper,
            IContractServices contractServices, IFileUploadToBlobService fileService, IBookingServices booking,
            ICustomerServices customerService, IOpenForSaleDetailServices openDetailService,
            IDocumentTemplateService documentService)
        {
            _pro = pro;
            _pagingServices = pagingServices;
            _blobServiceClient = blobServiceClient;
            _mapper = mapper;
            _hubContext = hubContext;
            _contractServices = contractServices;
            _fileService = fileService;
            _booking = booking;
            _customerService = customerService;
            _openDetailService = openDetailService;
            _documentService = documentService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Property")]
        public IActionResult GetAllProperty([FromQuery] string? propertyCode, [FromQuery] Guid? zoneID, [FromQuery] Guid? blockID, [FromQuery] Guid? floorID, [FromQuery] Guid? projectcategorydetailID, [FromQuery] int page = 1)
        {
            try
            {
                var propertysQuery = _pro.GetProperty().AsQueryable();

                if (propertysQuery == null || !propertysQuery.Any())
                {
                    return NotFound(new
                    {
                        message = "Căn không tồn tại."
                    });
                }

                if (!string.IsNullOrEmpty(propertyCode))
                {
                    propertysQuery = propertysQuery.Where(p => p.PropertyCode.Contains(propertyCode));
                }
                if (zoneID.HasValue)
                {
                    propertysQuery = propertysQuery.Where(p => p.ZoneID == zoneID.Value);
                }
                if (blockID.HasValue)
                {
                    propertysQuery = propertysQuery.Where(p => p.BlockID == blockID.Value);
                }
                if (floorID.HasValue)
                {
                    propertysQuery = propertysQuery.Where(p => p.FloorID == floorID.Value);
                }
                if (projectcategorydetailID.HasValue)
                {
                    propertysQuery = propertysQuery.Where(p => p.ProjectCategoryDetailID == projectcategorydetailID.Value);
                }

                var pagedResult = _pagingServices.GetPagedList(propertysQuery, page, PAGE_SIZE);

                if (pagedResult.Items == null || !pagedResult.Items.Any())
                {
                    return NotFound(new
                    {
                        message = "Căn không tồn tại."
                    });
                }

                var response = _mapper.Map<List<PropertyVM>>(pagedResult.Items);


                return Ok(new
                {
                    TotalPages = pagedResult.TotalPages,
                    CurrentPage = pagedResult.CurrentPage,
                    Propertys = response
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Property By ID")]
        public IActionResult GetPropertyByID(Guid id)
        {
            var property = _pro.GetPropertyById(id);

            if (property != null)
            {
                var responese = _mapper.Map<PropertyVM>(property);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Căn này không tồn tại."
            });

        }

        [HttpGet("unitType/{unitTypeID}")]
        [SwaggerOperation(Summary = "Get Property By UnitTypeID")]
        public IActionResult GetPropertyByUnitTypeID(Guid unitTypeID)
        {
            var property = _pro.GetPropertyByUnitTypeID(unitTypeID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "Căn này không tồn tại."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Căn này không tồn tại."
            });

        }

        [HttpGet("zone/{zoneID}")]
        [SwaggerOperation(Summary = "Get Property By ZoneID")]
        public IActionResult GetPropertyByZoneID(Guid zoneID)
        {
            var property = _pro.GetPropertyByZoneID(zoneID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "Căn này không tồn tại."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Căn này không tồn tại."
            });

        }

        [HttpGet("block/{blockID}")]
        [SwaggerOperation(Summary = "Get Property By BlockID")]
        public IActionResult GetPropertyByBlockID(Guid blockID)
        {
            var property = _pro.GetPropertyByBlockID(blockID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "Căn này không tồn tại."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Căn này không tồn tại."
            });

        }

        [HttpGet("floor/{floorID}")]
        [SwaggerOperation(Summary = "Get Property By FloorID")]
        public IActionResult GetPropertyByFloorID(Guid floorID)
        {
            var property = _pro.GetPropertyByFloorID(floorID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "Căn này không tồn tại."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Căn này không tồn tại."
            });

        }

        [HttpGet("categoryDetail/{categoryDetailID}")]
        [SwaggerOperation(Summary = "Get Property By ProjectCategoryDetailID")]
        public IActionResult GetPropertyByProjectCategoryDetailID(Guid categoryDetailID)
        {
            var property = _pro.GetPropertyByProjectCategoryDetailID(categoryDetailID);

            if (property != null)
            {
                var responese = property.Select(property => _mapper.Map<PropertyVM>(property)).ToList();

                if (responese.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "Căn này không tồn tại."
                    });
                }

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Căn này không tồn tại."
            });

        }

        [HttpGet("property-not-sale/{categoryDetailID}")]
        [SwaggerOperation(Summary = "Get Property Not Sale By CategoryDetailID")]
        public IActionResult GetPropertyNotSaleByCategoryDetailID(Guid categoryDetailID)
        {
            var property = _pro.GetPropertyNotSaleByCategoryDetailID(categoryDetailID);

            if (property != null)
            {
                var responese = _mapper.Map<List<PropertyVM>>(property);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Căn này không tồn tại."
            });

        }

        [HttpGet("search")]
        [SwaggerOperation(Summary = "Search Property By Name")]
        public ActionResult<Property> SearchPropertyByName(string searchValue)
        {
            if (_pro.GetProperty() == null)
            {
                return NotFound(new
                {
                    message = "Căn này không tồn tại."
                });
            }
            var property = _pro.SearchPropertyByName(searchValue);

            if (property == null)
            {
                return NotFound(new
                {
                    message = "Căn này không tồn tại."
                });
            }

            return Ok(property);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new Property")]
        public IActionResult AddNewProperty(PropertyCreateDTO property)
        {
            try
            {

                var newProperty = new PropertyCreateDTO
                {
                    PropertyID = Guid.NewGuid(),
                    PropertyCode = property.PropertyCode,
                    View = property.View,
                    PriceSold = property.PriceSold,
                    Status = PropertyStatus.ChuaBan.GetEnumDescription(),
                    UnitTypeID = property.UnitTypeID,
                    FloorID = property.FloorID,
                    BlockID = property.BlockID,
                    ZoneID = property.ZoneID,
                    ProjectCategoryDetailID = property.ProjectCategoryDetailID,
                };

                var _property = _mapper.Map<Property>(newProperty);
                _pro.AddNew(_property);

                return Ok(new
                {
                    message = "Tạo căn thành công."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Property by ID")]
        public IActionResult UpdateProperty([FromForm] PropertyUpdateDTO property, Guid id)
        {
            try
            {

                var existingProperty = _pro.GetPropertyById(id);
                if (existingProperty != null)
                {
                    if (!string.IsNullOrEmpty(property.PropertyCode))
                    {
                        existingProperty.PropertyCode = property.PropertyCode;
                    }
                    if (!string.IsNullOrEmpty(property.View))
                    {
                        existingProperty.View = property.View;
                    }
                    if (property.PriceSold.HasValue)
                    {
                        existingProperty.PriceSold = property.PriceSold.Value;
                    }
                    if (!string.IsNullOrEmpty(property.Status) && int.TryParse(property.Status, out int statusValue))
                    {
                        if (Enum.IsDefined(typeof(PropertyStatus), statusValue))
                        {
                            var statusEnum = (PropertyStatus)statusValue;
                            var statusDescription = statusEnum.GetEnumDescription();
                            existingProperty.Status = statusDescription;
                        }
                    }
                    if (property.UnitTypeID.HasValue)
                    {
                        existingProperty.UnitTypeID = property.UnitTypeID.Value;
                    }
                    if (property.FloorID.HasValue)
                    {
                        existingProperty.FloorID = property.FloorID.Value;
                    }
                    if (property.BlockID.HasValue)
                    {
                        existingProperty.BlockID = property.BlockID.Value;
                    }
                    if (property.ZoneID.HasValue)
                    {
                        existingProperty.ZoneID = property.ZoneID.Value;
                    }
                    if (property.ProjectCategoryDetailID.HasValue)
                    {
                        existingProperty.ProjectCategoryDetailID = property.ProjectCategoryDetailID.Value;
                    }
                    _pro.UpdateProperty(existingProperty);

                    return Ok(new
                    {
                        message = "Cập nhật căn thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Căn này không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut("select")]
        [SwaggerOperation(Summary = "Customer select Property after check in")]
        public async Task<IActionResult> UpdateStatusProperty(Guid propertyId, Guid customerID)
        {
            try
            {
                var existingCustomer = _customerService.GetCustomerByID(customerID);
                if (existingCustomer == null)
                {
                    return NotFound(new
                    {
                        message = "Khách hàng không tồn tại."
                    });
                }

                var existingProperty = _pro.GetPropertyById(propertyId);
                if (existingProperty == null)
                {
                    return NotFound(new
                    {
                        message = "Căn này không tồn tại."
                    });
                }

                var categoryDetailId = existingProperty.ProjectCategoryDetailID.GetValueOrDefault(Guid.Empty);

                if (categoryDetailId == Guid.Empty)
                {
                    throw new ArgumentException("Căn hộ không có Chi tiết dự án.");
                }

                var booking = _booking.GetBookingByCustomerSelect(customerID, categoryDetailId);
                if (booking == null)
                {
                    return NotFound(new
                    {
                        message = "Đặt giữ chỗ không tồn tại."
                    });
                }

                var openDetail = _openDetailService.GetDetailByPropertyIdOpenId(propertyId, booking.OpeningForSaleID);
                if (openDetail == null)
                {
                    return NotFound(new
                    {
                        message = "Căn hiện tại chưa được mở bán."
                    });
                }
                var documentReservation = _documentService.GetDocumentByDocumentName("Thỏa thuận đặt cọc");
                if (documentReservation == null)
                {
                    return NotFound(new
                    {
                        message = "Hợp đồng không tồn tại"
                    });
                }

                booking.PropertyID = propertyId;
                booking.Status = BookingStatus.DaChonSanPham.GetEnumDescription();
                _booking.UpdateBooking(booking);

                string nextContractCode = GenerateNextContractCode();
                var newContract = new ContractCreateDTO
                {
                    ContractID = Guid.NewGuid(),
                    ContractCode = nextContractCode,
                    ContractType = ContractType.DatCoc.GetEnumDescription(),
                    CreatedTime = DateTime.Now,
                    UpdatedTime = null,
                    ExpiredTime = DateTime.Now.AddDays(1),
                    TotalPrice = openDetail.Price,
                    Description = null,
                    ContractDepositFile = null,
                    ContractSaleFile = null,
                    PriceSheetFile = null,
                    ContractTransferFile = null,
                    Status = ContractStatus.ChoXacNhanTTGD.GetEnumDescription(),
                    DocumentTemplateID = documentReservation.DocumentTemplateID,
                    BookingID = booking.BookingID,
                    CustomerID = customerID,
                    PaymentProcessID = null,
                    PromotionDetailID = null
                };

                var _contract = _mapper.Map<Contract>(newContract);
                _contractServices.AddNewContract(_contract);

                existingProperty.Status = PropertyStatus.GiuCho.GetEnumDescription();
                existingProperty.PriceSold = openDetail.Price;
                _pro.UpdateProperty(existingProperty);

                await _hubContext.Clients.All.SendAsync("ReceivePropertyStatus", propertyId.ToString(), existingProperty.Status);
                return Ok(new
                {
                    message = "Cập nhật căn thành công."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Property by ID")]
        public IActionResult DeleteProperty(Guid id)
        {

            var property = _pro.GetPropertyById(id);
            if (property == null)
            {
                return NotFound(new
                {
                    message = "Căn hộ không tồn tại."
                });
            }

            _pro.ChangeStatusProperty(property);


            return Ok(new
            {
                message = "Xóa hợp đồng thành công"
            });
        }

        private string GenerateNextContractCode()
        {
            // Lấy số hợp đồng hiện tại (có thể từ DB hoặc cache)
            var lastContract = _contractServices.GetLastContract();

            int nextNumber = 1;

            // Nếu có hợp đồng trước đó, lấy số lớn nhất và tăng lên
            if (lastContract != null)
            {
                string lastCode = lastContract.ContractCode.Split('/')[0];  // Lấy phần số trước dấu "/"
                int.TryParse(lastCode, out nextNumber);
                nextNumber++;  // Tăng số lên
            }

            // Định dạng mã hợp đồng, số có 4 chữ số kèm phần định danh "/TTĐC"
            string nextContractCode = nextNumber.ToString() + "/HD";

            return nextContractCode;
        }



    }




}
