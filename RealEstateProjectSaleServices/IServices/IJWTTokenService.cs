using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.IServices
{
    public interface IJWTTokenService
    {
        string CreateJWTToken(Account account);
        string CreateAdminJWTToken();
        AuthVM ParseJwtToken(string token);

    }
}
