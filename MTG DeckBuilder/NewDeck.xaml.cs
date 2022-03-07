using System.Collections;
using System.Data;
using System.Runtime.Remoting.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF;

namespace MTG_DeckBuilder
{
    public partial class NewDeck : Page
    {
        public NewDeck()
        {
            InitializeComponent();
        }

        private void btnCreateDeck_Click(object sender, RoutedEventArgs e)
        {
            if (txtDeckName.Text.Equals(""))
            {
                MessageBox.Show("Name cannot be left blank");
            } else if (cBoxFormat.SelectionBoxItem.ToString().Equals(""))
            {
                MessageBox.Show("Format cannot be left blank");
            }
            else
            {
                DataTable dt;
                Hashtable ht = new Hashtable();
                string sql = "";
            
                ht.Add("@Name", txtDeckName.Text);
                ht.Add("@UserID", App.currentUser.ID);

                sql = $"SELECT Name FROM Decks WHERE Name=@Name AND UserID = @UserID";
                dt = ExDB.GetDataTable("AwesomeDB", ht, sql);

                if (dt.Rows.Count != 0)
                {
                    MessageBox.Show("You already have a deck with this name!");
                }
                else
                {
                    ht.Clear();
                    ht.Add("@Name", txtDeckName.Text);
                    ht.Add("@Format", cBoxFormat.SelectionBoxItem.ToString());
                    ht.Add("@UserID", App.currentUser.ID);

                    sql = "INSERT INTO Decks (Name, Format, UserID) VALUES(@Name, @Format, @UserID)";
                    ExDB.GetDataTable("AwesomeDB", ht, sql);

                    MessageBox.Show("Deck Created");
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow.frmMainFrame.Content = new Decks();
                }
            }
        }
    }
}