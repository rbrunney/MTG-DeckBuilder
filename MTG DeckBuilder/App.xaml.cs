using System.Collections.Generic;
using System.Windows.Input;

namespace MTG_DeckBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static CurrentUser currentUser = new CurrentUser();
        public static CurrentDeck currentDeck = new CurrentDeck();

        public class CurrentUser
        {
            private int id;
            private string username;
            private string password;

            public int ID
            {
                get { return id; }
                set { id = value; }
            }
            public string Username
            {
                get { return username; }
                set { username = value; }
            }

            public string Password
            {
                get { return password; }
                set { password = value; }
            }
        }

        public class CurrentDeck
        {
            private int id;
            private string name;
            public List<string> cardLinks = new List<string>();

            public int ID
            {
                get { return id; }
                set { id = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }
    }
}