using RealEstateProjectSaleBusinessObject.Enums.EnumHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Enums
{
    public enum ContractStatus
    {
        [EnumDescription("Chờ xác nhận TTĐC")]
        ChoXacNhanTTDC = 1,

        [EnumDescription("Đã xác nhận TTĐC")]
        DaXacNhanTTDC = 2,

        [EnumDescription("Đã xác nhận chính sách bán hàng")]
        DaXacNhanCSBH = 3,

        [EnumDescription("Đã xác nhận phiếu tính giá")]
        DaXacNhanPhieuTinhGia = 4,

        [EnumDescription("Đã thanh toán đợt 1 hợp đồng mua bán")]
        DaThanhToanDotMotHDMB = 5,

        [EnumDescription("Đã xác nhận hợp đồng mua bán")]
        DaXacNhanHDMB = 6,

        [EnumDescription("Đã bàn giao quyền sở hữu đất")]
        DaBanGiaoQSHD = 7,

        [EnumDescription("Đã xác nhận chuyển nhượng")]
        DaXacNhanChuyenNhuong = 8,

        [EnumDescription("Đã hủy")]
        DaHuy = 9
    }
}
