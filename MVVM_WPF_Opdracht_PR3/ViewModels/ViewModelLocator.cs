using CommonServiceLocator;
using DAL_OPDRACHT_PR3.Resources;
using BLL_Opdracht_PR.Services;
using BLL_Opdracht_PR.Logic;
using GalaSoft.MvvmLight.Ioc;
using System;

namespace MVVM_WPF_Opdracht_PR3.ViewModels
{
    public class ViewModelLocator
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


            // Create run time view services and models
            //- Dit is voor Dependency Injection
            SimpleIoc.Default.Register<IUnitOfWork, UnitOfWork>();
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<ITeBetalenLogic, TeBetalenLogic>();
            SetupNavigation();

            //ViewModels registreren
            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<UitgavenLijstViewModel>();
            SimpleIoc.Default.Register<UitgavenNiewViewModel>();
            /// <summary>
            ///     geregistreerde VMs zijn standaard lazy loaded singletons
            ///     Door "createInstanceImmediatly" op true te zetten gaan we Lazy Loading afzetten 
            ///     => Eager loading
            /// </summary>
            SimpleIoc.Default.Register<UitgavenWijzigenViewModel>(true); //
            SimpleIoc.Default.Register<TeBetalenOverzichtViewModel>();
            SimpleIoc.Default.Register<PersoonOverzichtViewModel>();
        }
        #endregion

        #region Properties
        #region Main
        /// <summary>
        /// Gets the ViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        #endregion

        #region UitgavenLijst
        /// <summary>
        /// Gets the UitgavenLijst property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UitgavenLijstViewModel UitgavenLijst
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UitgavenLijstViewModel>();
            }
        }
        #endregion

        #region UitgavenNieuw
        /// <summary>
        /// Gets the UitgavenNieuw property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UitgavenNiewViewModel UitgavenNieuw
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UitgavenNiewViewModel>();
            }
        }
        #endregion

        #region UitgavenWijzigen
        /// <summary>
        /// Gets the UitgavenWijzigen property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UitgavenWijzigenViewModel UitgavenWijzigen
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UitgavenWijzigenViewModel>();
            }
        }
        #endregion

        #region TeBetalenOverzicht
        /// <summary>
        /// Gets the TeBetalenOverzicht property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public TeBetalenOverzichtViewModel TeBetalenOverzicht
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeBetalenOverzichtViewModel>();
            }
        }
        #endregion

        #region PersoonOverzicht
        /// <summary>
        /// Gets the PersoonOverzicht property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PersoonOverzichtViewModel PersoonOverzicht
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PersoonOverzichtViewModel>();
            }
        }
        #endregion
        #endregion

        #region Methods
        #region Cleanup
        //- Dit is om memory leaks te voorkomen!
        //- Altijd gebruiken als je viewmodel niet meer nodig hebt
        //- bv bij het sluiten van een view
        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<UitgavenLijstViewModel>();
            SimpleIoc.Default.Unregister<UitgavenNiewViewModel>();
            SimpleIoc.Default.Unregister<UitgavenWijzigenViewModel>();
            SimpleIoc.Default.Unregister<TeBetalenOverzichtViewModel>();
            SimpleIoc.Default.Unregister<PersoonOverzichtViewModel>();

        }
        #endregion

        #region Navigation Setup
        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("Home", new Uri("../Views/HomeViewUC.xaml", UriKind.Relative));
            navigationService.Configure("UitgavenLijst", new Uri("../Views/UitgavenLijstViewUC.xaml", UriKind.Relative));
            navigationService.Configure("UitgavenNieuw", new Uri("../Views/UitgavenNieuwViewUC.xaml", UriKind.Relative));
            navigationService.Configure("UitgavenWijzigen", new Uri("../Views/UitgavenWijzigenViewUC.xaml", UriKind.Relative));
            navigationService.Configure("TeBetalenOverzicht", new Uri("../Views/TeBetalenOverzichtViewUC.xaml", UriKind.Relative));
            navigationService.Configure("PersoonOverzicht", new Uri("../Views/PersoonOverzichtViewUC.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }
        #endregion
        #endregion
    }
}
