using AutoMapper;
using BLL_Opdracht_PR.Helpers;
using BLL_Opdracht_PR.ModelDto;
using BLL_Opdracht_PR.Services;
using System.Collections.Generic;

namespace BLL_Opdracht_PR.Logic
{
    public class TeBetalenLogic : ITeBetalenLogic
    {
        #region Constructor met DI
        private readonly IDataService _dataservice;
        private readonly IMapper _mapper;

        public TeBetalenLogic(IDataService dataservice)
        {
            this._dataservice = dataservice;

            //Configuration of AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = new Mapper(config);
        }
        #endregion

        #region Methodes
        public decimal GetTotaleNietVerekendeUitgaven()
        {
            decimal TotaleUitgaven = 0;
            var lijstUitgaven = _dataservice.GetUitgavenByIsVerekend(false);

            foreach (var uitgave in lijstUitgaven)
            {
                TotaleUitgaven += uitgave.Prijs;
            }

            return TotaleUitgaven;
        }

        // In deze staat is deze methode niet te unit testen
        // Bij de setup van de dataservice kan ik geen 2x dezelfde methode mocken
        public Dictionary<string, decimal> GetTeBetalenPerGezin()
        {
            Dictionary<string, decimal> teBetalenPerGezin = new Dictionary<string, decimal>();
            var gezinnen = _dataservice.GetAllGezinnen();
            decimal totaleKortingsCoefficient = (decimal)(_dataservice.GetTotaleKortingCoefficient());

            foreach (var gezin in gezinnen)
            {
                decimal totaleUitgavenPerGezin = 0;
                var lijstUitgavenPerGezin = _dataservice.GetUitgaveByIsVerekendAndByGezin(false, gezin.ID);
                var kortingsCoefficientPerGezin = (decimal)(_dataservice.GetKortingCoeffiecentPerGezin(gezin.ID));

                foreach (var uitgave in lijstUitgavenPerGezin)
                {
                    totaleUitgavenPerGezin += uitgave.Prijs;
                }

                var teBetalen = ((GetTotaleNietVerekendeUitgaven() / totaleKortingsCoefficient) * kortingsCoefficientPerGezin) - totaleUitgavenPerGezin;

                teBetalenPerGezin.Add(gezin.Gezinsnaam, teBetalen);
            }

            return teBetalenPerGezin;
        }

        public void SetAllUitgavenToVerekend()
        {
            var lijstUitgaven = _dataservice.GetUitgavenByIsVerekend(false);

            foreach (var uitgave in lijstUitgaven)
            {
                uitgave.IsVerekend = true;

                // In Dataservice de methode uitgavenwijzigen neemt een uitgavenForWijzigenDto object als argument
                var uitgaveWijzigenDto = _mapper.Map<UitgavenForWijzigDto>(uitgave);

                _dataservice.UitgaveWijzigen(uitgaveWijzigenDto);
            }
        }
        #endregion
    }
}
