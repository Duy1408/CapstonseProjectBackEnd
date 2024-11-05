using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Enums
{
    public enum ContractType
    {
        [EnumDescription("Đặt cọc")]
        DatCoc = 1,

        [EnumDescription("Mua bán")]
        MuaBan = 2,

        [EnumDescription("Chuyển nhượng")]
        ChuyenNhuong = 3
    }
}
