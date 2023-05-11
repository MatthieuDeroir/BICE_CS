using BICE.DTO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
using static System.Net.Mime.MediaTypeNames;

namespace BICE.WPF
{
    /// <summary>
    /// Logique d'interaction pour InterventionReturnWindow.xaml
    /// </summary>
    public partial class InterventionReturnWindow : Window
    {
        private const string ApiUrl = "https://localhost:7001/api";
        Intervention_DTO _intervention;
        List<string> _usedBarcodes;
        List<string> _unusedBarcodes;
        public InterventionReturnWindow(Intervention_DTO selectedIntervention)
        {
            InitializeComponent();

            _intervention = selectedIntervention;

            LabelTitle.Content = $"Intervention {_intervention.Denomination}";
            VehiclesComboBox.Text = "Sélectionnez un véhicule de l'intervention";

            LoadVehicles();
        }

        private async void LoadVehicles()
        {
            using HttpClient client = new HttpClient();
            var vehicles = await client.GetFromJsonAsync<List<Vehicle_DTO>>(ApiUrl + $"/Vehicle/{_intervention.Id}/vehicles");

            if (vehicles != null)
            {
                VehiclesComboBox.ItemsSource = vehicles;
                VehiclesComboBox.DisplayMemberPath = "Denomination"; 
                VehiclesComboBox.SelectedValuePath = "Id";  
            }
            else
            {
                MessageBox.Show("Erreur lors de la récupération des véhicules depuis l'API.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ImportCsvButton_Click1(object sender, RoutedEventArgs e)
        {
            string filePath = OpenCsvFileDialog();
            if (!string.IsNullOrEmpty(filePath))
            {
                LabelFile1.Content = filePath;
                _usedBarcodes = ReadCsvFile(filePath);
            }
        }

        private void ImportCsvButton_Click2(object sender, RoutedEventArgs e)
        {
            string filePath = OpenCsvFileDialog();
            if (!string.IsNullOrEmpty(filePath))
            {
                LabelFile2.Content = filePath;
                _unusedBarcodes = ReadCsvFile(filePath);
            }
        }

        private string OpenCsvFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        private List<string> ReadCsvFile(string filePath)
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            SendInterventionReturn();
        }

        public async Task SendInterventionReturn()
        {
            //TODO Uri = 
        }

    }
}
