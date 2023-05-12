
﻿using BICE.DAL;
using BICE.DTO;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Net.Http.Json;
using CsvHelper.Configuration;

namespace BICE.WPF

{
    public partial class MainWindow : Window
    {
        string ApiUrl = "https://localhost:7001/api/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Vehicle(object sender, RoutedEventArgs e)
        {
            VehicleWindow vehicleWindow = new VehicleWindow();
            vehicleWindow.Show();
        }
        private void Button_Click_Material(object sender, RoutedEventArgs e)
        {
            MaterialWindow materialWindow = new MaterialWindow();
            materialWindow.Show();
        }
        private void Button_Click_Intervention(object sender, RoutedEventArgs e)
        {
            InterventionWindos interventionWindos = new InterventionWindos();
            interventionWindos.Show();
        }



        public async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch data from the API
                var materials = await GetStoredMaterialsFromApi();

                // Write data to a CSV file
                WriteDataToCsv(materials);

                // Show a success message
                MessageBox.Show("Le fichier a été téléchargé avec succès", "Téléchargement réussi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Show an error message
                MessageBox.Show($"Une erreur s'est produite lors du téléchargement du fichier : {ex.Message}", "Erreur de téléchargement", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<IEnumerable<Material_DTO>> GetStoredMaterialsFromApi()
        {
            using HttpClient client = new HttpClient();
            var materials = await client.GetFromJsonAsync<IEnumerable<Material_DTO>>(ApiUrl + "Material/materials-to-be-removed");

            return materials;
        }

        private void WriteDataToCsv(IEnumerable<Material_DTO> materials)
        {
            // Define the file path where the CSV file will be saved
            string filePath = $"C:\\Users\\Victor\\Desktop\\BICE\\{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-Materiel_a_enlever.csv";

            // Use CsvWriter to write the data to a CSV file
            using (var writer = new StreamWriter(filePath))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";", // Use semicolon as delimiter
                };
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(materials);
                }
            }
        }



    }
}
