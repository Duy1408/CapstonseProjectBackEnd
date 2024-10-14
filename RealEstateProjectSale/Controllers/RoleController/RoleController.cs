using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.RoleController
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleServices;
        private readonly IMapper _mapper;

        public RoleController(IRoleServices roleServices, IMapper mapper)
        {
            _roleServices = roleServices;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Role")]
        public IActionResult GetAllRole()
        {
            try
            {
                if (_roleServices.GetAllRole() == null)
                {
                    return NotFound(new
                    {
                        message = "Role not found."
                    });
                }
                var roles = _roleServices.GetAllRole();
                var response = _mapper.Map<List<RoleVM>>(roles);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get comment by ID")]
        public IActionResult GetRoleByID(Guid id)
        {
            var role = _roleServices.GetRoleByID(id);

            if (role != null)
            {
                var responese = _mapper.Map<RoleVM>(role);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Role not found."
            });

        }

    }
}
