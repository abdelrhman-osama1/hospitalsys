using DatabaseProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace hospitalsys
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private void LoadPatients()
        {
            DBAccess db = new DBAccess();
            DataTable dt = new DataTable();
            String query = "SELECT ID, Name, Phone, Age, Address, Nationality, Gender, Insurance FROM Patients";
            db.readDatathroughAdapter(query, dt);
            PatientData.ItemsSource = dt.DefaultView;
        }
        public HomePage()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientFormWIndow wIndow = new
                PatientFormWIndow();
            wIndow.ShowDialog();
            LoadPatients();
        }
    }
}
