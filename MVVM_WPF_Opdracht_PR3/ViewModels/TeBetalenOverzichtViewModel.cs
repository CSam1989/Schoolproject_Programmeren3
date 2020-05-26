using BLL_Opdracht_PR.Logic;
using BLL_Opdracht_PR.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;

namespace MVVM_WPF_Opdracht_PR3.ViewModels
{
    public class TeBetalenOverzichtViewModel : ViewModelBase
    {
        #region Constructor met DI
        private readonly ITeBetalenLogic _teBetalenLogic;
        private readonly IFrameNavigationService _navigation;

        public TeBetalenOverzichtViewModel(
            ITeBetalenLogic teBetalenLogic,
            IFrameNavigationService navigation)
        {

            this._teBetalenLogic = teBetalenLogic;
            this._navigation = navigation;

            //Dit wordt gedaan om bij het initialiseren al gegevens in de lijst te hebben
            //Wordt maar 1 keer geinitialiseerd bij VMLocator
            DictTeBetalenPerGezin = _teBetalenLogic.GetTeBetalenPerGezin();

            //Zelfde als bij UitgavenOverzicht, bij elke wijziging in de lijst wordt deze VM ook geupdate!
            MessengerInstance.Register<string>(this, UpdateLijst);
        }
        #endregion

        #region Properties
        /// <summary>
        /// The <see cref="DictTeBetalenPerGezin" /> property's name.
        /// </summary>
        public const string DictTeBetalPerGezinPropertyName = "DictTeBetalPerGezin";

        private Dictionary<string, decimal> _dictTeBetalenPerGezin;

        /// <summary>
        /// Sets and gets the DictTeBetalPerGezin property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Dictionary<string, decimal> DictTeBetalenPerGezin
        {
            get
            {
                return _dictTeBetalenPerGezin;
            }
            set
            {
                Set(DictTeBetalPerGezinPropertyName, ref _dictTeBetalenPerGezin, value);
            }
        }

        /// <summary>
        /// The <see cref="IsOpen" /> property's name.
        /// </summary>
        public const string IsOpenPropertyName = "IsOpen";

        private bool _isOpen;

        /// <summary>
        /// Sets and gets the IsOpen property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                Set(IsOpenPropertyName, ref _isOpen, value);
            }
        }
        #endregion

        #region Commands
        private RelayCommand<bool> _TeBetalenVerwerkenCommand;

        /// <summary>
        /// Gets the TeBetalenVerwerkenCommand.
        /// </summary>
        public RelayCommand<bool> TeBetalenVerwerkenCommand
        {
            get
            {
                return _TeBetalenVerwerkenCommand
                    ?? (_TeBetalenVerwerkenCommand = new RelayCommand<bool>(
                    p =>
                    {
                        _teBetalenLogic.SetAllUitgavenToVerekend();


                        IsOpen = false;

                        MessengerInstance.Send("UpdateLijst"); //Updates Lijst Uitgave bij UitgavenLijstVM & TeBetalenVM

                        //Voor de page te refreshen, WERKT NIET!!!!
                        //Dictionary wordt niet gerefreshed
                        //Enkel als je van een andere pagina navigeert, dan wordt de dictionary wel gerefreshed, WAAROM???
                        _navigation.NavigateTo("Home");
                        ////Hier wordt de NotifyPropertyChanged niet opgeroepen omdat de nieuwe value niet verschilt!!
                        //_navigation.NavigateTo("TeBetalenOverzicht");
                    }));
            }
        }
        #endregion

        #region Methods
        private void UpdateLijst(string message)
        {
            if (message == "UpdateLijst")
            {
                DictTeBetalenPerGezin = _teBetalenLogic.GetTeBetalenPerGezin();
            }
        }
        #endregion
    }
}
