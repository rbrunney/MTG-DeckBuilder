using System;
using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WPF;

namespace MTG_DeckBuilder
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt;
            Hashtable ht = new Hashtable();
            string sql = "";
            
            ht.Add("@Username", txtUsername.Text);
            ht.Add("@Email", txtUsername.Text);

            sql = $"SELECT Username,Password FROM Users WHERE Username=@Username OR Email=@Email";
            dt = ExDB.GetDataTable("AwesomeDB", ht, sql);

            try
            {
                DataRow dr = dt.Rows[0];
                if (!txtUsername.Text.Equals(dr["Username"].ToString()) || !BCrypt.Net.BCrypt.Verify(txtPassword.Text, dr["Password"].ToString()))
                {
                    MessageBox.Show("Username and/or Password are incorrect");
                }
                else
                {
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow.frmMainFrame.Content = new Decks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Username and/or Password are incorrect");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new MainMenu();
        }
    }
}