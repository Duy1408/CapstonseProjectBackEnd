using AutoMapper;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;
using RealEstateProjectSaleBusinessObject.ViewModels;

namespace RealEstateProjectSale.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<StaffVM, Staff>().ReverseMap();
            CreateMap<StaffCreateDTO, Staff>().ReverseMap();
            CreateMap<StaffUpdateDTO, Staff>().ReverseMap();

            CreateMap<AccountVM, Account>().ReverseMap().ForMember(dest => dest.RoleName,
                                       opt => opt.MapFrom(src => src.Role!.RoleName));
            CreateMap<AccountCreateDTO, Account>().ReverseMap();
            CreateMap<AccountUpdateDTO, Account>().ReverseMap();

            CreateMap<RoleVM, Role>().ReverseMap();

            CreateMap<CustomerVM, Customer>().ReverseMap();
            CreateMap<CustomerCreateDTO, Customer>().ReverseMap();
            CreateMap<CustomerUpdateDTO, Customer>().ReverseMap();

            CreateMap<ContractVM, Contract>().ReverseMap().ForMember(dest => dest.PaymentProcessName,
                                       opt => opt.MapFrom(src => src.PaymentProcess!.PaymentProcessName));
            CreateMap<ContractCreateDTO, Contract>().ReverseMap();
            CreateMap<ContractUpdateDTO, Contract>().ReverseMap();

            CreateMap<PaymentTypeVM, PaymentType>().ReverseMap();
            CreateMap<PaymentTypeCreateDTO, PaymentType>().ReverseMap();
            CreateMap<PaymentTypeUpdateDTO, PaymentType>().ReverseMap();

            CreateMap<PromotionDetailVM, PromotionDetail>().ReverseMap();
            CreateMap<PromotionDetailCreateDTO, PromotionDetail>().ReverseMap();
            CreateMap<PromotionDetailUpdateDTO, PromotionDetail>().ReverseMap();

            CreateMap<ProjectVM, Project>().ReverseMap();
            CreateMap<ProjectCreateDTO, Project>().ReverseMap();
            CreateMap<ProjectUpdateDTO, Project>().ReverseMap();

            CreateMap<CommentVM, Comment>().ReverseMap().ForMember(dest => dest.PropertyName,
                                       opt => opt.MapFrom(src => src.Property!.PropertyName))
                                                        .ForMember(dest => dest.PersonalEmail,
                                       opt => opt.MapFrom(src => src.Customer!.PersonalEmail));
            CreateMap<CommentCreateDTO, Comment>().ReverseMap();

            CreateMap<SalepolicyVM, Salespolicy>().ReverseMap().ForMember(dest => dest.ProjectName,
                                     opt => opt.MapFrom(src => src.Project!.ProjectName));
            CreateMap<SalepolicyCreateDTO, Salespolicy>().ReverseMap();
            CreateMap<SalePolicyUpdateDTO, Salespolicy>().ReverseMap();

            CreateMap<BookingVM, Booking>().ReverseMap().ForMember(dest => dest.PropertyName,
                                       opt => opt.MapFrom(src => src.Property!.PropertyName))
                                                        .ForMember(dest => dest.DescriptionName,
                                       opt => opt.MapFrom(src => src.OpeningForSale!.DescriptionName))
                                                        .ForMember(dest => dest.ProjectName,
                                       opt => opt.MapFrom(src => src.Project!.ProjectName))
                                                        .ForMember(dest => dest.PersonalEmailCs,
                                       opt => opt.MapFrom(src => src.Customer!.PersonalEmail))
                                                        .ForMember(dest => dest.PersonalEmailSt,
                                       opt => opt.MapFrom(src => src.Staff!.PersonalEmail));
            CreateMap<BookingCreateDTO, Booking>().ReverseMap();


        }
    }
}
