using BICE.DTO;
using BICE.SRV;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BICE.WPF.ViewModels
{
    public class VehicleViewModel
    {
        private readonly Vehicle_SRV _vehicleService;

        public ObservableCollection<Vehicle_DTO> Vehicles { get; set; }

        public VehicleViewModel()
        {
            _vehicleService = new Vehicle_SRV();
            LoadVehicles();
        }

        public async Task LoadVehicles()
        {
            //Vehicles = new ObservableCollection<Vehicle_DTO>(_vehicleService.GetVehicle());
            var allVehicles = _vehicleService.GetVehicle();
            var activeVehicles = allVehicles.Where(v => v.IsActive);
            Vehicles = new ObservableCollection<Vehicle_DTO>(activeVehicles);
        }

        public async Task EnableDisableVehicle(Vehicle_DTO vehicle)
        {
            // Envoyez une requête à l'API pour desac le véhicule
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7001/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string vehicleJson = JsonConvert.SerializeObject(vehicle);
            StringContent content = new StringContent(vehicleJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            if (vehicle.IsActive)
            {
                response = await client.PutAsync($"api/Vehicle/disable/{vehicle.Id}", content);
            }
            else
            {
                response = await client.PutAsync($"api/Vehicle/enable/{vehicle.Id}", content);
            }

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Le véhicule a été désactivé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de la suppression du véhicule.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Refresh   
            await LoadVehicles();
        }

        public async Task UpdateVehicle(Vehicle_DTO vehicle)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7001/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string vehicleJson = JsonConvert.SerializeObject(vehicle);
            StringContent content = new StringContent(vehicleJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"api/Vehicle", content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Le véhicule a été mis à jour avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de la mise à jour du véhicule.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*
        public async Task UpdateVehicleAsync(Vehicle_DTO vehicle)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7001/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsonString = JsonConvert.SerializeObject(vehicle);
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"api/Vehicle", content);
                if (response.IsSuccessStatusCode)
                {
                    // Recharger la liste des véhicules pour refléter les modifications
                    await LoadVehicles();
                }
                else
                {
                    // Gérer l'échec de la requête
                }
            }
        }*/


    }
}
