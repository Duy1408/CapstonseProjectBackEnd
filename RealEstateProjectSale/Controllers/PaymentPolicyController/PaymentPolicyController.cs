using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateProjectSale.Controllers.PaymentPolicyController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPolicyController : ControllerBase
    {
        private readonly IPaymentPolicyService _policyServices;
        private readonly IMapper _mapper;

        public PaymentPolicyController(IPaymentPolicyService policyServices, IMapper mapper)
        {
            _policyServices = policyServices;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get All PaymentPolicy")]
        public IActionResult GetAllPaymentPolicy()
        {
            try
            {
                if (_policyServices.GetAllPaymentPolicy() == null)
                {
                    return NotFound(new
                    {
                        message = "Chính sách thanh toán không tồn tại."
                    });
                }
                var policys = _policyServices.GetAllPaymentPolicy();
                var response = _mapper.Map<List<PaymentPolicyVM>>(policys);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get PaymentPolicy By ID")]
        public IActionResult GetPaymentPolicyByID(Guid id)
        {
            var policy = _policyServices.GetPaymentPolicyByID(id);

            if (policy != null)
            {
                var responese = _mapper.Map<PaymentPolicyVM>(policy);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Chính sách thanh toán không tồn tại."
            });

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new PaymentPolicy")]
        public IActionResult AddNewPaymentPolicy(PaymentPolicyCreateDTO policy)
        {
            try
            {
                var newPolicy = new PaymentPolicyCreateDTO
                {
                    PaymentPolicyID = Guid.NewGuid(),
                    PaymentPolicyName = policy.PaymentPolicyName,
                    LateDate = policy.LateDate,
                    Status = true,
                    PercentLate = policy.PercentLate
                };

                var _policy = _mapper.Map<PaymentPolicy>(newPolicy);
                _policyServices.AddNewPaymentPolicy(_policy);

                return Ok(new
                {
                    message = "Tạo Chính sách thanh toán thành công"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update PaymentPolicy")]
        public IActionResult UpdatePaymentPolicy([FromForm] PaymentPolicyUpdateDTO policy, Guid id)
        {
            try
            {
                var existingPolicy = _policyServices.GetPaymentPolicyByID(id);
                if (existingPolicy != null)
                {
                    if (!string.IsNullOrEmpty(policy.PaymentPolicyName))
                    {
                        existingPolicy.PaymentPolicyName = policy.PaymentPolicyName;
                    }
                    if (policy.LateDate.HasValue)
                    {
                        existingPolicy.LateDate = policy.LateDate.Value;
                    }
                    if (policy.PercentLate.HasValue)
                    {
                        existingPolicy.PercentLate = policy.PercentLate.Value;
                    }
                    if (policy.Status.HasValue)
                    {
                        existingPolicy.Status = policy.Status.Value;
                    }


                    _policyServices.UpdatePaymentPolicy(existingPolicy);

                    return Ok(new
                    {
                        message = "Cập nhật Chính sách thanh toán thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Chính sách thanh toán không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete PaymentPolicy")]
        public IActionResult DeletePaymentPolicy(Guid id)
        {

            var policy = _policyServices.GetPaymentPolicyByID(id);
            if (policy == null)
            {
                return NotFound(new
                {
                    message = "Chính sách thanh toán không tồn tại."
                });
            }

            _policyServices.ChangeStatusPaymentPolicy(policy);

            return Ok(new
            {
                message = "Xóa Chính sách thanh toán thành công"
            });
        }

    }
}
