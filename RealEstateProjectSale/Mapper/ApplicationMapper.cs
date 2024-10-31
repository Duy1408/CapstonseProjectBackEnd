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
            CreateMap<StaffVM, Staff>().ReverseMap().ForMember(dest => dest.Email,
                                       opt => opt.MapFrom(src => src.Account!.Email));
            CreateMap<StaffCreateDTO, Staff>().ReverseMap();
            CreateMap<StaffUpdateDTO, Staff>().ReverseMap();

            CreateMap<AccountVM, Account>().ReverseMap().ForMember(dest => dest.RoleName,
                                       opt => opt.MapFrom(src => src.Role!.RoleName));
            CreateMap<AccountCreateDTO, Account>().ReverseMap();
            CreateMap<AccountUpdateDTO, Account>().ReverseMap();

            CreateMap<RoleVM, Role>().ReverseMap();

            CreateMap<CustomerVM, Customer>().ReverseMap().ForMember(dest => dest.Email,
                                       opt => opt.MapFrom(src => src.Account!.Email));
            CreateMap<CustomerCreateDTO, Customer>().ReverseMap();
            CreateMap<CustomerUpdateDTO, Customer>().ReverseMap();

            CreateMap<ContractVM, Contract>().ReverseMap().ForMember(dest => dest.PaymentProcessName,
                                       opt => opt.MapFrom(src => src.PaymentProcess!.PaymentProcessName))
                                                          .ForMember(dest => dest.DocumentName,
                                       opt => opt.MapFrom(src => src.DocumentTemplate!.DocumentName))
                                                          .ForMember(dest => dest.CustomerID,
                                       opt => opt.MapFrom(src => src.Booking!.CustomerID));
            CreateMap<ContractCreateDTO, Contract>().ReverseMap();
            CreateMap<ContractUpdateDTO, Contract>().ReverseMap();


            CreateMap<PromotionDetailVM, PromotionDetail>().ReverseMap().ForMember(dest => dest.PropertyTypeName,
                                      opt => opt.MapFrom(src => src.PropertyType!.PropertyTypeName))
                                                       .ForMember(dest => dest.PromotionName,
                                      opt => opt.MapFrom(src => src.Promotion!.PromotionName));
            CreateMap<PromotionDetailCreateDTO, PromotionDetail>().ReverseMap();
            CreateMap<PromotionDetailUpdateDTO, PromotionDetail>().ReverseMap();

            CreateMap<ProjectVM, Project>().ReverseMap();
            CreateMap<ProjectCreateDTO, Project>().ReverseMap();
            CreateMap<ProjectUpdateDTO, Project>().ReverseMap();

            CreateMap<CommentVM, Comment>().ReverseMap().ForMember(dest => dest.PropertyName,
                                       opt => opt.MapFrom(src => src.Property!.PropertyCode))
                                                        .ForMember(dest => dest.PersonalEmail,
                                       opt => opt.MapFrom(src => src.Customer!.FullName));
            CreateMap<CommentCreateDTO, Comment>().ReverseMap();

            CreateMap<SalepolicyVM, Salespolicy>().ReverseMap().ForMember(dest => dest.ProjectName,
                                     opt => opt.MapFrom(src => src.Project!.ProjectName));
            CreateMap<SalepolicyCreateDTO, Salespolicy>().ReverseMap();
            CreateMap<SalePolicyUpdateDTO, Salespolicy>().ReverseMap();

            //CreateMap<BookingVM, Booking>().ReverseMap().ForMember(dest => dest.DocumentName,
            //                           opt => opt.MapFrom(src => src.DocumentTemplate!.DocumentName))
            //                                            .ForMember(dest => dest.DecisionName,
            //                           opt => opt.MapFrom(src => src.OpeningForSale!.DecisionName))
            //                                            .ForMember(dest => dest.PropertyCategoryName,
            //                           opt => opt.MapFrom(src => src.PropertyCategory!.PropertyCategoryName))
            //                                            .ForMember(dest => dest.ProjectName,
            //                           opt => opt.MapFrom(src => src.Project!.ProjectName))
            //                                            .ForMember(dest => dest.CustomerName,
            //                           opt => opt.MapFrom(src => src.Customer!.FullName))
            //                                            .ForMember(dest => dest.StaffName,
            //                           opt => opt.MapFrom(src => src.Staff!.Name));
            CreateMap<BookingCreateDTO, Booking>().ReverseMap();
            CreateMap<BookingUpdateDTO, Booking>().ReverseMap();

            CreateMap<PropertyVM, Property>().ReverseMap().ForMember(dest => dest.BathRoom,
                                       opt => opt.MapFrom(src => src.UnitType!.BathRoom))
                                                          .ForMember(dest => dest.BedRoom,
                                       opt => opt.MapFrom(src => src.UnitType!.BedRoom))
                                                          .ForMember(dest => dest.KitchenRoom,
                                       opt => opt.MapFrom(src => src.UnitType!.KitchenRoom))
                                                          .ForMember(dest => dest.LivingRoom,
                                       opt => opt.MapFrom(src => src.UnitType!.LivingRoom))
                                                          .ForMember(dest => dest.NumberFloor,
                                       opt => opt.MapFrom(src => src.UnitType!.NumberFloor))
                                                          .ForMember(dest => dest.Basement,
                                       opt => opt.MapFrom(src => src.UnitType!.Basement))
                                                          .ForMember(dest => dest.NetFloorArea,
                                       opt => opt.MapFrom(src => src.UnitType!.NetFloorArea))
                                                          .ForMember(dest => dest.GrossFloorArea,
                                       opt => opt.MapFrom(src => src.UnitType!.GrossFloorArea))
                                                          .ForMember(dest => dest.ImageUnitType,
                                       opt => opt.MapFrom(src => src.UnitType!.Image))
                                                          .ForMember(dest => dest.NumFloor,
                                       opt => opt.MapFrom(src => src.Floor!.NumFloor))
                                                          .ForMember(dest => dest.BlockName,
                                       opt => opt.MapFrom(src => src.Block!.BlockName))
                                                          .ForMember(dest => dest.ZoneName,
                                       opt => opt.MapFrom(src => src.Zone!.ZoneName));
            CreateMap<PropertyCreateDTO, Property>().ReverseMap();
            CreateMap<PropertyUpdateDTO, Property>().ReverseMap();

            CreateMap<PropertyTypeVM, PropertyType>().ReverseMap().ForMember(dest => dest.PropertyCategoryName,
                                  opt => opt.MapFrom(src => src.PropertyCategory!.PropertyCategoryName));
            CreateMap<PropertyTypeCreateDTO, PropertyType>().ReverseMap();
            CreateMap<PropertyTypeUpdateDTO, PropertyType>().ReverseMap();


            CreateMap<PromotionVM, Promotion>().ReverseMap().ForMember(dest => dest.SalesPolicyType,
                                  opt => opt.MapFrom(src => src.Salespolicy!.SalesPolicyType));
            CreateMap<ProjectCreateDTO, Promotion>().ReverseMap();
            CreateMap<PromotionUpdateDTO, Promotion>().ReverseMap();

            //CreateMap<OpeningForSaleVM, OpeningForSale>().ReverseMap().ForMember(dest => dest.ProjectName,
            //                      opt => opt.MapFrom(src => src.Project!.ProjectName));
            CreateMap<OpeningForSaleCreateDTO, OpeningForSale>().ReverseMap();
            CreateMap<OpeningForSaleUpdateDTO, OpeningForSale>().ReverseMap();

            CreateMap<OpenForSaleDetailVM, OpenForSaleDetail>().ReverseMap().ForMember(dest => dest.OpeningForSaleName,
                                 opt => opt.MapFrom(src => src.OpeningForSale!.DecisionName))
                                                                            .ForMember(dest => dest.PropertyName,
                                 opt => opt.MapFrom(src => src.Property!.PropertyCode));
            CreateMap<OpenForSaleDetailCreateDTO, OpenForSaleDetail>().ReverseMap();

            CreateMap<PaymentProcessVM, PaymentProcess>().ReverseMap().ForMember(dest => dest.SalesPolicyType,
                                 opt => opt.MapFrom(src => src.Salespolicy!.SalesPolicyType));
            CreateMap<PaymentProcessCreateDTO, PaymentProcess>().ReverseMap();
            CreateMap<PaymentProcessUpdateDTO, PaymentProcess>().ReverseMap();

            CreateMap<PaymentProcessDetailVM, PaymentProcessDetail>().ReverseMap().ForMember(dest => dest.PaymentProcessName,
                                 opt => opt.MapFrom(src => src.PaymentProcess!.PaymentProcessName));
            CreateMap<PaymentProcessDetailCreateDTO, PaymentProcessDetail>().ReverseMap();
            CreateMap<PaymentProcessDetailUpdateDTO, PaymentProcessDetail>().ReverseMap();

            CreateMap<PaymentProcessVM, PaymentProcess>().ReverseMap().ForMember(dest => dest.SalesPolicyType,
                                 opt => opt.MapFrom(src => src.Salespolicy!.SalesPolicyType));
            CreateMap<PaymentProcessCreateDTO, PaymentProcess>().ReverseMap();
            CreateMap<PaymentProcessUpdateDTO, PaymentProcess>().ReverseMap();

            CreateMap<ContractPaymentDetailVM, ContractPaymentDetail>().ReverseMap().ForMember(dest => dest.ContractName,
                                 opt => opt.MapFrom(src => src.Contract!.ContractName));
            CreateMap<ContractPaymentDetailCreateDTO, ContractPaymentDetail>().ReverseMap();
            CreateMap<ContractPaymentDetailUpdateDTO, ContractPaymentDetail>().ReverseMap();

            CreateMap<ZoneCreateDTO, Zone>().ReverseMap();
            CreateMap<ZoneUpdateDTO, Zone>().ReverseMap();
            CreateMap<ZoneVM, Zone>().ReverseMap();

            CreateMap<BlockCreateDTO, Block>().ReverseMap();
            CreateMap<BlockUpdateDTO, Block>().ReverseMap();
            CreateMap<BlockVM, Block>().ReverseMap();

            CreateMap<FloorCreateDTO, Floor>().ReverseMap();
            CreateMap<FloorUpdateDTO, Floor>().ReverseMap();
            CreateMap<FloorVM, Floor>().ReverseMap();

            CreateMap<DocumentTemplateCreateDTO, DocumentTemplate>().ReverseMap();
            CreateMap<DocumentTemplateCreateDTO, DocumentTemplate>().ReverseMap();
            CreateMap<DocumentTemplateVM, DocumentTemplate>().ReverseMap();

            CreateMap<PropertyCategoryVM, PropertyCategory>().ReverseMap();
            CreateMap<PropertyCategoryCreateDTO, PropertyCategory>().ReverseMap();
            CreateMap<PropertyCategoryUpdateDTO, PropertyCategory>().ReverseMap();

            CreateMap<ProjectCategoryDetailVM, ProjectCategoryDetail>().ReverseMap().ForMember(dest => dest.ProjectName,
                                 opt => opt.MapFrom(src => src.Project!.ProjectName))
                                                                                    .ForMember(dest => dest.PropertyCategoryName,
                                 opt => opt.MapFrom(src => src.PropertyCategory!.PropertyCategoryName));
            CreateMap<ProjectCategoryDetailCreateDTO, ProjectCategoryDetail>().ReverseMap();
            CreateMap<ProjectCategoryDetailUpdateDTO, ProjectCategoryDetail>().ReverseMap();

            CreateMap<UnitTypeVM, UnitType>().ReverseMap().ForMember(dest => dest.PropertyTypeName,
                                 opt => opt.MapFrom(src => src.PropertyType!.PropertyTypeName))
                                                           .ForMember(dest => dest.ProjectName,
                                 opt => opt.MapFrom(src => src.Project!.ProjectName));
            CreateMap<UnitTypeCreateDTO, UnitType>().ReverseMap();
            CreateMap<UnitTypeUpdateDTO, UnitType>().ReverseMap();

            CreateMap<PaymentVM, Payment>().ReverseMap().ForMember(dest => dest.BookingStatus,
                                 opt => opt.MapFrom(src => src.Booking!.Status))
                                                        .ForMember(dest => dest.FullName,
                                 opt => opt.MapFrom(src => src.Customer!.FullName));
            CreateMap<PaymentCreateDTO, Payment>().ReverseMap();
            CreateMap<PaymentUpdateDTO, Payment>().ReverseMap();


            CreateMap<NotificationVM, Notification>().ReverseMap().ForMember(dest => dest.FullName,
                                 opt => opt.MapFrom(src => src.Customer!.FullName))
                                                        .ForMember(dest => dest.DecisionName,
                                 opt => opt.MapFrom(src => src.OpeningForSale!.DecisionName));
            CreateMap<NotificationCreateDTO, Notification>().ReverseMap();


        }
    }
}
