using BLL_Opdracht_PR.ModelDto;
using BLL_Opdracht_PR.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;

namespace MVVM_WPF_Opdracht_PR3.ViewModels
{
    public class UitgavenLijstViewModel : ViewModelBase
    {
        #region Attributen
        private int _start = 0;
        private int _itemCount = 10;
        private bool _descending = false;
        private int _totalItems = 0;
        private List<string> _lijstTeSorteren = new List<string> { "ID", "Gezin", "Plaats", "Prijs", "UitgaveDatum" };
        #endregion

        #region Constructor met DI
        //Dependency Injection ingesteld bij ViewModelLocator
        private readonly IDataService _dataservice;
        private readonly IFrameNavigationService _navigation;

        public UitgavenLijstViewModel(
            IDataService dataservice,
            IFrameNavigationService navigation)
        {
            this._dataservice = dataservice;
            this._navigation = navigation;

            //SorteerVeld initialiseren op ID veld
            SorteerVeld = LijstSorteren[0];

            /// <summary>
            ///     Krijgt elke keer de boodschap om de lijst te updaten 
            ///     Wanneer er naar deze pagina genavigeerd wordt
            ///     Deze ViewModels worden via een IoC (Inversion of Control) container eermaal geinitialiseerd (Singleton??)
            ///     Dus de constructor wordt maar 1 keer opgeroepen,
            ///     Hiervoor moeten we met een Messenger werken, dat deze boodschap (broadcast) registreer
            /// 
            ///     We zouden deze broadcast door de navigationService kunnen laten versturen,
            ///     Maar dan wordt deze service Tightly Coupled met deze VM, dit willen we ten alle tijden vermijden
            ///     Dus  elke keer we de nav service gebruiken in een andere VM, moeten we deze broadcast verzenden vanuit die andere VM
            ///     
            ///     Deze string action gaat de lijst opvullen met dataservice.GetAllUitgaven() 
            /// </summary>
            MessengerInstance.Register<string>(this, UpdateLijst);

            //Dit wordt gedaan om bij het initialiseren al gegevens in de lijst te hebben
            //Wordt maar 1 keer geinitialiseerd bij VMLocator
            UpdateLijst();
            //LijstUitgaven = _dataservice.GetAllUitgaven();
        }
        #endregion

        #region Properties
        #region LijstUitgaven
        /// <summary>
        /// The <see cref="LijstUitgaven" /> property's name.
        /// </summary>
        public const string LijstUitgavenPropertyName = "LijstUitgaven";
        private ICollection<UitgavenForLijstDto> _lijstUitgaven;

        /// <summary>
        /// Sets and gets the LijstUitgaven property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ICollection<UitgavenForLijstDto> LijstUitgaven
        {
            get
            {
                return _lijstUitgaven;
            }
            set
            {
                Set(LijstUitgavenPropertyName, ref _lijstUitgaven, value);
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

        #region SelectedItem
        /// <summary>
        /// The <see cref="SelectedItem" /> property's name.
        /// </summary>
        public const string SelectedItemPropertyName = "SelectedItem";

        private UitgavenForLijstDto _selectedItem;

        /// <summary>
        /// Sets and gets the SelectedItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public UitgavenForLijstDto SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                Set(SelectedItemPropertyName, ref _selectedItem, value);
                ToonDetail();
            }
        }

        private void ToonDetail()
        {
            if (SelectedItem != null)
            {
                this.Prijs = SelectedItem.Prijs;
                this.Plaats = SelectedItem.Plaats;
                this.Opmerking = SelectedItem.Opmerking;
                this.UitgaveDatum = SelectedItem.UitgaveDatum;
            }

        }
        #endregion

