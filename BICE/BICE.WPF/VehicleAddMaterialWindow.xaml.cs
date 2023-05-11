using BICE.DTO;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
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
using System.Windows.Shapes;
using BICE.WPF.ViewModels;
using System.IO;

namespace BICE.WPF
{
    /// <summary>
    /// Logique d'interaction pour VehicleAddMaterialWindow.xaml
    /// </summary>
    public partial class VehicleAddMaterialWindow : Window
    {

        private readonly VehicleViewModel _vehicleViewModel;
        private readonly Vehicle_DTO _vehicle;

        public VehicleAddMaterialWindow(VehicleViewModel vehicleViewModel, Vehicle_DTO vehicle)
        {
            InitializeComponent();

            _vehicleViewModel = vehicleViewModel;
            _vehicle = vehicle;
        }

        private void ImportCsvButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string csvPath = openFileDialog.FileName;
                var materials = ParseCsvToMaterialDto(csvPath);

                if (materials != null)
                {
                    AddMaterialsAsync(materials);
                }
                else
                {
                    MessageBox.Show("Erreur lors de la lecture du fichier CSV.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public List<string> ParseCsvToMaterialDto(string filePath)
        {
            List<string> barcodes = new List<string>();
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(';');

                string barcode = fields[0];

                barcodes.Add(barcode);
            }

            return barcodes;
        }

        private async void AddMaterialsAsync(List<string> barcodes)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://localhost:7001/api/Material/vehicle-preparation/{_vehicle.Id}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string materialsJson = JsonConvert.SerializeObject(barcodes);
            StringContent content = new StringContent(materialsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Les matériels ont été ajoutés avec succès à {_vehicle.Denomination}.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de l'ajout du matériel.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
