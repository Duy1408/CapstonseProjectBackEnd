using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Enums
{
    public enum BookingStatus
    {
        [EnumDescription("Chưa thanh toán tiền giữ chỗ")]
        ChuaThanhToanTienGiuCho = 1,

        [EnumDescription("Đã đặt chỗ")]
        DaDatCho = 2,

        [EnumDescription("Đã check in")]
        DaCheckIn = 3,

        [EnumDescription("Đã chọn sản phẩm")]
        DaChonSanPham = 4,

        [EnumDescription("Đã ký thỏa thận đặt cọc")]
        DaKyTTDC = 5,

        [EnumDescription("Đã hủy")]
        DaHuy = 6

    }

}
