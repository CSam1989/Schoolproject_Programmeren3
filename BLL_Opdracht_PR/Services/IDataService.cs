using BLL_Opdracht_PR.ModelDto;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BLL_Opdracht_PR.Services
{
    //Interface aanmaken voor DI te kunnen gebruiken
    public interface IDataService
    {
        #region Uitgaven
        ObservableCollection<UitgavenForLijstDto> GetAllUitgaven();

        ObservableCollection<UitgavenForLijstDto> GetFilteredUitgavenPaged(int start, int itemCount, string sortColumn, bool descending, out int totalItems, string filter);

        ObservableCollection<UitgavenForTeBetalenDto> GetUitgavenByIsVerekend(bool IsVerekend);

        ObservableCollection<UitgavenForTeBetalenDto> GetUitgaveByIsVerekendAndByGezin(bool IsVerekend, int gezinsID);

        UitgavenForLijstDto GetUitgaveByID(int uitgaveID);

        void UitgaveToevoegen(UitgavenForCreateDto uitgave);

        void UitgaveWijzigen(UitgavenForWijzigDto uitgave);

        void UitgaveVerwijderen(int uitgaveID);
        #endregion

        #region Gezinnen
        ObservableCollection<GezinForUitgaveLijstDto> GetAllGezinnen();

        ICollection<GezinForUitgebreidLijstDto> GetAllGezinnenUitgebreid();
        #endregion

        #region Kortingen
        double GetTotaleKortingCoefficient();

        double GetKortingCoeffiecentPerGezin(int gezinsID);
        #endregion
    }
}
