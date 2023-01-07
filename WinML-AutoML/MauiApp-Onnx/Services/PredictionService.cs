using MauiApp_Onnx.Models;
using Microsoft.Maui.Graphics;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace MauiApp_Onnx.Services
{
    public class PredictionService
    {
        public InferenceSession LoadModel()
        {
            using var modelStream = FileSystem.OpenAppPackageFileAsync("taxiFarePred.onnx").Result;

            using var modelMemoryStream = new MemoryStream();
            modelStream.CopyTo(modelMemoryStream);

            var _model = modelMemoryStream.ToArray();
            InferenceSession inferenceSession = new InferenceSession(model: _model);

            return inferenceSession;
        }

        private readonly InferenceSession session;

        public PredictionService() => session = LoadModel();

        private static NamedOnnxValue GetOnnxValue<T>(string column, T value)
        {
            T[] inputData = new T[] { value };
            int[] dim = new int[] { 1, 1 };
            var tensor = new DenseTensor<T>(inputData, dim);
            var namedOnnxValue = NamedOnnxValue.CreateFromTensor<T>(column, tensor);
            return namedOnnxValue;
        }

        public float Predict(TaxiTrip taxiTrip)
        {
            // Setup inputs
            var inputs = new List<NamedOnnxValue>
            {
                GetOnnxValue("PassengerCount", taxiTrip.PassengerCount),
                GetOnnxValue("TripTime", taxiTrip.TripTime),
                GetOnnxValue("TripDistance", taxiTrip.TripDistance),
                GetOnnxValue("FareAmount", 0f)
            };

            // Run inference
            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> result = session.Run(inputs);

            // Return prediction
            var output = result.First(x => x.Name == "Score.output").AsTensor<float>().ToArray();            
            var pred = output.Max();
            return pred;
        }
    }
}
