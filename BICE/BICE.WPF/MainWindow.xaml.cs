
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

namespace BICE.WPF

{
    public partial class MainWindow : Window
    {
        string ApiUrl = "http://localhost:7001/api/";
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
            // Fetch data from the API
            var materials = await GetStoredMaterialsFromApi();

            // Write data to a CSV file
            WriteDataToCsv(materials);
        }

        private async Task<List<MaterialVehicle_DTO>> GetStoredMaterialsFromApi()
        {
            using HttpClient client = new HttpClient();
            var materials = await client.GetFromJsonAsync<List<MaterialVehicle_DTO>>(ApiUrl + "Material/materials-to-be-removed"); 

            return materials;
        }

        private void WriteDataToCsv(List<MaterialVehicle_DTO> materials)
        {
            // Define the file path where the CSV file will be saved
            string filePath = "C:\\Users\\Victor\\Desktop\\fichier.csv";

            // Use CsvWriter to write the data to a CSV file
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(materials);
            }
        }

}
}
