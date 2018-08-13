using AutoMapper;
using JournalResearcher.DataAccess.Data.Models;
using JournalResearcher.DataAccess.ViewModel;

namespace JournalResearcher
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ModelMapper();
        }

        public void ModelMapper()
        {
            CreateMap<JournalViewModel, Journal>();
            CreateMap<Journal, JournalViewModel>();
            CreateMap<Journal, JournalItem>().ForMember(dest => dest.ThesisFile, opt => opt.Ignore())
                .ForMember(dest => dest.ThesisFileUrl, opt => opt.MapFrom(x => x.ThesisFile))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id));
            CreateMap<ApplicationUser, UserModel>();
            CreateMap<UserModel, ApplicationUser>();
        }
    }
}