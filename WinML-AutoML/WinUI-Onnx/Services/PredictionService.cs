using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using WinUI_Onnx.Models;

namespace WinUI_Onnx.Services
{
    public class PredictionService
    {

        public async Task<InferenceSession> LoadModel()
        {

            var modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/taxiFarePred.onnx"));

            if (modelFile != null)
            {
                using Windows.Storage.Streams.IRandomAccessStream randAccStream =
                    await modelFile.OpenAsync(Windows.Storage.FileAccessMode.Read);

                using var modelMemoryStream = new MemoryStream();
                randAccStream.AsStream().CopyTo(modelMemoryStream);

                var _model = modelMemoryStream.ToArray();
                InferenceSession inferenceSession = new InferenceSession(model: _model);

                return inferenceSession;
            }

            return null;

        }

        private InferenceSession session;

        public async Task Loader()
        {
            session = await LoadModel();
        }

        private static NamedOnnxValue GetOnnxValue<T>(string column, T value)
        {
            T[] inputData = new T[] { value };
            int[] dim = new int[] { 1, 1 };
            var tensor = new DenseTensor<T>(inputData, dim);
            var namedOnnxValue = NamedOnnxValue.CreateFromTensor(column, tensor);
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
