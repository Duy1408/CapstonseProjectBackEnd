using FluentValidation;
using RealEstateProjectSaleBusinessObject.DTO.Request;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.Enums;

namespace RealEstateProjectSale.Validations.Update
{
    public class ProjectUpdateDTOValidator : AbstractValidator<ProjectUpdateDTO>
    {
        public ProjectUpdateDTOValidator()
        {
            RuleFor(x => x.ProjectName)
                .MaximumLength(200).WithMessage("Tên dự án không được vượt quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.ProjectName));

            RuleFor(x => x.Location)
                .MaximumLength(300).WithMessage("Địa điểm không được vượt quá 300 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Location));

            RuleFor(x => x.Investor)
                .MaximumLength(200).WithMessage("Nhà đầu tư không được vượt quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Investor));

            RuleFor(x => x.GeneralContractor)
                .MaximumLength(200).WithMessage("Nhà thầu chính không được vượt quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.GeneralContractor));

            RuleFor(x => x.DesignUnit)
                .MaximumLength(200).WithMessage("Đơn vị thiết kế không được vượt quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.DesignUnit));

            RuleFor(x => x.TotalArea)
                .MaximumLength(200).WithMessage("Tổng diện tích không được vượt quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.TotalArea));

            RuleFor(x => x.Scale)
                .MaximumLength(200).WithMessage("Quy mô không được vượt quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Scale));

            RuleFor(x => x.BuildingDensity)
                .MaximumLength(200).WithMessage("Mật độ xây dựng không được vượt quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.BuildingDensity));

            RuleFor(x => x.TotalNumberOfApartment)
                .Matches(@"^\d+$").WithMessage("Số lượng căn hộ phải là một số nguyên dương.")
                .Must(BeValidApartmentCount).WithMessage("Số lượng căn hộ phải lớn hơn 0.")
                .When(x => !string.IsNullOrEmpty(x.TotalNumberOfApartment));

            RuleFor(x => x.LegalStatus)
                .MaximumLength(200).WithMessage("Tình trạng pháp lý không được vượt quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.LegalStatus));

            RuleFor(x => x.HandOver)
                .MaximumLength(200).WithMessage("Ngày bàn giao không được vượt quá 200 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.HandOver));

            RuleFor(x => x.Convenience)
                .MaximumLength(500).WithMessage("Mô tả tiện ích không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Convenience));

            RuleFor(x => x.Status)
                .Must(status =>
                {
                    if (string.IsNullOrEmpty(status)) return true;
                    return int.TryParse(status, out var value) && Enum.IsDefined(typeof(ProjectStatus), value);
                })
                .WithMessage("Trạng thái không hợp lệ. Vui lòng nhập số tương ứng với enum ProjectStatus.");



        }

        private bool BeValidApartmentCount(string totalNumberOfApartment)
        {
            if (int.TryParse(totalNumberOfApartment, out var apartmentCount))
            {
                return apartmentCount > 0;
            }
            return false;
        }

    }
}
