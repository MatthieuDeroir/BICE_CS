using BICE.DTO;
using BICE.WPF.ViewModels;
using System.Windows;

namespace BICE.WPF
{
    public partial class VehicleEditWindow : Window
    {
        private readonly VehicleViewModel _vehicleViewModel;
        private readonly Vehicle_DTO _vehicle;

        public VehicleEditWindow(VehicleViewModel vehicleViewModel, Vehicle_DTO vehicle)
        {
            InitializeComponent();

            _vehicleViewModel = vehicleViewModel;
            _vehicle = vehicle;

            // Remplir les champs du formulaire avec les données du véhicule
            InternalNumberTextBox.Text = _vehicle.InternalNumber;
            DenominationTextBox.Text = _vehicle.Denomination;
            LicensePlateTextBox.Text = _vehicle.LicensePlate;
            IsActiveCheckBox.IsChecked = _vehicle.IsActive;
        }

        private async void UpdateVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            // Mettre à jour les données du véhicule
            _vehicle.InternalNumber = InternalNumberTextBox.Text;
            _vehicle.Denomination = DenominationTextBox.Text;
            _vehicle.LicensePlate = LicensePlateTextBox.Text;
            _vehicle.IsActive = IsActiveCheckBox.IsChecked.Value;

            // Appeler la méthode de mise à jour du véhicule
            await _vehicleViewModel.UpdateVehicleAsync(_vehicle);

            // Fermer la fenêtre
            Close();
        }
    }
}
