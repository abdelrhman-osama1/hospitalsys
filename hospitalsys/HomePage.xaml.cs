using DatabaseProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Configuration;
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

        private void SearchPatients ()
        {
            DBAccess db = new DBAccess();
            DataTable dt = new DataTable();
            List<string> filters = new List<string>();
            SqlCommand cmd = new SqlCommand();

            if(!string.IsNullOrWhiteSpace(IDSearch.Text))
            {
                filters.Add("ID LIKE @ID");
                cmd.Parameters.AddWithValue("@ID", "%" + IDSearch.Text + "%");
            }

            if (!string.IsNullOrWhiteSpace(NameSearch.Text))
            {
                filters.Add("Name LIKE @Name");
                cmd.Parameters.AddWithValue("@Name", "%" + NameSearch.Text + "%");
            }

            if (!string.IsNullOrWhiteSpace(PhoneSearch.Text))
            {
                filters.Add("Phone LIKE @Phone");
                cmd.Parameters.AddWithValue("@Phone", "%" + PhoneSearch.Text + "%");
            }

            string query = "SELECT ID,  Name, Phone, Age, Address, Nationality, Gender, Insurance FROM Patients";
            if (filters.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", filters);
            }

            cmd.CommandText = query;
            db.readDatathroughAdapter(cmd, dt);
            PatientData.ItemsSource = dt.DefaultView;
        }

        private void IDSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchPatients();
                
            }

        }

        private void NameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchPatients();

            }
        }

        private void PhoneSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchPatients();

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientData.SelectedItem == null)
            {
                MessageBox.Show("Please select a patient to delete", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DataRowView row = (DataRowView)PatientData.SelectedItem;
            string patientId = row["ID"].ToString();
            MessageBoxResult result = MessageBox.Show("Are you Sure You want To Delet this Patient?", "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                try
                {
                    DBAccess db = new DBAccess();
                    db.createConn();

                    SqlCommand cmd = new SqlCommand("DELETE FROM Patients WHERE ID = @ID");
                    cmd.Parameters.AddWithValue("@ID", patientId);
                    db.executeQuery(cmd);
                    db.closeConn();
                    MessageBox.Show("Patient deleted successfully.", "Deleted", MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    SearchPatients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting Patient: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }    
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PatientData.SelectedItem == null)
            {
                MessageBox.Show("Please select a patient to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
            }
            DataRowView row = (DataRowView)PatientData.SelectedItem;
            PatientFormWIndow editForm = new
            PatientFormWIndow(row);
            editForm.ShowDialog();
            SearchPatients();
        }
    }
}
