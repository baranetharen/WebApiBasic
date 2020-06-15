using AutoMapper;


namespace WebApi.Models
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            CreateMap<Camp, CampModel>().
                ForMember(x=>x.Country , opt => opt.MapFrom(x=>x.Location.Country)).ReverseMap();
        }
    }

    public class TalkProfile : Profile
    {
        public TalkProfile()
        {
            CreateMap<Talk, TalkModel>().ReverseMap()
                .ForMember(x => x.Camp, opt => opt.Ignore()).
                ForMember(x => x.Speaker, opt => opt.Ignore());
        }
    }

    public class SpeakerProfile : Profile
    {
        public SpeakerProfile()
        {
            CreateMap<Speaker, SpeakerModel>().ReverseMap();
        }
    }
}