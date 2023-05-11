using BICE.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private async void CreateInterventionButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les valeurs du formulaire
            string denomination = DenominationTextBox.Text;
            string description = DescriptionTextBox.Text;
            DateTime startDate = StartDatePicker.SelectedDate.Value;
            DateTime? endDate = EndDatePicker.SelectedDate;

            Intervention_DTO newIntervention;
            // Créer une nouvelle instance de Intervention_DTO
            if (endDate != null)
            {
                newIntervention = new Intervention_DTO
                {
                Denomination = denomination,
                Description = description,
                StartDate = startDate,
                EndDate = (DateTime)endDate
                };
            }
            else
            {
                newIntervention = new Intervention_DTO
                {
                    Denomination = denomination,
                    Description = description,
                    StartDate = startDate
                };
            }
            

            // Envoyer la nouvelle intervention à l'API
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ApiUrl + "/Intervention");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string interventionJson = JsonConvert.SerializeObject(newIntervention);
            StringContent content = new StringContent(interventionJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("L'intervention a été ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de l'ajout de l'intervention.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
