using AutoMapper;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.DTO.Create;
using RealEstateProjectSaleBusinessObject.DTO.Update;

namespace RealEstateProjectSale.Mapper
{
    public class ProjectProfile : Profile
    {

        public ProjectProfile()
        {
            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(
                dest => dest.ProjectID,
                opt => opt.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                   dest => dest.CommericalName,
                opt => opt.MapFrom(src => src.CommericalName)
                )
                .ForMember(
                   dest => dest.ShortName,
                opt => opt.MapFrom(src => src.ShortName)
                )
                 .ForMember(
                    dest => dest.TypeOfProject,
                 opt => opt.MapFrom(src => src.TypeOfProject)
                )
                  .ForMember(
                    dest => dest.Address,
                 opt => opt.MapFrom(src => src.Address)
                )
                   .ForMember(
                    dest => dest.Commune,
                 opt => opt.MapFrom(src => src.Commune)
                )
                   .ForMember(
                    dest => dest.District,
                 opt => opt.MapFrom(src => src.District)
                )
                    .ForMember(
                    dest => dest.DepositPrice,
                 opt => opt.MapFrom(src => src.DepositPrice)
                )
                    .ForMember(
                    dest => dest.Summary,
                 opt => opt.MapFrom(src => src.Summary)
                )
                   .ForMember(
                    dest => dest.LicenseNo,
                 opt => opt.MapFrom(src => src.LicenseNo)
                )
             .ForMember(
                    dest => dest.DateOfIssue,
                 opt => opt.MapFrom(src => src.DateOfIssue)
                )
                .ForMember(
                    dest => dest.CampusArea,
                 opt => opt.MapFrom(src => src.CampusArea)
                )
                .ForMember(
                    dest => dest.PlaceofIssue,
                 opt => opt.MapFrom(src => src.PlaceofIssue)
                )
                 .ForMember(
                    dest => dest.Code,
                 opt => opt.MapFrom(src => src.Code)
                )
                 .ForMember(
                    dest => dest.Image,
                 opt => opt.MapFrom(src => src.Image)
                )
                ;

            CreateMap<ProjectUpdateDTO, Project>()
              
                .ForMember(
                   dest => dest.CommericalName,
                opt => opt.MapFrom(src => src.CommericalName)
                )
                .ForMember(
                   dest => dest.ShortName,
                opt => opt.MapFrom(src => src.ShortName)
                )
            
                  .ForMember(
                    dest => dest.Address,
                 opt => opt.MapFrom(src => src.Address)
                )
                   .ForMember(
                    dest => dest.Commune,
                 opt => opt.MapFrom(src => src.Commune)
                )
                   .ForMember(
                    dest => dest.District,
                 opt => opt.MapFrom(src => src.District)
                )
                    .ForMember(
                    dest => dest.DepositPrice,
                 opt => opt.MapFrom(src => src.DepositPrice)
                )
                    .ForMember(
                    dest => dest.Summary,
                 opt => opt.MapFrom(src => src.Summary)
                )
                   .ForMember(
                    dest => dest.LicenseNo,
                 opt => opt.MapFrom(src => src.LicenseNo)
                )
             .ForMember(
                    dest => dest.DateOfIssue,
                 opt => opt.MapFrom(src => src.DateOfIssue)
                )
                .ForMember(
                    dest => dest.CampusArea,
                 opt => opt.MapFrom(src => src.CampusArea)
                )
                .ForMember(
                    dest => dest.PlaceofIssue,
                 opt => opt.MapFrom(src => src.PlaceofIssue)
                )
                 .ForMember(
                    dest => dest.Code,
                 opt => opt.MapFrom(src => src.Code)
                )
                 .ForMember(
                    dest => dest.Image,
                 opt => opt.MapFrom(src => src.Image)
                )
                ;
        }




    }
}
