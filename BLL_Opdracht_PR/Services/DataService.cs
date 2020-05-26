using AutoMapper;
using BLL_Opdracht_PR.Helpers;
using BLL_Opdracht_PR.ModelDto;
using DAL_OPDRACHT_PR3.Models;
using DAL_OPDRACHT_PR3.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BLL_Opdracht_PR.Services
{
    public class DataService : IDataService
    {
        #region Constructor with DI

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DataService(IUnitOfWork UoW)
        {
            this._unitOfWork = UoW;

            //Configuration of AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();

                // Hier heb ik een aparte CreateMap gebruikt
                // Omwille van databasetoegang voor korting per gezin op te halen
                // In automapperprofile kan ik de dataservice niet gebruiken
                cfg.CreateMap<Gezin, GezinForUitgebreidLijstDto>()
                .ForMember(dest => dest.Korting, opt => opt.MapFrom(src => _unitOfWork.KortingRepo.GetKortingCoeffiecentPerGezin(src.ID)));
            });

            _mapper = new Mapper(config);
        }
        #endregion

        #region Methodes
        #region Uitgaven
        public ObservableCollection<UitgavenForLijstDto> GetAllUitgaven()
        {
            var uitgaven = _unitOfWork.UitgavenRepo.GetAllUitgaven();

            var uitgavenLijstDto = _mapper.Map<ObservableCollection<UitgavenForLijstDto>>(uitgaven);

            return uitgavenLijstDto;
        }

        public ObservableCollection<UitgavenForLijstDto> GetFilteredUitgavenPaged(int start, int itemCount, string sortColumn, bool descending, out int totalItems, string filter)
        {
            var uitgavenGefilterd = _unitOfWork.UitgavenRepo.GetFilteredUitgaven(filter);
            var uitgaven = _mapper.Map<ObservableCollection<UitgavenForLijstDto>>(uitgavenGefilterd);


            List<UitgavenForLijstDto> UitgavenGesorteerd;
            ObservableCollection<UitgavenForLijstDto> UitgavenGefilterd = new ObservableCollection<UitgavenForLijstDto>();

            totalItems = uitgaven.Count;

            //Datagrid heeft functie Sort on colums, maar als je paging gebruikt
            //Gaat die ingebouwde Sort enkel op die pagina sorteren (wat niet in orde is)
            //Dus Sorteren we hier op de hele Collectie
            switch (sortColumn)
            {
                case ("Prijs"):
                    UitgavenGesorteerd = uitgaven.OrderBy(u => u.Prijs).ToList();
                    break;
                case ("Plaats"):
                    UitgavenGesorteerd = uitgaven.OrderBy(u => u.Plaats).ToList();
                    break;
                case ("UitgaveDatum"):
                    UitgavenGesorteerd = uitgaven.OrderBy(u => u.UitgaveDatum).ToList();
                    break;
                case ("Gezin"):
                    UitgavenGesorteerd = uitgaven.OrderBy(u => u.Gezin.Gezinsnaam).ToList();
                    break;
                default:
                    UitgavenGesorteerd = uitgaven.OrderBy(u => u.ID).ToList();
                    break;
            }

            if (descending)
                UitgavenGesorteerd.Reverse();

            for (int i = start; i < start + itemCount && i < totalItems; i++)
            {
                UitgavenGefilterd.Add(UitgavenGesorteerd[i]);
            }

            return UitgavenGefilterd;
        }

        public ObservableCollection<UitgavenForTeBetalenDto> GetUitgavenByIsVerekend(bool isVerekend)
        {
            var uitgaven = _unitOfWork.UitgavenRepo.GetUitgavenByIsVerekend(isVerekend);

            var uitgavenDto = _mapper.Map<ObservableCollection<UitgavenForTeBetalenDto>>(uitgaven);

            return uitgavenDto;
        }

        public ObservableCollection<UitgavenForTeBetalenDto> GetUitgaveByIsVerekendAndByGezin(bool isVerekend, int gezinsID)
        {
            var uitgaven = _unitOfWork.UitgavenRepo.GetUItgavenByIsVerekendAndByGezin(isVerekend, gezinsID);

            var uitgavenDto = _mapper.Map<ObservableCollection<UitgavenForTeBetalenDto>>(uitgaven);

            return uitgavenDto;
        }

        public UitgavenForLijstDto GetUitgaveByID(int uitgaveID)
        {
            var uitgave = _unitOfWork.UitgavenRepo.GetUitgaveByID(uitgaveID);

            var uitgaveDto = _mapper.Map<UitgavenForLijstDto>(uitgave);

            return uitgaveDto;
        }

        public void UitgaveToevoegen(UitgavenForCreateDto uitgavenForCreateDto)
        {
            var uitgaveNieuw = _mapper.Map<Uitgave>(uitgavenForCreateDto);

            _unitOfWork.UitgavenRepo.Insert(uitgaveNieuw);
            _unitOfWork.SaveAll();
        }

        public void UitgaveWijzigen(UitgavenForWijzigDto uitgaveDto)
        {
            var uitgave = _mapper.Map<Uitgave>(uitgaveDto);

            _unitOfWork.UitgavenRepo.Update(uitgave);
            _unitOfWork.SaveAll();
        }

        public void UitgaveVerwijderen(int uitgaveID)
        {
            var uitgaven = GetAllUitgaven();

            if (uitgaven.FirstOrDefault(u => u.ID == uitgaveID) != null)
                _unitOfWork.UitgavenRepo.Delete(uitgaveID);

            _unitOfWork.SaveAll();
        }
        #endregion

        #region Gezinnen
        public ObservableCollection<GezinForUitgaveLijstDto> GetAllGezinnen()
        {
            var gezinnen = _unitOfWork.GezinRepo.GetAllGezinnen();

            var gezinnenForLijstDto = _mapper.Map<ObservableCollection<GezinForUitgaveLijstDto>>(gezinnen);

            return gezinnenForLijstDto;
        }

        public ICollection<GezinForUitgebreidLijstDto> GetAllGezinnenUitgebreid()
        {
            var gezinnen = _unitOfWork.GezinRepo.GetAllGezinnenUitgebreid();

            var gezinnnenForLijstDto = _mapper.Map<ICollection<GezinForUitgebreidLijstDto>>(gezinnen);

            return gezinnnenForLijstDto;
        }
        #endregion

        #region Kortingen
        public double GetTotaleKortingCoefficient()
        {
            return _unitOfWork.KortingRepo.GetTotaleKortingCoefficient();
        }

        public double GetKortingCoeffiecentPerGezin(int gezinsID)
        {
            return _unitOfWork.KortingRepo.GetKortingCoeffiecentPerGezin(gezinsID);
        }
        #endregion
        #endregion
    }
}
