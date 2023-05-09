using BICE.DAL;
using BICE.DTO;
using BICE.SRV;
using BICE.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void AddVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            var addVehicleWindow = new VehicleAddWindow();
            addVehicleWindow.Show();
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

    }
}
