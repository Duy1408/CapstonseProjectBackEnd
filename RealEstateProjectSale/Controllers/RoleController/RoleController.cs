using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;

namespace RealEstateProjectSale.Controllers.RoleController
{
    [Route("api/[controller]")]
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
        public IActionResult GetAllRole()
        {
            try
            {
                if (_roleServices.GetAllRole() == null)
                {
                    return NotFound();
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
        public IActionResult GetRoleByID(Guid id)
        {
            var role = _roleServices.GetRoleByID(id);

            if (role != null)
            {
                var responese = _mapper.Map<RoleVM>(role);

                return Ok(responese);
            }

            return NotFound();

        }

    }
}
