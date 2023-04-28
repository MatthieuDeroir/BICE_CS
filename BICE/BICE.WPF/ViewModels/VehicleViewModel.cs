using BICE.DTO;
using BICE.SRV;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BICE.WPF.ViewModels
{
    internal class VehicleViewModel
    {
        private readonly Vehicle_SRV _vehicleService;

        public ObservableCollection<Vehicle_DTO> Vehicles { get; set; }

        public VehicleViewModel()
        {
            _vehicleService = new Vehicle_SRV();
            LoadVehicles();
        }

        private void LoadVehicles()
        {
            Vehicles = new ObservableCollection<Vehicle_DTO>(_vehicleService.GetVehicle());
        }
    }
}
