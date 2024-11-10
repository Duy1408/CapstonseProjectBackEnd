using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Enums
{
    public enum OpeningForSaleStatus
    {
        [EnumDescription("Chưa mở bán")]
        ChuaMoBan = 1,

        [EnumDescription("Giữ chỗ")]
        GiuCho = 2,

        [EnumDescription("Check in")]
        CheckIn = 3,

        [EnumDescription("Mua trực tiếp")]
        MuaTrucTiep = 4
    }
}
