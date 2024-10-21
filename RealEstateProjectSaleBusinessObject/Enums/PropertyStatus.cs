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

        [EnumDescription("Giữ chỗ")]
        DangMoBan = 2,

        [EnumDescription("Đặt cọc")]
        DatCoc = 3,

        [EnumDescription("Đã bán")]
        DaBan = 4,

        [EnumDescription("Bàn giao")]
        BanGiao = 5
    }
}
