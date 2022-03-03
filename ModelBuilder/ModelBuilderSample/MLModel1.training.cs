﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Transforms.Image;
using Microsoft.ML;

namespace ModelBuilderSample
{
    public partial class MLModel1
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ResizeImages(outputColumnName:@"input1",inputColumnName:@"input1",imageHeight:224,imageWidth:224,cropAnchor:ImageResizingEstimator.Anchor.Center,resizing:ImageResizingEstimator.ResizingKind.IsoCrop)      
                                    .Append(mlContext.Transforms.ExtractPixels(outputColumnName:@"input1",inputColumnName:@"input1",colorsToExtract:ImagePixelExtractingEstimator.ColorBits.Rgb,orderOfExtraction:ImagePixelExtractingEstimator.ColorsOrder.ARGB,outputAsFloatArray:true))      
                                    .Append(mlContext.Transforms.ApplyOnnxModel(modelFile:@"D:\presentations\mlnet-modelbuilder-talk\ModelBuilder\ModelBuilderSample\MLModel1.onnx",fallbackToCpu:true));

            return pipeline;
        }
    }
}
