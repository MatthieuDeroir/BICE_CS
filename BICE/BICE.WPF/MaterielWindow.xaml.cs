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

        public List<Material_DTO> ParseCsvToMaterialDto(string filePath)
        {
            List<Material_DTO> materials = new List<Material_DTO>();
            string[] lines = File.ReadAllLines(filePath);

            // Skip the header if the CSV file has one

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

        public async void FichierDropStackPanel_CSVReader_JsonConverter(object sender, DragEventArgs e)
        {
            var liste_DTO = new List<Material_DTO>();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string path = Path.GetFullPath(files[0]);
                string filename = Path.GetFileName(files[0]);
                NomFichierLabel.Content = filename;

                try
                {
                    liste_DTO = ParseCsvToMaterialDto(path);

                    await envoyer_e(liste_DTO);
                }
                catch (Exception ex)
                {
                    // Handle exceptions here, e.g. show a message to the user
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public async Task envoyer_e(List<Material_DTO> liste_DTO)
        {
            try
            {
                string json = JsonConvert.SerializeObject(liste_DTO);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7001/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync("api/Material/insert-list", new StringContent(json, Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
