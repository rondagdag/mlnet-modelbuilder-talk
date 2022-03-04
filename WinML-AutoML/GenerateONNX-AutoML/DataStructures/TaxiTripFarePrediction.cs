using Microsoft.ML.Data;

namespace TaxiFarePrediction.DataStructures
{
    public sealed class TaxiTripFareVectorPrediction
    {
        [ColumnName("Score0")]
        public float[] FareAmount { get; set; }
    }
}