        #region Paging, Sorting & Filtering Properties
        /// <summary>
        ///     De Paging Functie heb ik gevonden op http://www.nullskull.com/a/1368/wpf-datagrid-custom-paging-and-sorting.aspx
        ///     Sorting op bovenstaande link heb ik niet gebruikt omdat deze in code-behind is
        ///     Ik heb Sorting via MVVM principe toegepast
        ///     Paging heb ik wel overgenomen (mits enkele aanpassingen naar mijn project)
        ///     Special thanks to Michael Detras (Author)
        /// </summary>
        #region Start
        /// <summary>
        /// Gets the index of the first item in the products list.
        /// </summary>
        public int Start { get { return _start + 1; } }
        #endregion

        #region End
        /// <summary>
        /// Gets the index of the last item in the products list.
        /// </summary>
        public int End { get { return _start + _itemCount < _totalItems ? _start + _itemCount : _totalItems; } }
        #endregion

        #region TotalItems
        /// <summary>
        /// The number of total items in the data store.
        /// </summary>
        public int TotalItems { get { return _totalItems; } }
        #endregion

        #region LijstSorteren
        /// <summary>
        /// The <see cref="LijstSorteren" /> property's name.
        /// </summary>
        public const string LijstSorterenPropertyName = "LijstSorteren";

        /// <summary>
        /// Sets and gets the LijstSorteren property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<string> LijstSorteren
        {
            get
            {
                return _lijstTeSorteren;
            }
        }
        #endregion

        #region SorteerVeld
        /// <summary>
        /// The <see cref="SorteerVeld" /> property's name.
        /// </summary>
        public const string SorteerVeldPropertyName = "SorteerVeld";

        private string _sorteerVeld;

        /// <summary>
        /// Sets and gets the SorteerVeld property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SorteerVeld
        {
            get
            {
                return _sorteerVeld;
            }
            set
            {
                Set(SorteerVeldPropertyName, ref _sorteerVeld, value);
                UpdateLijst();
            }
        }
        #endregion

        #region Filter
        /// <summary>
        /// The <see cref="Filter" /> property's name.
        /// </summary>
        public const string FilterPropertyName = "Filter";

        private string _filter;

