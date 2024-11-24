using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.SalespolicyController
{
    [Route("api/sales-policys")]
    [ApiController]
    public class SalespoliciesController : ControllerBase
    {
        private readonly ISalespolicyServices _sale;
        private readonly IProjectServices _projectService;
        private readonly IMapper _mapper;

        public SalespoliciesController(ISalespolicyServices sale, IMapper mapper, IProjectServices projectService)
        {
            _sale = sale;
            _projectService = projectService;
            _mapper = mapper;
        }

        // GET: api/Salespolicies
        [HttpGet]
        [SwaggerOperation(Summary = "Get All SalePolicy")]
        public IActionResult GetAllSalePolicy()
        {
            try
            {
                if (_sale.GetSalespolicys() == null)
                {
                    return NotFound(new
                    {
                        message = "Chính sách bán hàng không tồn tại."
                    });
                }
                var sales = _sale.GetSalespolicys();
                var response = _mapper.Map<List<SalepolicyVM>>(sales);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Salespolicies/5

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get SalePolicy by ID")]
        public IActionResult GetSalePolicyByID(Guid id)
        {
            var sale = _sale.GetSalespolicyById(id);

            if (sale != null)
            {
                var responese = _mapper.Map<SalepolicyVM>(sale);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chính sách bán hàng không tồn tại."
            });

        }

        [HttpGet("project/{projectId}")]
        [SwaggerOperation(Summary = "Get SalePolicy by project ID")]
        public IActionResult GetSalePolicyByProjectID(Guid projectId)
        {
            var sale = _sale.GetSalespolicyByProjectID(projectId);

            if (sale != null)
            {
                var responese = _mapper.Map<List<SalepolicyVM>>(sale);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chính sách bán hàng không tồn tại."
            });

        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update SalePolicy by ID")]
        public IActionResult UpdateSalePolicy([FromForm] SalePolicyUpdateDTO sale, Guid id)
        {
            try
            {
                var existingSale = _sale.GetSalespolicyById(id);
                if (existingSale != null)
                {

                    if (!string.IsNullOrEmpty(sale.SalesPolicyType))
                    {
                        existingSale.SalesPolicyType = sale.SalesPolicyType;
                    }
                    if (!string.IsNullOrEmpty(sale.PeopleApplied))
                    {
                        existingSale.PeopleApplied = sale.PeopleApplied;
                    }
                    if (sale.Status.HasValue)
                    {
                        existingSale.Status = sale.Status.Value;
                    }
                    if (sale.ExpressTime.HasValue)
                    {
                        existingSale.ExpressTime = sale.ExpressTime.Value;
                    }
                    if (sale.ProjectID.HasValue)
                    {
                        existingSale.ProjectID = sale.ProjectID.Value;
                    }


                    _sale.UpdateSalespolicy(existingSale);

                    return Ok(new
                    {
                        message = "Cập nhật chính sách bàn hàng thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Chính sách bán hàng không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new SalePolicy")]
        public IActionResult AddNew(SalepolicyCreateDTO sale)
        {
            try
            {
                var existingProject = _projectService.GetProjectById(sale.ProjectID);
                if (existingProject == null)
                {
                    return NotFound(new
                    {
                        message = "Dự án không tồn tại."
                    });
                }

                var existingSale = _sale.FindByProjectIdAndStatus(sale.ProjectID);
                if (existingSale != null)
                {
                    return BadRequest(new
                    {
                        message = "Chính sách bán hàng của dự án này đã tồn tại."
                    });
                }

                var newSale = new SalepolicyCreateDTO
                {
                    SalesPolicyID = Guid.NewGuid(),
                    SalesPolicyType = sale.SalesPolicyType,
                    ExpressTime = DateTime.Now.Date,
                    PeopleApplied = null,
                    Status = true,
                    ProjectID = sale.ProjectID,
                };

                var salepolicy = _mapper.Map<Salespolicy>(newSale);

                _sale.AddNew(salepolicy);

                return Ok(new
                {
                    message = "Tạo chính sách bàn hàng thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete SalePolicy by ID")]
        public IActionResult DeleteSalePolicy(Guid id)
        {
            if (_sale.GetSalespolicys() == null)
            {
                return NotFound(new
                {
                    message = "Chính sách bán hàng không tồn tại."
                });
            }
            var sale = _sale.GetSalespolicyById(id);
            if (sale == null)
            {
                return NotFound(new
                {
                    message = "Chính sách bán hàng không tồn tại."
                });
            }

            _sale.ChangeStatus(sale);


            return Ok(new
            {
                message = "Xóa chính sách bàn hàng thành công."
            });
        }



    }
}
