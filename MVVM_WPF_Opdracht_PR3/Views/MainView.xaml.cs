using System.Windows;

namespace MVVM_WPF_Opdracht_PR3.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Hier Opteer ik voor Code Behind omdat deze acties(events) window based zijn
        ///     ViewModel heeft hier niets mee te maken
        ///     Bovendien om dit in het Viewmodel te implementeren heb je veel meer lijnen code nodig!!
        /// </summary>


        private void btnMinimaliseren_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximaliseren_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomeViewUC());
        }
    }
}
