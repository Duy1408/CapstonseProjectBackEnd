using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Enums
{
    public enum PropertyStatus
    {
        [EnumDescription("Chưa bán")]
        ChuaBan = 1,

        [EnumDescription("Mở bán")]
        MoBan = 2,

        [EnumDescription("Giữ chỗ")]
        GiuCho = 3,

        [EnumDescription("Đặt cọc")]
        DatCoc = 4,

        [EnumDescription("Đã bán")]
        DaBan = 5,

        [EnumDescription("Bàn giao")]
        BanGiao = 6
    }
}
