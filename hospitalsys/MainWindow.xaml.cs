using System;
using System.Windows;
using System.Windows.Navigation;
using System.Data;
using DatabaseProject;
using System.Data.SqlClient;
namespace hospitalsys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string id, name, Password, Role;
        DBAccess objDBAccess = new DBAccess();
        DataTable dthospitalUsers = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string Name = UsernameBox.Text;
            string password = PasswordBox.Password;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your Username and Password");
                return;
            }
            else 
            {
            string query = "SELECT * FROM hospitalUsers WHERE Name = '" + Name + "'AND password = '" + password + "'";
                objDBAccess.readDatathroughAdapter(query, dthospitalUsers);
                if(dthospitalUsers.Rows.Count > 0)
                {
                    MessageBox.Show("welcome " + @Name);
                    this.Hide();
                    HomePage homePage = new HomePage();
                    homePage.Show();

                }
                else
                {
                    MessageBox.Show("Invalid Username or Password");
                }

            }
        }
    }
}
