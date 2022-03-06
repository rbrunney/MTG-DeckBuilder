using System.Windows;
using System.Windows.Controls;

namespace MTG_DeckBuilder
{
    public partial class DeckList : Page
    {
        public DeckList()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new Decks();
        }
    }
}