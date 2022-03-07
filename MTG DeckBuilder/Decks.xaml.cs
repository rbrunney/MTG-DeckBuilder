using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MTG_DeckBuilder.UserControls;
using WPF;

namespace MTG_DeckBuilder
{
    public partial class Decks : Page
    {
        public Decks()
        {
            InitializeComponent();
            decks.RowDefinitions.Clear();
            DataTable dt;
            Hashtable ht = new Hashtable();
            string sql = "";
            
            ht.Add("@UserID", App.currentUser.ID);

            sql = $"SELECT DeckID, Name, Format FROM Decks WHERE UserID=@UserID";
            dt = ExDB.GetDataTable("AwesomeDB", ht, sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decks.RowDefinitions.Add(new RowDefinition());
                DeckInfo deckInfo = new DeckInfo();
                deckInfo.Height = 45;
                deckInfo.Width = 450;
                deckInfo.Margin = new Thickness(0, 5, 0,5);
                // deckInfo.Cursor = Cursors.Hand;
                deckInfo.DeckName = (string) dt.Rows[i]["Name"];
                deckInfo.DeckFormat = (string) dt.Rows[i]["Format"];
                deckInfo.SetValue(Grid.RowProperty, i);
                deckInfo.MouseLeftButtonDown += (s, e) =>
                {
                    App.currentDeck.Name = ((DeckInfo) s).DeckName;
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow.frmMainFrame.Content = new DeckList();
                };

                decks.Children.Add(deckInfo);
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new MainMenu();
        }

        private void btnDeckList_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new NewDeck();
        }
    }
}