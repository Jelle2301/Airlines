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
           
            CreateMap<Vlucht, VluchtVM>()
                .ForMember(dest=> dest.BeginPrijs,
                    opts=> opts.MapFrom(
                        src=> src.Prijs))
                .ForMember(dest=> dest.VliegtuigNaam,
                    opts=>opts.MapFrom(
                        src=>src.Vliegtuig.VliegtuigNaam))
                .ForMember(dest => dest.AantalTotalePlaatsen,
                    opts => opts.MapFrom(
                        src => src.Vliegtuig.AantalTotalePlaatsen))
                .ForMember(dest => dest.AantalGewonePlaatsen,
                    opts => opts.MapFrom(
                        src => src.Vliegtuig.AantalGewonePlaatsen))
                .ForMember(dest => dest.AantalBusinessPlaatsen,
                    opts => opts.MapFrom(
                        src => src.Vliegtuig.AantalBusinessPlaatsen))
                .ForMember(dest => dest.AantalEconomyPlaatsen,
                    opts => opts.MapFrom(
                        src => src.Vliegtuig.AantalEconomyPlaatsen))
                .ForMember(dest => dest.VertrekNaam,
                    opts => opts.MapFrom(
                        src => src.Vertrekplaats.Plaats.Naam))
                .ForMember(dest => dest.OverstapNaam,
                    opts => opts.MapFrom(
                        src => src.Overstap.Plaats.Naam))
                .ForMember(dest => dest.BestemmingNaam,
                    opts => opts.MapFrom(
                        src => src.Bestemming.Plaats.Naam))
                ;

            CreateMap<Maaltijd, MaaltijdVM>()
                .ForMember(dest => dest.ExtraPrijs,
                    opts => opts.MapFrom(
                        src => src.Prijs))
                .ForMember(dest => dest.SoortMaaltijd,
                    opts => opts.MapFrom(
                        src => src.Soort));
            CreateMap<Reisklasse, ReisklasseVM>();
            CreateMap<Seizoen, SeizoenVM>();
            CreateMap<Zitplaat, ZitplaatsVM>();
            CreateMap<Ticket, TicketVM>();
                
                

        }
    }
}
