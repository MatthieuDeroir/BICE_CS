using BICE.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace BICE.WPF
{
    /// <summary>
    /// Interaction logic for VehicleMaterialWindow.xaml
    /// </summary>
    public partial class VehicleMaterialWindow : Window
    {
        private readonly Vehicle_DTO _vehicle;
        private const string ApiUrl = "https://localhost:7001/api";

        public VehicleMaterialWindow(Vehicle_DTO vehicle)
        {
            InitializeComponent();

            _vehicle = vehicle;

            LoadMaterials();
            VehicleLabel.Content = $"Liste des matériels pour le véhicule {_vehicle.Denomination} ({_vehicle.InternalNumber})";
        }

        private async void LoadMaterials()
        {
            using HttpClient client = new HttpClient();
            var materials = await client.GetFromJsonAsync<List<Material_DTO>>(ApiUrl + $"/Material/vehicle/{_vehicle.Id}");

            if (materials != null)
            {
                VehicleMaterialGrid.ItemsSource = materials;
            }
            else
            {
                MessageBox.Show("Erreur lors de la récupération des matériels depuis l'API.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
