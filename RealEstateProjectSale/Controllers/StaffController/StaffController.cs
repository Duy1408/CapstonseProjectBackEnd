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

namespace RealEstateProjectSale.Controllers.StaffController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffServices _staffServices;
        private readonly IAccountServices _accountServices;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;

        public StaffController(IStaffServices staffServices, IAccountServices accountServices, IMapper mapper, BlobServiceClient blobServiceClient)
        {
            _staffServices = staffServices;
            _accountServices = accountServices;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;
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
        public async Task<IActionResult> AddNewStaff([FromForm] RegisterStaffVM accountStaff)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("realestateprojectpictures");

                // Upload image and image signature to Azure Blob Storage if they are provided
                string? blobUrl1 = null, blobUrl2 = null;
                if (accountStaff.Image != null)
                {
                    var blobName1 = $"{Guid.NewGuid()}_{accountStaff.Image.FileName}";
                    var blobInstance1 = containerInstance.GetBlobClient(blobName1);
                    blobInstance1.Upload(accountStaff.Image.OpenReadStream());
                    var storageAccountUrl = "https://realestateprojectimage.blob.core.windows.net/realestateprojectpictures";
                    blobUrl1 = $"{storageAccountUrl}/{blobName1}";
                }

                if (accountStaff.Imagesignature != null)
                {
                    var blobName2 = $"{Guid.NewGuid()}_{accountStaff.Imagesignature.FileName}";
                    var blobInstance2 = containerInstance.GetBlobClient(blobName2);
                    blobInstance2.Upload(accountStaff.Imagesignature.OpenReadStream());
                    var storageAccountUrl = "https://realestateprojectimage.blob.core.windows.net/realestateprojectpictures";
                    blobUrl2 = $"{storageAccountUrl}/{blobName2}";
                }

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
                    RoleID = new Guid("f4402e46-a36e-4404-9602-9ef0ae4d5636")
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
                    Taxcode = accountStaff.Taxcode,
                    BankName = accountStaff.BankName,
                    BankNumber = accountStaff.BankNumber,
                    Status = true,
                    AccountID = account.AccountID
                };

                var _staff = _mapper.Map<Staff>(staff);
                _staff.Image = blobUrl1;
                _staff.Imagesignature = blobUrl2;
                _staffServices.AddNewStaff(_staff);

                return Ok("Create Staff Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //public ActionResult<Staff> AddNewStaff([FromForm] StaffCreateDTO staff)
        //{
        //    try
        //    {
        //        var containerInstance = _blobServiceClient.GetBlobContainerClient("realestateprojectpictures");
        //        //get file name from request and upload to azure blod storage
        //        var blobName = $"{Guid.NewGuid()} {staff.Image?.FileName}";
        //        //local file path
        //        var blobInstance = containerInstance.GetBlobClient(blobName);
        //        blobInstance.Upload(staff.Image?.OpenReadStream());

        //        //storageAccountUrl
        //        var storageAccountUrl = "https://realestateprojectimage.blob.core.windows.net/realestateprojectpictures";
        //        //get blod url
        //        var blobUrl = $"{storageAccountUrl}/{blobName}";

        //        var _staff = _mapper.Map<Staff>(staff);
        //        _staff.Image = blobUrl;
        //        _staffServices.AddNewStaff(_staff);

        //        return Ok("Create Staff Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        [HttpPut("{id}")]
        public IActionResult UpdateStaff([FromForm] StaffUpdateDTO staff, Guid id)
        {
            try
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("realestateprojectpictures");

                // Upload image and image signature to Azure Blob Storage if they are provided
                string? blobUrl1 = null, blobUrl2 = null;
                if (staff.Image != null)
                {
                    var blobName1 = $"{Guid.NewGuid()}_{staff.Image.FileName}";
                    var blobInstance1 = containerInstance.GetBlobClient(blobName1);
                    blobInstance1.Upload(staff.Image.OpenReadStream());
                    var storageAccountUrl = "https://realestateprojectimage.blob.core.windows.net/realestateprojectpictures";
                    blobUrl1 = $"{storageAccountUrl}/{blobName1}";
                }

                if (staff.Imagesignature != null)
                {
                    var blobName2 = $"{Guid.NewGuid()}_{staff.Imagesignature.FileName}";
                    var blobInstance2 = containerInstance.GetBlobClient(blobName2);
                    blobInstance2.Upload(staff.Imagesignature.OpenReadStream());
                    var storageAccountUrl = "https://realestateprojectimage.blob.core.windows.net/realestateprojectpictures";
                    blobUrl2 = $"{storageAccountUrl}/{blobName2}";
                }



                var _staff = _staffServices.GetStaffByID(id);
                if (_staff != null)
                {
                    staff.StaffID = _staff.StaffID;
                    staff.AccountID = _staff.AccountID;

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
                    if (blobUrl1 != null)
                    {
                        _staff.Image = blobUrl1;
                    }
                    if (blobUrl2 != null)
                    {
                        _staff.Imagesignature = blobUrl2;
                    }
                    if (!string.IsNullOrEmpty(staff.IdentityCardNumber))
                    {
                        _staff.IdentityCardNumber = staff.IdentityCardNumber;
                    }
                    if (!string.IsNullOrEmpty(staff.Sex))
                    {
                        _staff.Sex = staff.Sex;
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
                    if (!string.IsNullOrEmpty(staff.Taxcode))
                    {
                        _staff.Taxcode = staff.Taxcode;
                    }
                    if (!string.IsNullOrEmpty(staff.BankName))
                    {
                        _staff.BankName = staff.BankName;
                    }
                    if (staff.BankNumber.HasValue)
                    {
                        _staff.BankNumber = staff.BankNumber.Value;
                    }
                    if (staff.Status.HasValue)
                    {
                        _staff.Status = staff.Status.Value;
                    }


                    //var staffUpdate = _mapper.Map<Staff>(staff);
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
