using BLL_Opdracht_PR.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MVVM_WPF_Opdracht_PR3.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructor met DI
        private readonly IFrameNavigationService _navigation;

        public MainViewModel(IFrameNavigationService navigation)
        {
            //Dependency Injection (aangemaakt in ViewModelLocator)
            this._navigation = navigation;
        }
        #endregion

        #region Commands voor Navigatie
        #region UitgavenLijst
        private RelayCommand _uitgavenLijstCommand;

        /// <summary>
        /// Gets the UitgavenLijstCommand.
        /// </summary>
        public RelayCommand UitgavenLijstCommand
        {
            get
            {
                return _uitgavenLijstCommand
                    ?? (_uitgavenLijstCommand = new RelayCommand(
                    () =>
                    {
                        MessengerInstance.Send("UpdateLijst"); //Haalt Lijst Uitgave op bij UitgavenLijstVM
                        _navigation.NavigateTo("UitgavenLijst");
                    }));
            }
        }
        #endregion

        #region UitgavenNieuw
        private RelayCommand _uitgavenNieuwCommand;

        /// <summary>
        /// Gets the UitgavenLijstCommand.
        /// </summary>
        public RelayCommand UitgavenNieuwCommand
        {
            get
            {
                return _uitgavenNieuwCommand
                    ?? (_uitgavenNieuwCommand = new RelayCommand(
                    () =>
                    {
                        _navigation.NavigateTo("UitgavenNieuw");
                    }));
            }
        }
        #endregion

        #region TeBetalenOverzicht
        private RelayCommand _teBetalenOverzicht;

        /// <summary>
        /// Gets the TeBetalenOverzicht.
        /// </summary>
        public RelayCommand TeBetalenOverzicht
        {
            get
            {
                return _teBetalenOverzicht
                    ?? (_teBetalenOverzicht = new RelayCommand(
                    () =>
                    {
                        MessengerInstance.Send("UpdateLijst"); //Haalt Lijst TeBetalen op bij TeBetalenVM
                        _navigation.NavigateTo("TeBetalenOverzicht");
                    }));
            }
        }
        #endregion

        #region PersoonOverzicht
        private RelayCommand _persoonOverzicht;

        /// <summary>
        /// Gets the PersoonOverzicht.
        /// </summary>
        public RelayCommand PersoonOverzicht
        {
            get
            {
                return _persoonOverzicht
                    ?? (_persoonOverzicht = new RelayCommand(
                    () =>
                    {
                        _navigation.NavigateTo("PersoonOverzicht");
                    }));
            }
        }
        #endregion
        #endregion
    }
}
