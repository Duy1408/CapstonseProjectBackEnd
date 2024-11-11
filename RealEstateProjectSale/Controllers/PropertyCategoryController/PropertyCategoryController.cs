using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Stripe;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PropertyCategoryController
{
    [Route("api/property-categorys")]
    [ApiController]
    public class PropertyCategoryController : ControllerBase
    {
        private readonly IPropertyCategoryServices _categoryServices;
        private readonly IMapper _mapper;

        public PropertyCategoryController(IPropertyCategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All PropertyCategory")]
        public IActionResult GetAllPropertyCategory()
        {
            try
            {
                if (_categoryServices.GetAllPropertyCategory() == null)
                {
                    return NotFound(new
                    {
                        message = "Loại hình không tồn tại."
                    });
                }
                var categorys = _categoryServices.GetAllPropertyCategory();
                var response = _mapper.Map<List<PropertyCategoryVM>>(categorys);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get PropertyCategory By ID")]
        public IActionResult GetPropertyCategoryByID(Guid id)
        {
            var category = _categoryServices.GetPropertyCategoryByID(id);

            if (category != null)
            {
                var responese = _mapper.Map<PropertyCategoryVM>(category);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Loại hình không tồn tại."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new PropertyCategory")]
        public IActionResult AddNewPropertyCategory(PropertyCategoryCreateDTO category)
        {
            try
            {

                var newCategory = new PropertyCategoryCreateDTO
                {
                    PropertyCategoryID = Guid.NewGuid(),
                    PropertyCategoryName = category.PropertyCategoryName,
                    Status = true
                };

                var _category = _mapper.Map<PropertyCategory>(newCategory);
                _categoryServices.AddNewPropertyCategory(_category);

                return Ok(new
                {
                    message = "Tạo loại hình thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update PropertyCategory By ID")]
        public IActionResult UpdatePropertyCategory([FromForm] PropertyCategoryUpdateDTO category, Guid id)
        {
            try
            {
                var existingCategory = _categoryServices.GetPropertyCategoryByID(id);
                if (existingCategory != null)
                {
                    if (!string.IsNullOrEmpty(category.PropertyCategoryName))
                    {
                        existingCategory.PropertyCategoryName = category.PropertyCategoryName;
                    }
                    if (category.Status.HasValue)
                    {
                        existingCategory.Status = category.Status.Value;
                    }


                    _categoryServices.UpdatePropertyCategory(existingCategory);

                    return Ok(new
                    {
                        message = "Cập nhật loại hình thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Loại hình không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete PropertyCategory By ID")]
        public IActionResult DeletePropertyCategory(Guid id)
        {

            var category = _categoryServices.GetPropertyCategoryByID(id);
            if (category == null)
            {
                return NotFound(new
                {
                    message = "Loại hình không tồn tại."
                });
            }

            _categoryServices.ChangeStatusPropertyCategory(category);

            return Ok(new
            {
                message = "Xóa loại hình thành công."
            });
        }

    }
}
