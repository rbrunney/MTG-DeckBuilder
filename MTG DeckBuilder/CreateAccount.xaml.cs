using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WPF;

namespace MTG_DeckBuilder
{
    public partial class CreateAccount : Page
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt;
            Hashtable ht = new Hashtable();
            string sql;
            
            ht.Add("@Username", txtUsername.Text);
            ht.Add("@Email", txtEmail.Text);

            sql = $"SELECT Username, Email FROM Users WHERE Username=@Username OR Email=@Email";
            dt = ExDB.GetDataTable("AwesomeDB", ht, sql);

            if (txtName.Text.Equals(""))
            {
                MessageBox.Show("Name must not be empty");
            } 
            else if(txtUsername.Text.Equals(""))
            {
                MessageBox.Show("Username must not be empty");
            } 
            else if (txtPassword.Text.Equals(""))
            {
                MessageBox.Show("Password must not be empty");
            } 
            else if(txtConfirmPassword.Text.Equals(""))
            {
                MessageBox.Show("Confirm Password must not be empty");
            }
            else if (txtEmail.Text.Equals(""))
            {
                MessageBox.Show("Email must not be empty");
            } else if (!txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                MessageBox.Show("Password and Confirm Password must be the same");
            }
            else if (dt.Rows.Count != 0)
            {
                MessageBox.Show("Email and/or Username already taken");
            }
            else
            {
                ht.Add("@Name", txtName.Text);
                ht.Add("@Password", BCrypt.Net.BCrypt.HashPassword(txtPassword.Text));
                sql = "INSERT INTO Users (Name, Username, Password, Email) VALUES(@Name, @Username, @Password, @Email)";
                ExDB.ExecuteIt("AwesomeDB", sql, ht);
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.frmMainFrame.Content = new Login();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.frmMainFrame.Content = new MainMenu();
        }
    }
}