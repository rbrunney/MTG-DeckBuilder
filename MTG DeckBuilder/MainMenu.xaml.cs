using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MTG_DeckBuilder
{
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            // lblTitle.FontFamily = new FontFamily(new Uri("pack//application:,,,/"), "./Fonts/GoudyMedivaeval-Regular");
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new Login();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new CreateAccount();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}