        /// <summary>
        /// Sets and gets the Filter property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                Set(FilterPropertyName, ref _filter, value);
            }
        }
        #endregion
        #endregion
        #endregion

        #region Commands
        #region VerwijderCommand
        private RelayCommand<int> _verwijderCommand;

        /// <summary>
        /// Gets the VerwijderCommand.
        /// </summary>
        public RelayCommand<int> VerwijderCommand
        {
            get
            {
                return _verwijderCommand
                    ?? (_verwijderCommand = new RelayCommand<int>(
                    p =>
                    {
                        _dataservice.UitgaveVerwijderen(p);
                        MessengerInstance.Send("UpdateLijst"); //Updates Lijst Uitgave bij UitgavenLijstVM & TeBetalenVM
                    }));
            }
        }
        #endregion

        #region WijzigCommand
        private RelayCommand<int> _uitgavenWijzigenCommand;

        /// <summary>
        /// Gets the UitgavenWijzigenCommand.
        /// </summary>
        public RelayCommand<int> UitgavenWijzigenCommand
        {
            get
            {
                return _uitgavenWijzigenCommand
                    ?? (_uitgavenWijzigenCommand = new RelayCommand<int>(
                    uitgaveID =>
                    {
                        _navigation.NavigateTo("UitgavenWijzigen");

                        //Hier wordt de ID verstuurd via broadcast & bij VM UitgaveWijzigen wordt deze broadcast opgevangen
                        MessengerInstance.Send(uitgaveID);
                    }));
            }
        }
        #endregion

        #region NiewCommand
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

        #region Paging, Sorting & Filter Commands
        #region FirstCommand
        private RelayCommand _firstCommand;

        /// <summary>
        /// Gets the FirstCommand.
        /// </summary>
        public RelayCommand FirstCommand
        {
            get
            {
                return _firstCommand
                    ?? (_firstCommand = new RelayCommand(
                    () =>
                    {
                        _start = 0;
                        UpdateLijst();
                    },
                    () => _start - _itemCount >= 0 ? true : false));
            }
        }
        #endregion

        #region PreviousCommand
        private RelayCommand _previousCommand;

        /// <summary>
        /// Gets the PreviousCommand.
        /// </summary>
        public RelayCommand PreviousCommand
        {
            get
            {
                return _previousCommand
                    ?? (_previousCommand = new RelayCommand(
                    () =>
                    {
                        _start -= _itemCount;
                        UpdateLijst();
                    },
                    () => _start - _itemCount >= 0 ? true : false));
            }
        }
        #endregion

        #region NextCommand
        private RelayCommand _nextCommand;

        /// <summary>
        /// Gets the NextCommand.
        /// </summary>
        public RelayCommand NextCommand
        {
            get
            {
                return _nextCommand
                    ?? (_nextCommand = new RelayCommand(
                    () =>
                    {
                        _start += _itemCount;
                        UpdateLijst();
                    },
                    () => _start + _itemCount < TotalItems ? true : false));
            }
        }
        #endregion

        #region LastCommand
        private RelayCommand _lastCommand;

        /// <summary>
        /// Gets the LastCommand.
        /// </summary>
        public RelayCommand LastCommand
        {
            get
            {
                return _lastCommand
                    ?? (_lastCommand = new RelayCommand(
                    () =>
                    {
                        // laatste pagina ipv starten bij totalcount
                        // als totalItems = 99 & itemCount = 10 => result = 80
                        // LET HIER OP OMDAT WE MET INT WERKEN
                        // 99 / 10 = 9 - 1 = 8 * 10 = 80 (= index) 
                        // Property Start toont 80 + 1
                        _start = (_totalItems / _itemCount - 1) * _itemCount;
                        // Controle als rest totalItems / itemCount niet 0 is (zie voorbeeld hierboven)
                        // Dan bekom je niet de laatste pagina, dan moet je de itemCount er nog eens bijtellen
                        _start += _totalItems % _itemCount == 0 ? 0 : _itemCount;

                        UpdateLijst();
                    },
                    () => _start + _itemCount < _totalItems ? true : false));
            }
        }
        #endregion

        #region SorteerDalendCommand
        private RelayCommand _sorteerDalendCommand;

        /// <summary>
        /// Gets the SorteerDalendCommand.
        /// </summary>
        public RelayCommand SorteerDalendCommand
        {
            get
            {
                return _sorteerDalendCommand
                    ?? (_sorteerDalendCommand = new RelayCommand(
                    () =>
                    {
                        _descending = true;
                        UpdateLijst();
                    }));
            }
        }
        #endregion

        #region SorteerStijgendCommand
        private RelayCommand _sorteerStijgendCommand;

        /// <summary>
        /// Gets the SorteerStijgendCommand.
        /// </summary>
        public RelayCommand SorteerStijgendCommand
        {
            get
            {
                return _sorteerStijgendCommand
                    ?? (_sorteerStijgendCommand = new RelayCommand(
                    () =>
                    {
                        _descending = false;
                        UpdateLijst();
                    }));
            }
        }
        #endregion

        #region ApplyFilter
        private RelayCommand _applyFilter;

        /// <summary>
        /// Gets the ApplyFilter.
        /// </summary>
        public RelayCommand ApplyFilter
        {
            get
            {
                return _applyFilter
                    ?? (_applyFilter = new RelayCommand(
                    () =>
                    {
                        UpdateLijst();
                    }));
            }
        }
        #endregion
        #endregion
        #endregion

        #region Methodes
        private void UpdateLijst(string message = "UpdateLijst")
        {
            //Is controle of deze komt van MessengerInstance (hier moest ik een string als parameter toevoegen)
            //Nu heb ik achteraf nog deze methode gebuikt binnen de klasse paging te gebruiken
            if (message == "UpdateLijst")
                LijstUitgaven = _dataservice.GetFilteredUitgavenPaged(_start, _itemCount, SorteerVeld, _descending, out _totalItems, Filter);

            RaisePropertyChanged("Start");
            RaisePropertyChanged("End");
            RaisePropertyChanged("TotalItems");
            #endregion
        }
    }
}
