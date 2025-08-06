using System;
using System.Collections.Generic;
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
using DatabaseProject;
using System.Data.SqlClient;
using System.Configuration;

namespace hospitalsys
{
    /// <summary>
    /// Interaction logic for PatientFormWIndow.xaml
    /// </summary>
    public partial class PatientFormWIndow : Window
    {
        public PatientFormWIndow()
        {
            InitializeComponent();
        }

        private void SaveNew_Click(object sender, RoutedEventArgs e)
        {
            String name = PatientName.Text;
            String Phone = PatientNumber.Text;
            String Age = PatientAge.Text;
            String Address = PatientAddress.Text;
            String Nationality = PatientNationality.Text;
            String Gender = (PatientGender.SelectedItem as ComboBoxItem)?.Content?.ToString();
            String insurance = (PatientInsurance.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "None" ;

            if (String.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Pleas enter Patient name" ,"Warnning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                DBAccess db = new DBAccess();
                db.createConn();

                string insertQuery = "INSERT INTO patients (NAME, Phone, Age, Address, Nationality, Gender, insurance) VALUES (@Name, @phone, @Age, @Address, @Nationality, @Gender, @insurance)";
                SqlCommand cmd = new
                    SqlCommand(insertQuery);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Age", Age);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Nationality", Nationality);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@insurance", insurance);
                db.executeQuery(cmd);
                MessageBox.Show("Patient added ", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                db.closeConn();
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occur: " + ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PatientNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }
    }
}
