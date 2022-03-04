using Microsoft.ML.Data;

namespace TaxiFarePrediction.DataStructures
{
    public class TaxiTrip
    {

        [LoadColumn(2)]
        public float PassengerCount;

        [LoadColumn(3)]
        public float TripTime;

        [LoadColumn(4)]
        public float TripDistance;

        [LoadColumn(6)]
        public float FareAmount;
    }
}