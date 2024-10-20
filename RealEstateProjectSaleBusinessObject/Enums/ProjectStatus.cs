using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Enums
{
    public enum ProjectStatus
    {
        [EnumDescription("Sắp mở bán")]
        SapMoBan = 1,

        [EnumDescription("Đang mở bán")]
        DangMoBan = 2,

        [EnumDescription("Đã bàn giao")]
        DaBanGiao = 3
    }




}
