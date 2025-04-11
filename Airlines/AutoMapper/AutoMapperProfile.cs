using Airlines.ViewModels;
using AutoMapper;
using Domains.Entities;

namespace Airlines.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Plaats, PlaatsVM>();         
        }
    }
}
