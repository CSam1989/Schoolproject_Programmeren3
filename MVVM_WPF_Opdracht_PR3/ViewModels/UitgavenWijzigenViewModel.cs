using BLL_Opdracht_PR.BaseViewModel;
using BLL_Opdracht_PR.ModelDto;
using BLL_Opdracht_PR.Services;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MVVM_WPF_Opdracht_PR3.ViewModels
{
    public class UitgavenWijzigenViewModel : BaseViewModelDataError
    {
        #region Constructor met DI
        private readonly IDataService _dataservice;
        private readonly IFrameNavigationService _navigation;

        public UitgavenWijzigenViewModel(
            IDataService dataservice,
            IFrameNavigationService navigation)
        {
            //Deze message komt van UitgaveLijstVM
            MessengerInstance.Register<int>(this, GetTeWijzigenUitgave);

            this._dataservice = dataservice;
            this._navigation = navigation;

            //Voor Combobox op te vullen!
            LijstGezinnen = _dataservice.GetAllGezinnen();
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

        #region UitgaveID
        /// <summary>
        /// The <see cref="UitgaveID" /> property's name.
        /// </summary>
        public const string UitgaveIDPropertyName = "UitgaveID";

        private int _id;

        /// <summary>
        /// Sets and gets the UitgaveID property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int UitgaveID
        {
            get
            {
                return _id;
            }
            set
            {
                Set(UitgaveIDPropertyName, ref _id, value);
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

        #region IsVerekend
        /// <summary>
        /// The <see cref="IsVerekend" /> property's name.
        /// </summary>
        public const string IsVerekendPropertyName = "IsVerekend";

        private bool _isVerekend = false;

        /// <summary>
        /// Sets and gets the IsVerekend property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsVerekend
        {
            get
            {
                return _isVerekend;
            }
            set
            {
                Set(IsVerekendPropertyName, ref _isVerekend, value);
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
        #region UitgaveWijzigen
        private RelayCommand _uitgaveWijzigen;

        /// <summary>
        /// Gets the UitgaveToevoegen.
        /// </summary>
        public RelayCommand UitgaveWijzigen
        {
            get
            {
                return _uitgaveWijzigen
                    ?? (_uitgaveWijzigen = new RelayCommand(
                    () =>
                    {
                        UitgavenForWijzigDto uitgave = new UitgavenForWijzigDto
                        {
                            ID = this.UitgaveID,
                            GezinID = this.Gezin.ID,
                            Plaats = this.Plaats,
                            Prijs = this.Prijs,
                            UitgaveDatum = this.UitgaveDatum,
                            Opmerking = this.Opmerking,
                            IsVerekend = this.IsVerekend

                        };

                        LeegmakenTextBoxen();

                        _dataservice.UitgaveWijzigen(uitgave);
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

                        _navigation.GoBack();
                    }));
            }
        }
        #endregion
        #endregion

        #region Methods
        private void GetTeWijzigenUitgave(int uitgaveID)
        {
            var uitgave = _dataservice.GetUitgaveByID(uitgaveID);

            // Bij Gezin = uitgave.Gezin, werkt het niet omdat dat 2 verschillende objecten zijn
            // Onderstaande werkwijze zorgt ervoor dat hij hetzelfde object gaat gebruiken om te tonen
            Gezin = LijstGezinnen.FirstOrDefault(g => g.ID == uitgave.Gezin.ID);

            UitgaveID = uitgave.ID;
            Plaats = uitgave.Plaats;
            Prijs = uitgave.Prijs;
            UitgaveDatum = uitgave.UitgaveDatum;
            Opmerking = uitgave.Opmerking;
            IsVerekend = uitgave.IsVerekend;
        }

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
