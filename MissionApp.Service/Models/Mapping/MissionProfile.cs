using AutoMapper;
using MissionApp.Domain;
using MissionApp.Models;

namespace MissionApp.Service.Models
{
    public class MissionProfile : Profile
    {
        public MissionProfile()
        {
            CreateMap<MissionRequest, Mission>()
               .ForMember(
               d => d.Country,
               o => o.MapFrom(model => model))
               .ForMember(
                d => d.Agent,
                o => o.MapFrom(model => model));
                
            CreateMap<MissionRequest, Country>()
                .ForMember(
                d => d.Name,
                o => o.MapFrom(s => s.Country));

            CreateMap<MissionRequest, Agent>()
                .ForMember(
                d => d.Name,
                o => o.MapFrom(model => model));

            CreateMap<MissionRequest, Agent>()
                .ForMember(
                d => d.Name,
                o => o.MapFrom(s => s.Agent));


            CreateMap<Mission, MissionResponse>()
                .ForMember(
                d => d.Agent,
                o => o.MapFrom(s => s.Agent.Name))
                .ForMember(
                d => d.Country,
                o => o.MapFrom(s => s.Country.Name));

        }
    }
}
