using BICE.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour InterventionWindos.xaml
    /// </summary>
    public partial class InterventionWindos : Window
    {
        private const string ApiUrl = "https://localhost:7001/api";
        private ObservableCollection<Intervention_DTO> _interventions;
        public InterventionWindos()
        {
            InitializeComponent();
            LoadInterventions();
        }

        private async void LoadInterventions()
        {
            using HttpClient client = new HttpClient();
            var interventions = await client.GetFromJsonAsync<List<Intervention_DTO>>(ApiUrl + "/Intervention");

            if (interventions != null)
            {
                _interventions = new ObservableCollection<Intervention_DTO>(interventions);
                InterventionGrid.ItemsSource = _interventions;
            }
            else
            {
                MessageBox.Show("Erreur lors de la récupération des interventions depuis l'API.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
