using MauiApp_Onnx.Models;
using MauiApp_Onnx.Services;
using System.Windows.Input;

namespace MauiApp_Onnx.ViewModels
{
    public class PredictViewModel : BaseViewModel
    {

        public PredictionService PredictionService { get; }
        private float passengerCount;

        public float PassengerCount
        {
            get { return passengerCount; }
            set { SetProperty(ref passengerCount, value); }
        }

        private float tripTime;

        public float TripTime
        {
            get { return tripTime; }
            set { SetProperty(ref tripTime, value); }
        }


        private float tripDistance;

        public float TripDistance
        {
            get { return tripDistance; }
            set { SetProperty(ref tripDistance, value); }
        }

        private float fareAmount;

        public float FareAmount
        {
            get { return fareAmount; }
            set { SetProperty(ref fareAmount, value); }
        }


        private TaxiTrip trip;

        public TaxiTrip Trip
        {
            get { return trip; }
            set { SetProperty(ref trip, value); }
        }

        public ICommand PredictCommand { private set; get; }

        private async Task Predict()
        {
            Trip = new TaxiTrip()
            {
                FareAmount = FareAmount,
                PassengerCount = PassengerCount,
                TripDistance = TripDistance,
                TripTime = TripTime,
            };

            var amount = PredictionService.Predict(Trip);
            await Application.Current.MainPage.DisplayAlert("Prediction", $"Trip Fare: {amount:C2}", "OK");
        }

        public PredictViewModel()
        {
            FareAmount = 0;
            PassengerCount = 1;
            TripDistance = 3.75f;
            TripTime = 1140;
            PredictionService = new PredictionService();
            PredictCommand = new Command(async () => await Predict());
        }
    }
}