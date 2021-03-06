// This file was automatically generated by VS extension Windows Machine Learning Code Generator v3
// from model file model.onnx
// Warning: This file may get overwritten if you add add an onnx file with the same name
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.AI.MachineLearning;
namespace App1
{
    
    public sealed class modelInput
    {
        public TensorString VendorId; // shape(1,1)
        public TensorString RateCode; // shape(1,1)
        public TensorFloat PassengerCount; // shape(1,1)
        public TensorFloat TripTime; // shape(1,1)
        public TensorFloat TripDistance; // shape(1,1)
        public TensorString PaymentType; // shape(1,1)
        public TensorFloat FareAmount; // shape(1,1)
    }
    
    public sealed class modelOutput
    {
        public TensorFloat VendorId2; // shape(1,2)
        public TensorFloat RateCode2; // shape(1,6)
        public TensorFloat PassengerCount0; // shape(1,1)
        public TensorFloat TripTime0; // shape(1,1)
        public TensorFloat TripDistance0; // shape(1,1)
        public TensorFloat PaymentType2; // shape(1,5)
        public TensorFloat FareAmount0; // shape(1,1)
        public TensorFloat Features0; // shape(1,16)
        public TensorFloat Score0; // shape(1,1)
    }
    
    public sealed class modelModel
    {
        private LearningModel model;
        private LearningModelSession session;
        private LearningModelBinding binding;
        public static async Task<modelModel> CreateFromStreamAsync(IRandomAccessStreamReference stream)
        {
            modelModel learningModel = new modelModel();
            learningModel.model = await LearningModel.LoadFromStreamAsync(stream);
            learningModel.session = new LearningModelSession(learningModel.model);
            learningModel.binding = new LearningModelBinding(learningModel.session);
            return learningModel;
        }
        public async Task<modelOutput> EvaluateAsync(modelInput input)
        {
            binding.Bind("VendorId", input.VendorId);
            binding.Bind("RateCode", input.RateCode);
            binding.Bind("PassengerCount", input.PassengerCount);
            binding.Bind("TripTime", input.TripTime);
            binding.Bind("TripDistance", input.TripDistance);
            binding.Bind("PaymentType", input.PaymentType);
            binding.Bind("FareAmount", input.FareAmount);
            var result = await session.EvaluateAsync(binding, "0");
            var output = new modelOutput();
            output.VendorId2 = result.Outputs["VendorId2"] as TensorFloat;
            output.RateCode2 = result.Outputs["RateCode2"] as TensorFloat;
            output.PassengerCount0 = result.Outputs["PassengerCount0"] as TensorFloat;
            output.TripTime0 = result.Outputs["TripTime0"] as TensorFloat;
            output.TripDistance0 = result.Outputs["TripDistance0"] as TensorFloat;
            output.PaymentType2 = result.Outputs["PaymentType2"] as TensorFloat;
            output.FareAmount0 = result.Outputs["FareAmount0"] as TensorFloat;
            output.Features0 = result.Outputs["Features0"] as TensorFloat;
            output.Score0 = result.Outputs["Score0"] as TensorFloat;
            return output;
        }
    }
}

