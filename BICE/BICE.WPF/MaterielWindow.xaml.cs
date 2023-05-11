using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BICE.DTO;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using Newtonsoft.Json;


namespace BICE.WPF
{
    public partial class MaterialWindow : Window
    {
        private const string ApiUrl = "https://localhost:7001/api";
        private ObservableCollection<Material_DTO> _materials;

        public MaterialWindow()
        {
            InitializeComponent();
            LoadMaterials();
        }

        private async void LoadMaterials()
        {
            using HttpClient client = new HttpClient();
            var materials = await client.GetFromJsonAsync<List<Material_DTO>>(ApiUrl + "/Material");

            if (materials != null)
            {
                _materials = new ObservableCollection<Material_DTO>(materials);
                MaterialGrid.ItemsSource = _materials;
            }
            else
            {
                MessageBox.Show("Erreur lors de la récupération des matériels depuis l'API.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        public List<Material_DTO> ParseCsvToMaterialDto(string filePath)
        {
            List<Material_DTO> materials = new List<Material_DTO>();
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(';');

                string barcode = fields[0];
                string denomination = fields[1];
                string category = fields[2];
                int usageCount = int.Parse(fields[3]);
                int? maxUsageCount = string.IsNullOrEmpty(fields[4]) ? (int?)null : int.Parse(fields[4]);
                DateTime? expirationDate = string.IsNullOrEmpty(fields[5]) ? (DateTime?)null : DateTime.ParseExact(fields[5], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime? nextControlDate = string.IsNullOrEmpty(fields[6]) ? (DateTime?)null : DateTime.ParseExact(fields[6], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                Material_DTO materialDto = new Material_DTO(barcode, denomination, category, usageCount, maxUsageCount, expirationDate, nextControlDate);
                materials.Add(materialDto);
            }

            return materials;
        }

        private async void AddMaterialsAsync(List<Material_DTO> materials)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7001/api/Material/insert-list");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string materialsJson = JsonConvert.SerializeObject(materials);
            StringContent content = new StringContent(materialsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Les matériels ont été ajoutés avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de l'ajout du matériel.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
