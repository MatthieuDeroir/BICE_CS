using BICE.DAL;
using BICE.DTO;
using BICE.SRV;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
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
    /// Logique d'interaction pour VehicleAddWindow.xaml
    /// </summary>
    public partial class VehicleAddWindow : Window
    {
        public VehicleAddWindow()
        {
            InitializeComponent();
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



    }
}
