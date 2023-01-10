using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.AI.MachineLearning;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var shape = new long[] { 1, 1 };
            var modelInput = new taxiFarePredInput()
            {
                PassengerCount = TensorFloat.CreateFromArray(shape, new float[] { float.Parse(PassengerCount.Text) }),
                TripTime = TensorFloat.CreateFromArray(shape, new float[] { float.Parse(TripTime.Text) }),
                TripDistance = TensorFloat.CreateFromArray(shape, new float[] { float.Parse(TripDistance.Text) }),
                FareAmount = TensorFloat.CreateFromArray(shape, new float[] { 0f }),
            };            
            var modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/taxiFarePred.onnx"));
            var session = await taxiFarePredModel.CreateFromStreamAsync(modelFile);

            var modelOutput = await session.EvaluateAsync(modelInput);
            var score = modelOutput.Score0.GetAsVectorView();

            PredictedFare.Text= score[0].ToString();

        }
    }
}
