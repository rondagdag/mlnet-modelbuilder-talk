// This file was automatically generated by VS extension Windows Machine Learning Code Generator v3
// from model file taxiFarePred.onnx
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
    
    public sealed class taxiFarePredInput
    {
        public TensorFloat PassengerCount; // shape(1,1)
        public TensorFloat TripTime; // shape(1,1)
        public TensorFloat TripDistance; // shape(1,1)
        public TensorFloat FareAmount; // shape(1,1)
    }
    
    public sealed class taxiFarePredOutput
    {
        public TensorFloat PassengerCount0; // shape(1,1)
        public TensorFloat TripTime0; // shape(1,1)
        public TensorFloat TripDistance0; // shape(1,1)
        public TensorFloat FareAmount0; // shape(1,1)
        public TensorFloat Features0; // shape(1,3)
        public TensorFloat Score0; // shape(1,1)
    }
    
    public sealed class taxiFarePredModel
    {
        private LearningModel model;
        private LearningModelSession session;
        private LearningModelBinding binding;
        public static async Task<taxiFarePredModel> CreateFromStreamAsync(IRandomAccessStreamReference stream)
        {
            taxiFarePredModel learningModel = new taxiFarePredModel();
            learningModel.model = await LearningModel.LoadFromStreamAsync(stream);
            learningModel.session = new LearningModelSession(learningModel.model);
            learningModel.binding = new LearningModelBinding(learningModel.session);
            return learningModel;
        }
        public async Task<taxiFarePredOutput> EvaluateAsync(taxiFarePredInput input)
        {
            binding.Bind("PassengerCount", input.PassengerCount);
            binding.Bind("TripTime", input.TripTime);
            binding.Bind("TripDistance", input.TripDistance);
            binding.Bind("FareAmount", input.FareAmount);
            var result = await session.EvaluateAsync(binding, "0");
            var output = new taxiFarePredOutput();
            output.PassengerCount0 = result.Outputs["PassengerCount0"] as TensorFloat;
            output.TripTime0 = result.Outputs["TripTime0"] as TensorFloat;
            output.TripDistance0 = result.Outputs["TripDistance0"] as TensorFloat;
            output.FareAmount0 = result.Outputs["FareAmount0"] as TensorFloat;
            output.Features0 = result.Outputs["Features0"] as TensorFloat;
            output.Score0 = result.Outputs["Score0"] as TensorFloat;
            return output;
        }
    }
}

