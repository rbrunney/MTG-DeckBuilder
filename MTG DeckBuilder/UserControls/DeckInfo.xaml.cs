using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WPF;

namespace MTG_DeckBuilder.UserControls
{
    public partial class DeckInfo : UserControl
    {
        public DeckInfo()
        {
            InitializeComponent();
        }

        public string DeckName
        {
            get { return txtDeckName.Text; }
            set { txtDeckName.Text = value; }
        }

        public string DeckFormat
        {
            get { return txtDeckFormat.Text; }
            set { txtDeckFormat.Text = value; }
        }

        private void btnRemoveDeck_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt;
            Hashtable ht = new Hashtable();
            string sql = "";
            
            ht.Add("@Name", DeckName);
            ht.Add("@UserID", App.currentUser.ID);
            sql = "SELECT DeckID FROM Decks WHERE Name = @Name AND UserID = @UserID";
            dt = ExDB.GetDataTable("AwesomeDB", ht, sql);
            
            ht.Clear();
            ht.Add("@DeckID", (int) dt.Rows[0]["DeckID"]);
            ht.Add("@UserID", App.currentUser.ID);
            sql = "DELETE FROM DeckList WHERE DeckID = @DeckID AND UserID = @UserID";
            ExDB.ExecuteIt("AwesomeDB", sql, ht);

            sql = "DELETE FROM Decks WHERE DeckID = @DeckID AND UserID = @UserID";
            ExDB.ExecuteIt("AwesomeDB", sql, ht);

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new Decks();
        }
    }
}