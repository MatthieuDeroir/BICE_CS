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
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BICE.WPF.ViewModels
{
    internal class VehicleViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Vehicle_DTO> _vehicles;
        public ObservableCollection<Vehicle_DTO> Vehicles
        {
            get => _vehicles;
            set
            {
                _vehicles = value;
                OnPropertyChanged();
            }
        }

        public VehicleViewModel()
        {
            LoadVehicles();
        }

        private async void LoadVehicles()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7001/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/vehicle");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                List<Vehicle_DTO> vehicles = JsonConvert.DeserializeObject<List<Vehicle_DTO>>(responseContent);
                Vehicles = new ObservableCollection<Vehicle_DTO>(vehicles);
            }
            else
            {
                
            }
        }

        public async Task DeleteVehicleFromApi(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7001/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.DeleteAsync($"api/Vehicle/{id}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Le véhicule a été supprimé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de la suppression du véhicule.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
