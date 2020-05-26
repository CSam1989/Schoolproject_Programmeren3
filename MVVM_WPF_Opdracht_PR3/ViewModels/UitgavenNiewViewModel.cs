using BLL_Opdracht_PR.BaseViewModel;
using BLL_Opdracht_PR.ModelDto;
using BLL_Opdracht_PR.Services;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MVVM_WPF_Opdracht_PR3.ViewModels
{
    public class UitgavenNiewViewModel : BaseViewModelDataError
    {
        #region Constructor met DI
        private readonly IDataService _dataservice;
        private readonly IFrameNavigationService _navigation;

        public UitgavenNiewViewModel(
            IDataService dataservice,
            IFrameNavigationService navigation)
        {
            this._dataservice = dataservice;
            this._navigation = navigation;

            //Voor Combobox op te vullen!
            LijstGezinnen = _dataservice.GetAllGezinnen();

            //Initial values voor Prijs, Datum & GezinCombobox
            LeegmakenTextBoxen();
        }
        #endregion

        #region Properties
        #region LijstGezinnen
        /// <summary>
        /// The <see cref="LijstGezinnen" /> property's name.
        /// </summary>
        public const string LijstGezinnenPropertyName = "LijstGezinnen";

        private ObservableCollection<GezinForUitgaveLijstDto> _lijstGezinnen;

        /// <summary>
        /// Sets and gets the LijstGezinnen property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<GezinForUitgaveLijstDto> LijstGezinnen
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

        #region Gezin
        /// <summary>
        /// The <see cref="Gezin" /> property's name.
        /// </summary>
        public const string GezinPropertyName = "Gezin";

        private GezinForUitgaveLijstDto _gezin;

        /// <summary>
        /// Sets and gets the Gezin property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public GezinForUitgaveLijstDto Gezin
        {
            get
            {
                return _gezin;
            }
            set
            {
                Set(GezinPropertyName, ref _gezin, value);
            }
        }
        #endregion

        #region Plaats
        /// <summary>
        /// The <see cref="Plaats" /> property's name.
        /// </summary>
        public const string PlaatsPropertyName = "Plaats";

        private string _plaats;

        /// <summary>
        /// Sets and gets the Plaats property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Plaats
        {
            get
            {
                return _plaats;
            }
            set
            {
                Set(PlaatsPropertyName, ref _plaats, value);
            }
        }
        #endregion

        #region Prijs
        /// <summary>
        /// The <see cref="Prijs" /> property's name.
        /// </summary>
        public const string PrijsPropertyName = "Prijs";

        private decimal _prijs;

        /// <summary>
        /// Sets and gets the Prijs property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal Prijs
        {
            get
            {
                return _prijs;
            }
            set
            {
                Set(PrijsPropertyName, ref _prijs, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }
        #endregion

        #region UitgaveDatum
        /// <summary>
        /// The <see cref="UitgaveDatum" /> property's name.
        /// </summary>
        public const string UitgaveDatumPropertyName = "UitgaveDatum";

        private DateTime _uitgaveDatum;

        /// <summary>
        /// Sets and gets the UitgaveDatum property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime UitgaveDatum
        {
            get
            {
                return _uitgaveDatum;
            }
            set
            {
                Set(UitgaveDatumPropertyName, ref _uitgaveDatum, value);
            }
        }
        #endregion

        #region Opmerking
        /// <summary>
        /// The <see cref="Opmerking" /> property's name.
        /// </summary>
        public const string OpmerkingPropertyName = "Opmerking";

        private string _opmerking;

        /// <summary>
        /// Sets and gets the Opmerking property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Opmerking
        {
            get
            {
                return _opmerking;
            }
            set
            {
                Set(OpmerkingPropertyName, ref _opmerking, value);
            }
        }
        #endregion

        #region DataError
        public override string this[string columnName]
        {
            get
            {
                string result = null;
                /// <summary>
                ///     IsValid methode geeft pas true terug als gezin in geselecteerd
                ///     Combobox wordt opgevuld bij het laden van de view, dus er staat steeds een waarde in
                ///     Deze controle is redundant, maar toch voor extra veilig te zijn
                /// </summary>
                if (columnName == "Gezin" && Gezin == null)
                    result = "Gezin is een verplicht veld";

                if (columnName == "Prijs" && Prijs < 0)
                    result = "Prijs mag niet negatief zijn";
                return result;
            }
        }
        #endregion
        #endregion

        #region Commands
        #region UitgaveToevoegen
        private RelayCommand _uitgaveToevoegen;

        /// <summary>
        /// Gets the UitgaveToevoegen.
        /// </summary>
        public RelayCommand UitgaveToevoegen
        {
            get
            {
                return _uitgaveToevoegen
                    ?? (_uitgaveToevoegen = new RelayCommand(
                    () =>
                    {
                        UitgavenForCreateDto uitgave = new UitgavenForCreateDto
                        {
                            GezinID = this.Gezin.ID,
                            Plaats = this.Plaats,
                            Prijs = this.Prijs,
                            UitgaveDatum = this.UitgaveDatum,
                            Opmerking = this.Opmerking
                        };

                        LeegmakenTextBoxen();

                        _dataservice.UitgaveToevoegen(uitgave);
                        MessengerInstance.Send("UpdateLijst"); //Updates Lijst Uitgave bij UitgavenLijstVM & TeBetalenVM
                        _navigation.NavigateTo("UitgavenLijst");
                    },
                    () =>
                    {
                        return IsValid();
                    }));
            }
        }
        #endregion

        #region Annuleren
        private RelayCommand _annuleren;

        /// <summary>
        /// Gets the Annuleren.
        /// </summary>
        public RelayCommand Annuleren
        {
            get
            {
                return _annuleren
                    ?? (_annuleren = new RelayCommand(
                    () =>
                    {

                        LeegmakenTextBoxen(); 

                        if (_navigation.Historic.Count > 1)
                            _navigation.GoBack();
                        else
                            _navigation.NavigateTo("Home");
                    }));
            }
        }
        #endregion
        #endregion

        #region Methods
        public override void LeegmakenTextBoxen()
        {
            Gezin = LijstGezinnen[0];
            Plaats = "";
            Prijs = 1;
            UitgaveDatum = DateTime.Now;
            Opmerking = "";
        }
        #endregion
    }
}
