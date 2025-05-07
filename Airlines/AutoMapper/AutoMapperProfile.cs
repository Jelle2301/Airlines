using Airlines.ViewModels;
using AutoMapper;
using Domains.Entities;

namespace Airlines.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Plaat, PlaatsVM>();
           
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
                /*.ForMember(dest => dest.OverstapNaam,
                    opts => opts.MapFrom(
                        src => src.Overstap.Plaats.Naam))*/
                .ForMember(dest => dest.BestemmingNaam,
                    opts => opts.MapFrom(
                        src => src.Bestemming.Plaats.Naam))
                .ForMember(dest => dest.IsOverstap,
                    opts => opts.MapFrom(
                        src => src.IsOverstap))
                .ForMember(dest => dest.VertrekCode,
                    opts => opts.MapFrom(
                        src => src.Vertrekplaats.Plaats.Code))
                .ForMember(dest => dest.BestemmingCode,
                    opts => opts.MapFrom(
                        src => src.Bestemming.Plaats.Code))
                ;

            CreateMap<Maaltijd, MaaltijdVM>()
                .ForMember(dest => dest.ExtraPrijs,
                    opts => opts.MapFrom(
                        src => src.Prijs))
                .ForMember(dest => dest.SoortMaaltijd,
                    opts => opts.MapFrom(
                        src => src.Soort));
            CreateMap<Reisklasse, ReisklasseVM>()
                .ForMember(dest => dest.ExtraPrijsReisklasse,
                    opts => opts.MapFrom(
                        src => src.ExtraPrijs))
                ;
            CreateMap<Seizoen, SeizoenVM>();
            CreateMap<Zitplaat, ZitplaatsVM>();
            CreateMap<Ticket, TicketVM>();

            //van VM naar Entity
            CreateMap<PlaatsVM, Plaat>();
            CreateMap<VluchtVM, Vlucht>()
                .ForMember(dest => dest.Prijs,
                    opts => opts.MapFrom(
                        src => src.BeginPrijs))
                .ForPath(dest => dest.Vliegtuig.VliegtuigNaam,
                    opts => opts.MapFrom(
                        src => src.VliegtuigNaam))
                .ForPath(dest => dest.Vliegtuig.AantalTotalePlaatsen,
                    opts => opts.MapFrom(
                        src => src.AantalTotalePlaatsen))
                .ForPath(dest => dest.Vliegtuig.AantalGewonePlaatsen,
                    opts => opts.MapFrom(
                        src => src.AantalGewonePlaatsen))
                .ForPath(dest => dest.Vliegtuig.AantalBusinessPlaatsen,
                    opts => opts.MapFrom(
                        src => src.AantalBusinessPlaatsen))
                .ForPath(dest => dest.Vliegtuig.AantalEconomyPlaatsen,
                    opts => opts.MapFrom(
                        src => src.AantalEconomyPlaatsen))
                .ForPath(dest => dest.Vertrekplaats.Plaats.Naam,
                    opts => opts.MapFrom(
                        src => src.VertrekNaam))
                /*.ForMember(dest => dest.OverstapNaam,
                    opts => opts.MapFrom(
                        src => src.Overstap.Plaats.Naam))*/
                .ForPath(dest => dest.Bestemming.Plaats.Naam,
                    opts => opts.MapFrom(
                        src => src.BestemmingNaam))
                .ForMember(dest => dest.IsOverstap,
                    opts => opts.MapFrom(
                        src => src.IsOverstap))
                .ForPath(dest => dest.Vertrekplaats.Plaats.Code,
                    opts => opts.MapFrom(
                        src => src.VertrekCode))
                .ForPath(dest => dest.Bestemming.Plaats.Code,
                    opts => opts.MapFrom(
                        src => src.BestemmingCode))
                ;


            CreateMap<MaaltijdVM, Maaltijd>()
                .ForMember(dest => dest.Prijs,
                    opts => opts.MapFrom(
                        src => src.ExtraPrijs))
                .ForMember(dest => dest.Soort,
                    opts => opts.MapFrom(
                        src => src.SoortMaaltijd));
            CreateMap<ReisklasseVM, Reisklasse>()
                .ForMember(dest => dest.ExtraPrijs,
                    opts => opts.MapFrom(
                        src => src.ExtraPrijsReisklasse))
                ;
            CreateMap<SeizoenVM, Seizoen>();
            CreateMap<ZitplaatsVM, Zitplaat>();
            CreateMap<TicketVM, Ticket>();
            

            CreateMap<AspNetUser, UserVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<Plaat, PlaatsVM>()
                .ForMember(dest => dest.Naam, opt => opt.MapFrom(src => src.Naam));
            CreateMap<BoekingVM, Boeking>();
        }
    }
}
