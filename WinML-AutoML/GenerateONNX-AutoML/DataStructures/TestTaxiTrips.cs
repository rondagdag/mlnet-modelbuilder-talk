namespace TaxiFarePrediction.DataStructures
{
    internal class SingleTaxiTripSample
    {
        internal static readonly TaxiTrip Trip1 = new TaxiTrip
        {
            PassengerCount = 1,
            TripTime = 1140f,
            TripDistance = 10.33f,
            FareAmount = 0 // predict it. actual = 29.5
        };
    }
}