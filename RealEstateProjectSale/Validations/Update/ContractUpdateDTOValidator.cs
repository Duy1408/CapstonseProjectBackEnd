using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;

namespace RealEstateProjectSale.Validations.Update
{
    public class ContractUpdateDTOValidator : AbstractValidator<ContractUpdateDTO>
    {
        public ContractUpdateDTOValidator()
        {
            RuleFor(x => x.ContractType)
                .MaximumLength(50).WithMessage("Loại hợp đồng không được vượt quá 50 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.ContractType));

            RuleFor(x => x.ExpiredTime)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Thời hạn hợp đồng phải lớn hơn hoặc bằng ngày hiện tại.")
                .When(x => x.ExpiredTime.HasValue);

            RuleFor(x => x.TotalPrice)
                .GreaterThanOrEqualTo(1500000000).WithMessage("Tổng giá trị hợp đồng tối thiểu phải từ 1 tỷ 500 triệu đồng.")
                .When(x => x.TotalPrice.HasValue);

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Mô tả không được vượt quá 2000 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Status)
                .Must(status =>
                {
                    if (string.IsNullOrEmpty(status)) return true;
                    return int.TryParse(status, out var value) && Enum.IsDefined(typeof(ContractStatus), value);
                })
                .WithMessage("Trạng thái không hợp lệ. Vui lòng nhập số tương ứng với enum ContractStatus.");



        }
    }
}
