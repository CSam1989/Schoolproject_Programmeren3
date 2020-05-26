
using BLL_Opdracht_PR.ModelDto;
using BLL_Opdracht_PR.Services;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace MVVM_WPF_Opdracht_PR3.ViewModels
{
    public class PersoonOverzichtViewModel : ViewModelBase
    {
        #region Constructor met DI
        private readonly IDataService _dataservice;

        public PersoonOverzichtViewModel(IDataService dataservice)
        {
            this._dataservice = dataservice;

            LijstGezinnen = _dataservice.GetAllGezinnenUitgebreid();
        }
        #endregion

        #region Properties
        #region LijstGezinnen
        /// <summary>
        /// The <see cref="LijstGezinnen" /> property's name.
        /// </summary>
        public const string LijstGezinnenPropertyName = "LijstGezinnen";

        private ICollection<GezinForUitgebreidLijstDto> _lijstGezinnen;

        /// <summary>
        /// Sets and gets the LijstGezinnen property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ICollection<GezinForUitgebreidLijstDto> LijstGezinnen
        {
            get
            {
                return _lijstGezinnen;
            }
            set
            {
                Set(LijstGezinnenPropertyName, ref _lijstGezinnen, value);
            }
        }
        #endregion

        #region ID
        /// <summary>
        /// The <see cref="ID" /> property's name.
        /// </summary>
        public const string IDPropertyName = "ID";

        private int _id;

        /// <summary>
        /// Sets and gets the ID property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                Set(IDPropertyName, ref _id, value);
            }
        }
        #endregion

        #region Gezinsnaam
        /// <summary>
        /// The <see cref="Gezinsnaam" /> property's name.
        /// </summary>
        public const string GezinsnaamPropertyName = "Gezinsnaam";

        private string _gezinsnaam;

        /// <summary>
        /// Sets and gets the Gezinsnaam property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Gezinsnaam
        {
            get
            {
                return _gezinsnaam;
            }
            set
            {
                Set(GezinsnaamPropertyName, ref _gezinsnaam, value);
            }
        }
        #endregion

        #region Straat
        /// <summary>
        /// The <see cref="Straat" /> property's name.
        /// </summary>
        public const string StraatPropertyName = "Straat";

        private string _straat;

        /// <summary>
        /// Sets and gets the Straat property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Straat
        {
            get
            {
                return _straat;
            }
            set
            {
                Set(StraatPropertyName, ref _straat, value);
            }
        }
        #endregion

        #region Huisnummer
        /// <summary>
        /// The <see cref="Huisnummer" /> property's name.
        /// </summary>
        public const string HuisnummerPropertyName = "Huisnummer";

        private string _huisnummer;

        /// <summary>
        /// Sets and gets the Huisnummer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Huisnummer
        {
            get
            {
                return _huisnummer;
            }
            set
            {
                Set(HuisnummerPropertyName, ref _huisnummer, value);
            }
        }
        #endregion

        #region Gemeente
        /// <summary>
        /// The <see cref="Gemeente" /> property's name.
        /// </summary>
        public const string GemeentePropertyName = "Gemeente";

        private string _gemeente;

        /// <summary>
        /// Sets and gets the Gemeente property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Gemeente
        {
            get
            {
                return _gemeente;
            }
            set
            {
                Set(GemeentePropertyName, ref _gemeente, value);
            }
        }
        #endregion

        #region Personen
        #region LijstPersonen
        /// <summary>
        /// The <see cref="Personen" /> property's name.
        /// </summary>
        public const string PersonenPropertyName = "Personen";

        private ICollection<PersonenForGezinLijstDto> _personen;

        /// <summary>
        /// Sets and gets the Personen property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ICollection<PersonenForGezinLijstDto> Personen
        {
            get
            {
                return _personen;
            }
            set
            {
                Set(PersonenPropertyName, ref _personen, value);
            }
        }
        #endregion

        #region Naam
        /// <summary>
        /// The <see cref="Naam" /> property's name.
        /// </summary>
        public const string NaamPropertyName = "Naam";

        private string _naam;

        /// <summary>
        /// Sets and gets the Naam property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Naam
        {
            get
            {
                return _naam;
            }
            set
            {
                Set(NaamPropertyName, ref _naam, value);
            }
        }
        #endregion

        #region Voornaam
        /// <summary>
        /// The <see cref="Voornaam" /> property's name.
        /// </summary>
        public const string VoornaamPropertyName = "Voornaam";

        private string _voornaam;

        /// <summary>
        /// Sets and gets the Voornaam property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Voornaam
        {
            get
            {
                return _voornaam;
            }
            set
            {
                Set(VoornaamPropertyName, ref _voornaam, value);
            }
        }
        #endregion

        #region Leeftijd
        /// <summary>
        /// The <see cref="Leeftijd" /> property's name.
        /// </summary>
        public const string LeeftijdPropertyName = "Leeftijd";

        private int _leeftijd;

        /// <summary>
        /// Sets and gets the Leeftijd property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Leeftijd
        {
            get
            {
                return _leeftijd;
            }
            set
            {
                Set(LeeftijdPropertyName, ref _leeftijd, value);
            }
        }
        #endregion
        #endregion

        #region Korting
        /// <summary>
        /// The <see cref="Korting" /> property's name.
        /// </summary>
        public const string KortingPropertyName = "Korting";

        private double _korting;

        /// <summary>
        /// Sets and gets the Korting property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Korting
        {
            get
            {
                return _korting;
            }
            set
            {
                Set(KortingPropertyName, ref _korting, value);
            }
        }
        #endregion
        #endregion
    }
}
