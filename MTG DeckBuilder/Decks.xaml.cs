using System.Windows;
using System.Windows.Controls;

namespace MTG_DeckBuilder
{
    public partial class Decks : Page
    {
        public Decks()
        {
            InitializeComponent();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new MainMenu();
        }

        private void btnDeckList_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new DeckList();
        }
    }
}