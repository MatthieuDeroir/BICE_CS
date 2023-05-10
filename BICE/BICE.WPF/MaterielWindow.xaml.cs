using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Windows;
using BICE.DTO;
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
            var materials = await client.GetFromJsonAsync<List<Material_DTO>>(ApiUrl+"/Material");

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
                var materials = ReadMaterialsFromCsv(csvPath);

                if (materials != null)
                {
                    foreach (var material in materials)
                    {
                        AddMaterialAsync(material);
                    }

                    MessageBox.Show("Les matériels ont été ajoutés avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Erreur lors de la lecture du fichier CSV.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private List<Material_DTO> ReadMaterialsFromCsv(string csvPath)
        {
            try
            {
                var lines = File.ReadAllLines(csvPath);
                var materials = new List<Material_DTO>();

                foreach (string line in lines)
                {
                    var values = line.Split(',');   

                    var material = new Material_DTO
                    {
                        Barcode = values[0],
                        Denomination = values[1],
                        Category = values[2],
                        UsageCount = int.Parse(values[3]),
                        MaxUsageCount = int.Parse(values[4]),
                        ExpirationDate = DateTime.Parse(values[5]),
                        NextControlDate = DateTime.Parse(values[6])
                    };

                    materials.Add(material);
                }

                return materials;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async void AddMaterialAsync(Material_DTO material)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ApiUrl+"");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string materialJson = JsonConvert.SerializeObject(material);
            StringContent content = new StringContent(materialJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Mettre à jour la liste des matériels (si nécessaire)
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de l'ajout du matériel.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
