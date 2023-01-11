// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUI_Onnx.Models;
using WinUI_Onnx.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_Onnx
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        readonly PredictionService predictionService = new();

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var trip = new TaxiTrip()
            {
                PassengerCount = float.Parse(PassengerCount.Text),
                TripDistance = float.Parse(TripDistance.Text),
                TripTime = float.Parse(TripTime.Text),
            };

            var amount = predictionService.Predict(trip);
            PredictedFare.Text = amount.ToString();
        }

        private async void RelativePanel_Loading(FrameworkElement sender, object args)
        {
            await predictionService.Loader();
        }
    }
}
