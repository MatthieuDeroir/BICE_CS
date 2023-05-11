using BICE.DTO;
using System;
using System.Collections.Generic;
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

namespace BICE.WPF
{
    /// <summary>
    /// Logique d'interaction pour InterventionAddVehiculesWindow.xaml
    /// </summary>
    public partial class InterventionAddVehiculesWindow : Window
    {
        private const string ApiUrl = "https://localhost:7001/api";
        private Intervention_DTO _intervention;
        public InterventionAddVehiculesWindow(Intervention_DTO Intervention)
        {
            InitializeComponent();
            _intervention = Intervention;

            LabelTitle.Content = $"Ajouter des véhicules à l'intervention {_intervention.Denomination}";

            LoadVehicles();

            LoadInterventionVehicles();


        }

        private async void LoadVehicles()
        {
            using HttpClient client = new HttpClient();
            var vehicles = await client.GetFromJsonAsync<List<Vehicle_DTO>>(ApiUrl + $"/Vehicle");

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

        private async void LoadInterventionVehicles()
        {
            using HttpClient client = new HttpClient();
            var vehicles = await client.GetFromJsonAsync<List<Vehicle_DTO>>(ApiUrl + $"/Intervention/{_intervention.Id}/vehicles");

            if (vehicles != null)
            {
                VehiclesDataGrid.ItemsSource = vehicles;
            }
            else
            {
                MessageBox.Show("Erreur lors de la récupération des véhicules de l'intervention depuis l'API.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public async void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if (VehiclesComboBox.SelectedValue != null)
            {
                using HttpClient client = new HttpClient();
                var vehicleId = VehiclesComboBox.SelectedValue.ToString();
                var response = await client.PostAsync(ApiUrl + $"/Intervention/{_intervention.Id}/vehicle/{vehicleId}", null);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Le véhicule a été ajouté à l'intervention avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadVehicles();
                    LoadInterventionVehicles();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout du véhicule à l'intervention.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un véhicule.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
