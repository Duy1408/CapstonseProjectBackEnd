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
        [EnumDescription("Chờ xác nhận TTDG")]
        ChoXacNhanTTGD = 1,

        [EnumDescription("Chờ xác nhận TTĐC")]
        ChoXacNhanTTDC = 2,

        [EnumDescription("Đã xác nhận TTĐC")]
        DaXacNhanTTDC = 3,

        [EnumDescription("Đã xác nhận chính sách bán hàng")]
        DaXacNhanCSBH = 4,

        [EnumDescription("Đã xác nhận phiếu tính giá")]
        DaXacNhanPhieuTinhGia = 5,

        [EnumDescription("Đã thanh toán đợt 1 hợp đồng mua bán")]
        DaThanhToanDotMotHDMB = 6,

        [EnumDescription("Đã xác nhận hợp đồng mua bán")]
        DaXacNhanHDMB = 7,

        [EnumDescription("Đã bàn giao quyền sở hữu đất")]
        DaBanGiaoQSHD = 8,

        [EnumDescription("Chờ xác nhận TTCN")]
        ChoXacNhanChuyenNhuong = 9,

        [EnumDescription("Đã xác nhận chuyển nhượng")]
        DaXacNhanChuyenNhuong = 10,

        [EnumDescription("Chờ xác nhận TTCNTTDC")]
        ChoXacNhanTTCNTTDC = 11,

        [EnumDescription("Đã hủy")]
        DaHuy = 12,

        [EnumDescription("Đã hết hạn")]
        DaHetHan = 13
    }
}
