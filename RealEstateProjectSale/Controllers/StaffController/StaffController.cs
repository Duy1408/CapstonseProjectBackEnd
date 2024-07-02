using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;
using RealEstateProjectSaleServices.IServices;

namespace RealEstateProjectSale.Controllers.StaffController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffServices _staffServices;
        private readonly IAccountServices _accountServices;
        private readonly IMapper _mapper;

        public StaffController(IStaffServices staffServices, IAccountServices accountServices, IMapper mapper)
        {
            _staffServices = staffServices;
            _accountServices = accountServices;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllStaff()
        {
            try
            {
                if (_staffServices.GetAllStaff() == null)
                {
                    return NotFound();
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
        public IActionResult GetStaffByID(Guid id)
        {
            var staff = _staffServices.GetStaffByID(id);

            if (staff != null)
            {
                var responese = _mapper.Map<StaffVM>(staff);

                return Ok(responese);
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddNewStaff(RegisterStaffVM accountStaff)
        {
            try
            {

                var checkEmail = _accountServices.GetAllAccount().Where(u =>
                u.Email.Equals(accountStaff.Email)).FirstOrDefault();

                if (checkEmail != null)
                {
                    return BadRequest("Email Existed");
                }

                var account = new AccountCreateDTO
                {
                    AccountID = Guid.NewGuid(),
                    Email = accountStaff.Email,
                    Password = accountStaff.Password,
                    Status = true,
                    RoleID = new Guid("59f5cb3c-efd7-45b9-aeec-4223aee3d253")
                };

                var _account = _mapper.Map<Account>(account);
                _accountServices.AddNewAccount(_account);

                var staff = new StaffCreateDTO
                {
                    StaffID = Guid.NewGuid(),
                    Name = accountStaff.Name,
                    PersonalEmail = accountStaff.PersonalEmail,
                    DateOfBirth = accountStaff.DateOfBirth,
                    Image = accountStaff.Image,
                    Imagesignature = accountStaff.Imagesignature,
                    IdentityCardNumber = accountStaff.IdentityCardNumber,
                    Sex = accountStaff.Sex,
                    Nationality = accountStaff.Nationality,
                    Placeoforigin = accountStaff.Placeoforigin,
                    PlaceOfresidence = accountStaff.PlaceOfresidence,
                    DateRange = accountStaff.DateRange,
                    Taxcode = accountStaff.Taxcode,
                    BankName = accountStaff.BankName,
                    BankNumber = accountStaff.BankNumber,
                    Status = true,
                    AccountID = account.AccountID
                };

                var _staff = _mapper.Map<Staff>(staff);
                _staffServices.AddNewStaff(_staff);

                return Ok("Create Staff Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStaff(StaffUpdateDTO staff, Guid id)
        {
            try
            {
                var existingStaff = _staffServices.GetStaffByID(id);
                if (existingStaff != null)
                {
                    staff.StaffID = existingStaff.StaffID;
                    staff.AccountID = existingStaff.AccountID;

                    var _staff = _mapper.Map<Staff>(staff);
                    _staffServices.UpdateStaff(_staff);

                    return Ok("Update Successfully");

                }

                return NotFound("Staff not found.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStaff(Guid id)
        {
            if (_staffServices.GetStaffByID(id) == null)
            {
                return NotFound();
            }
            var staff = _staffServices.GetStaffByID(id);
            if (staff == null)
            {
                return NotFound();
            }

            _staffServices.ChangeStatusStaff(staff);


            return Ok("Delete Successfully");
        }

    }
}
