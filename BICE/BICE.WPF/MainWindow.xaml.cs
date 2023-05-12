
﻿using BICE.DAL;
using BICE.DTO;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Net.Http.Json;
using CsvHelper.Configuration;

namespace BICE.WPF

{
    public partial class MainWindow : Window
    {
        string ApiUrl = "https://localhost:7001/api/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Vehicle(object sender, RoutedEventArgs e)
        {
            VehicleWindow vehicleWindow = new VehicleWindow();
            vehicleWindow.Show();
        }
        private void Button_Click_Material(object sender, RoutedEventArgs e)
        {
            MaterialWindow materialWindow = new MaterialWindow();
            materialWindow.Show();
        }
        private void Button_Click_Intervention(object sender, RoutedEventArgs e)
        {
            InterventionWindos interventionWindos = new InterventionWindos();
            interventionWindos.Show();
        }
    }
}
