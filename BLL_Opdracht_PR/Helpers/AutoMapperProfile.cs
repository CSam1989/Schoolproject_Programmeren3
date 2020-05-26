using AutoMapper;
using BLL_Opdracht_PR.ModelDto;
using DAL_OPDRACHT_PR3.Models;

namespace BLL_Opdracht_PR.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Uitgave Dto's
            CreateMap<Uitgave, UitgavenForLijstDto>();
            CreateMap<UitgavenForLijstDto, Uitgave>();

            CreateMap<UitgavenForCreateDto, Uitgave>();

            CreateMap<UitgavenForWijzigDto, Uitgave>();

            CreateMap<Uitgave, UitgavenForTeBetalenDto>();

            CreateMap<UitgavenForTeBetalenDto, UitgavenForWijzigDto>();
            #endregion

            #region Gezin Dto's
            CreateMap<Gezin, GezinForUitgaveLijstDto>();
            CreateMap<GezinForUitgaveLijstDto, Gezin>();


            #endregion

            #region Personen Dto's
            CreateMap<Persoon, PersonenForGezinLijstDto>()
                .ForMember(dest => dest.Leeftijd, opt => opt.MapFrom(src => src.Geboortedatum.Age()));
            #endregion

            #region Gemeente Dto's
            CreateMap<Gemeente, GemeenteForGezinLijstDto>();
            #endregion
        }
    }
}
