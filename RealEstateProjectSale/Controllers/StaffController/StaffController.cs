using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.Contracts;

namespace RealEstateProjectSale.Controllers.StaffController
{
    [Route("api/staffs")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffServices _staffServices;
        private readonly IAccountServices _accountServices;
        private readonly IRoleServices _roleServices;
        private readonly IMapper _mapper;
        private readonly IFileUploadToBlobService _fileService;

        public StaffController(IStaffServices staffServices, IAccountServices accountServices,
                               IRoleServices roleServices, IMapper mapper, IFileUploadToBlobService fileService)
        {
            _staffServices = staffServices;
            _accountServices = accountServices;
            _roleServices = roleServices;
            _mapper = mapper;
            _fileService = fileService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Staff")]
        public IActionResult GetAllStaff()
        {
            try
            {
                if (_staffServices.GetAllStaff() == null)
                {
                    return NotFound(new
                    {
                        message = "Nhân viên không tồn tại."
                    });
                }
                var staffs = _staffServices.GetAllStaff();
                var response = _mapper.Map<List<StaffVM>>(staffs);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Staff by ID")]
        public IActionResult GetStaffByID(Guid id)
        {
            var staff = _staffServices.GetStaffByID(id);

            if (staff != null)
            {
                var responese = _mapper.Map<StaffVM>(staff);

                return Ok(responese);
            }

            return NotFound(new
            {
                message = "Nhân viên không tồn tại."
            });

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Register Account Staff")]
        public async Task<IActionResult> AddNewStaff([FromForm] RegisterStaffVM accountStaff)
        {
            try
            {

                var checkEmail = _accountServices.GetAllAccount().Where(u =>
                u.Email.Equals(accountStaff.Email)).FirstOrDefault();

                if (checkEmail != null)
                {
                    return BadRequest(new
                    {
                        message = "Email đã tồn tại."
                    });
                }

                var roleStaff = _roleServices.GetRoleByRoleName("Assistant Staff");

                var account = new AccountCreateDTO
                {
                    AccountID = Guid.NewGuid(),
                    Email = accountStaff.Email,
                    Password = accountStaff.Password,
                    Status = true,
                    RoleID = roleStaff.RoleID
                };

                var _account = _mapper.Map<Account>(account);
                _accountServices.AddNewAccount(_account);

                string? blobUrl = null;
                if (accountStaff.Image != null)
                {
                    blobUrl = _fileService.UploadSingleImage(accountStaff.Image, "staffimage");
                }

                var staff = new StaffCreateDTO
                {
                    StaffID = Guid.NewGuid(),
                    Name = accountStaff.Name,
                    PersonalEmail = accountStaff.PersonalEmail,
                    DateOfBirth = accountStaff.DateOfBirth,
                    Image = accountStaff.Image,
                    IdentityCardNumber = accountStaff.IdentityCardNumber,
                    Nationality = accountStaff.Nationality,
                    Placeoforigin = accountStaff.Placeoforigin,
                    PlaceOfresidence = accountStaff.PlaceOfresidence,
                    Status = true,
                    AccountID = account.AccountID
                };

                var _staff = _mapper.Map<Staff>(staff);
                _staff.Image = blobUrl;
                _staffServices.AddNewStaff(_staff);

                return Ok(new
                {
                    message = "Tạo tài khoản nhân viên thành công."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update Staff by ID")]
        public IActionResult UpdateStaff([FromForm] StaffUpdateDTO staff, Guid id)
        {
            try
            {
                string? blobUrl = null;
                if (staff.Image != null)
                {
                    blobUrl = _fileService.UploadSingleImage(staff.Image, "staffimage");
                }

                var _staff = _staffServices.GetStaffByID(id);
                if (_staff != null)
                {
                    if (!string.IsNullOrEmpty(staff.Name))
                    {
                        _staff.Name = staff.Name;
                    }
                    if (!string.IsNullOrEmpty(staff.PersonalEmail))
                    {
                        _staff.PersonalEmail = staff.PersonalEmail;
                    }
                    if (staff.DateOfBirth.HasValue)
                    {
                        _staff.DateOfBirth = staff.DateOfBirth.Value;
                    }
                    if (blobUrl != null)
                    {
                        _staff.Image = blobUrl;
                    }
                    if (!string.IsNullOrEmpty(staff.IdentityCardNumber))
                    {
                        _staff.IdentityCardNumber = staff.IdentityCardNumber;
                    }
                    if (!string.IsNullOrEmpty(staff.Nationality))
                    {
                        _staff.Nationality = staff.Nationality;
                    }
                    if (!string.IsNullOrEmpty(staff.Placeoforigin))
                    {
                        _staff.Placeoforigin = staff.Placeoforigin;
                    }
                    if (!string.IsNullOrEmpty(staff.PlaceOfresidence))
                    {
                        _staff.PlaceOfresidence = staff.PlaceOfresidence;
                    }
                    if (staff.Status.HasValue)
                    {
                        _staff.Status = staff.Status.Value;
                    }
                    if (staff.AccountID.HasValue)
                    {
                        _staff.AccountID = staff.AccountID.Value;
                    }

                    _staffServices.UpdateStaff(_staff);

                    return Ok(new
                    {
                        message = "Cập nhật thông tin nhân viên thành công."
                    });

                }

                return NotFound(new
                {
                    message = "Nhân viên không tồn tại."
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Staff by ID")]
        public IActionResult DeleteStaff(Guid id)
        {
            if (_staffServices.GetStaffByID(id) == null)
            {
                return NotFound(new
                {
                    message = "Nhân viên không tồn tại."
                });
            }
            var staff = _staffServices.GetStaffByID(id);
            if (staff == null)
            {
                return NotFound(new
                {
                    message = "Nhân viên không tồn tại."
                });
            }

            _staffServices.ChangeStatusStaff(staff);


            return Ok(new
            {
                message = "Xóa nhân viên thành công."
            });
        }

    }
}
