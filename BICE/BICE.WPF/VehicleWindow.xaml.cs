using BICE.DAL;
using BICE.DTO;
using BICE.SRV;
using BICE.WPF.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace BICE.WPF
{
    /// <summary>
    /// Logique d'interaction pour VehicleWindow.xaml
    /// </summary>
    public partial class VehicleWindow : Window
    {
        public VehicleWindow()
        {
            InitializeComponent();

            // Créez une nouvelle instance du modèle de vue et définissez-la comme contexte de données
            DataContext = new VehicleViewModel();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var vehicle = button.DataContext as Vehicle_DTO;

            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer ce véhicule ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                await (DataContext as VehicleViewModel).DeleteVehicle(vehicle);
                (DataContext as VehicleViewModel).Vehicles.Remove(vehicle);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier si un véhicule est sélectionné
            if (VehicleGrid.SelectedItem != null)
            {
                Vehicle_DTO selectedVehicle = VehicleGrid.SelectedItem as Vehicle_DTO;

                // Créer une nouvelle instance de la fenêtre d'édition de véhicule
                VehicleEditWindow editWindow = new VehicleEditWindow(DataContext as VehicleViewModel, selectedVehicle);

                // Afficher la fenêtre d'édition de véhicule
                editWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un véhicule à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void CreateVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les données saisies dans les champs du formulaire
            string internalNumber = InternalNumberTextBox.Text;
            string denomination = DenominationTextBox.Text;
            string licensePlate = LicensePlateTextBox.Text;
            bool isActive = IsActiveCheckBox.IsChecked ?? false;

            // Créer un nouveau véhicule
            Vehicle_DTO newVehicle = new Vehicle_DTO
            {
                InternalNumber = internalNumber,
                Denomination = denomination,
                LicensePlate = licensePlate,
                IsActive = isActive
            };

            // // Afficher un message de retour
            //MessageBox.Show($"Création d'un nouveau véhicule : InternalNumber = {internalNumber}, Denomination = {denomination}, LicensePlate = {licensePlate}, IsActive = {isActive}");

            // Convertir l'objet en JSON
            string json = JsonConvert.SerializeObject(newVehicle);

            // Appel de la méthode pour créer le véhicule dans la base de données
            //Vehicle_SRV vehicleService = new Vehicle_SRV();
            //Vehicle_DTO createdVehicle = vehicleService.AddVehicle(newVehicle);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7001/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync("api/vehicle", new StringContent(json, Encoding.UTF8, "application/json"));


            // Condition de marche de la requête
            if (response.IsSuccessStatusCode)
            {
                // Affichage d'un message de confirmation
                MessageBox.Show("Le véhicule a été créé avec succès.");

                // Fermeture de la fenêtre
                Close();
            }
            else
            {
                // Affichage d'un message d'erreur
                MessageBox.Show("Une erreur est survenue lors de la création du véhicule.");
            }

            // Vider les champs du formulaire
            InternalNumberTextBox.Text = "";
            DenominationTextBox.Text = "";
            LicensePlateTextBox.Text = "";
            IsActiveCheckBox.IsChecked = false;
        }

        private void AddMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier si un véhicule est sélectionné
            if (VehicleGrid.SelectedItem != null)
            {
                Vehicle_DTO selectedVehicle = VehicleGrid.SelectedItem as Vehicle_DTO;

                // Créer une nouvelle instance de la fenêtre d'édition de véhicule
                VehicleAddMaterialWindow editWindow = new VehicleAddMaterialWindow(DataContext as VehicleViewModel, selectedVehicle);

                // Afficher la fenêtre d'édition de véhicule
                editWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un véhicule à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DisplayMaterialButton_Click(Object sender, RoutedEventArgs e)
        {
            // Vérifier si un véhicule est sélectionné
            if (VehicleGrid.SelectedItem != null)
            {
                Vehicle_DTO selectedVehicle = VehicleGrid.SelectedItem as Vehicle_DTO;

                // Créer une nouvelle instance de la fenêtre d'édition de véhicule
                VehicleMaterialWindow vehicleMaterialWindow = new VehicleMaterialWindow(selectedVehicle);

                // Afficher la fenêtre 
                vehicleMaterialWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un véhicule.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
