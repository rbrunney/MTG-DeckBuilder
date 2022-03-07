using System;
using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPF;

namespace MTG_DeckBuilder
{
    public partial class DeckList : Page
    {
        public DeckList()
        {
            InitializeComponent();
            lblDeckName.Content = App.currentDeck.Name;


            DataTable dt;
            Hashtable ht = new Hashtable();
            string sql = "";
            
            ht.Add("@Name", App.currentDeck.Name);
            sql = "SELECT DeckID FROM Decks WHERE Name = @Name";
            dt = ExDB.GetDataTable("AwesomeDB", ht, sql);

            ht.Clear();
            ht.Add("@DeckID", dt.Rows[0]["DeckID"]);
            sql = $"SELECT CardLink FROM DeckList WHERE DeckID = @DeckID";
            dt = ExDB.GetDataTable("AwesomeDB", ht, sql);

            int cardColumnTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cardList.ColumnDefinitions.Count < 4)
                {
                    cardList.ColumnDefinitions.Add(new ColumnDefinition());
                }

                FindImages((string) dt.Rows[i]["CardLink"], cardColumnTotal);
                if (cardColumnTotal == 3)
                {
                    cardColumnTotal = -1;
                }

                cardColumnTotal++;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new Decks();
        }

        private void btnFindCards_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new Search();
        }
        private void FindImages(string content, int nodeCount)
        {
            string innerHtml = content.Trim();
            string srcLink = innerHtml.Substring(innerHtml.IndexOf("https"));
            string imgLink = srcLink.Substring(0, srcLink.Length - 2);

            //Creating Card
            Image imgCard = new Image()
            {
                Height = 275,
                Width = 200,
                Source = new BitmapImage(new Uri(imgLink)),
                Margin = new Thickness(2),
            };
            
            imgCard.SetValue(Grid.ColumnProperty, nodeCount);
            imgCard.SetValue(Grid.RowProperty, cardList.RowDefinitions.Count);

            if (nodeCount == 3)
            {
                cardList.RowDefinitions.Add(new RowDefinition());
            }
            
            cardList.Children.Add(imgCard);
        }
        
    }
}