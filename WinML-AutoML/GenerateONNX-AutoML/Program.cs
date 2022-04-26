using Common;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaxiFarePrediction.DataStructures;

namespace GenerateONNX_AutoML
{
    class Program
    {
        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();

            // Create, train, evaluate and save a model
            BuildTrainEvaluateAndSaveModel(mlContext);

            // Make a single test prediction loading the model from .ZIP file
            TestSinglePrediction(mlContext);

            Console.WriteLine("Press any key to exit..");
            Console.ReadLine();
        }

        private static string BaseDatasetsRelativePath = @"Data";
        private static string TrainDataRelativePath = $"{BaseDatasetsRelativePath}/taxi-fare-train.csv";
        private static string TestDataRelativePath = $"{BaseDatasetsRelativePath}/taxi-fare-test.csv";
        private static string TrainDataPath = GetAbsolutePath(TrainDataRelativePath);
        private static string TestDataPath = GetAbsolutePath(TestDataRelativePath);
        private static string LabelColumnName = "FareAmount";
        private static uint ExperimentTime = 10;

        private static readonly string MODEL_NAME = "taxiFarePred.onnx";
        private static readonly string modelPath = Directory.GetCurrentDirectory() + @"\" + MODEL_NAME;
        private static ITransformer BuildTrainEvaluateAndSaveModel(MLContext mlContext)
        {
            // STEP 1: Common data loading configuration

            /* contents of csv file 
             vendor_id,rate_code,passenger_count,trip_time_in_secs,trip_distance,payment_type,fare_amount
VTS,1,1,1140,3.75,CRD,15.5
VTS,1,1,480,2.72,CRD,10.0
VTS,1,1,1680,7.8,CSH,26.5
VTS,1,1,600,4.73,CSH,14.5
             */

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<TaxiTrip>(TrainDataPath, hasHeader: true, separatorChar: ',');
            IDataView testDataView = mlContext.Data.LoadFromTextFile<TaxiTrip>(TestDataPath, hasHeader: true, separatorChar: ',');
            // Display first few rows of the training data
            ConsoleHelper.ShowDataViewInConsole(mlContext, trainingDataView);

            // STEP 2: Initialize our user-defined progress handler that AutoML will 
            // invoke after each model it produces and evaluates.
            var progressHandler = new RegressionExperimentProgressHandler();

            // STEP 3: Run AutoML regression experiment
            ConsoleHelper.ConsoleWriteHeader("=============== Training the model ===============");
            Console.WriteLine($"Running AutoML regression experiment for {ExperimentTime} seconds...");
            ExperimentResult<RegressionMetrics> experimentResult = mlContext.Auto()
                .CreateRegressionExperiment(ExperimentTime)
                .Execute(trainingDataView, LabelColumnName, progressHandler: progressHandler);
            // Print top models found by AutoML
            Console.WriteLine();
            PrintTopModels(experimentResult);

            // STEP 4: Evaluate the model and print metrics

            ConsoleHelper.ConsoleWriteHeader("===== Evaluating model's accuracy with test data =====");
            RunDetail<RegressionMetrics> best = experimentResult.BestRun;
            ITransformer trainedModel = best.Model;
            IDataView predictions = trainedModel.Transform(testDataView);
            var metrics = mlContext.Regression.Evaluate(predictions, labelColumnName: LabelColumnName, scoreColumnName: "Score");
            // Print metrics from top model
            ConsoleHelper.PrintRegressionMetrics(best.TrainerName, metrics);

            // STEP 5: Save/persist the trained model - convonnx


            using (var stream = File.Create(MODEL_NAME))
            {
                mlContext.Model.ConvertToOnnx(trainedModel, trainingDataView, stream);
            }
            Console.WriteLine("The model is saved to {0}", MODEL_NAME);


            return trainedModel;
        }

        private static void TestSinglePrediction(MLContext mlContext)
        {
            ConsoleHelper.ConsoleWriteHeader("=============== Testing prediction engine ===============");


            var session = new InferenceSession(modelPath);

            /*cont onnx*/

            var inputMeta = session.InputMetadata;

            var container = new List<NamedOnnxValue>();
            container.Add(GetNamedOnnxValue("PassengerCount", 1f));
            container.Add(GetNamedOnnxValue("TripTime", 1140f));
            container.Add(GetNamedOnnxValue("TripDistance", 3.75f));
            container.Add(GetNamedOnnxValue("FareAmount", 0f));
            /* output onnx*/

            var output = session.Run(container).First(x => x.Name == "Score.output").AsTensor<float>().Max();

            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {output:0.####}, actual fare: 15.5");
            Console.WriteLine($"**********************************************************************");
        }

        private static NamedOnnxValue GetNamedOnnxValue<T>(string column, T value)
        {
            T[] inputDataInt = new T[] { value };
            int[] dim = new int[] { 1, 1 };
            var tensor = new DenseTensor<T>(inputDataInt, dim );//inputMeta[column].Dimensions);

            var namedOnnxValue = NamedOnnxValue.CreateFromTensor<T>(column, tensor);
            return namedOnnxValue;
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        /// <summary>
        /// Print top models from AutoML experiment.
        /// </summary>
        private static void PrintTopModels(ExperimentResult<RegressionMetrics> experimentResult)
        {
            // Get top few runs ranked by R-Squared.
            // R-Squared is a metric to maximize, so OrderByDescending() is correct.
            // For RMSE and other regression metrics, OrderByAscending() is correct.
            var topRuns = experimentResult.RunDetails
                .Where(r => r.ValidationMetrics != null && !double.IsNaN(r.ValidationMetrics.RSquared))
                .OrderByDescending(r => r.ValidationMetrics.RSquared).Take(3);

            Console.WriteLine("Top models ranked by R-Squared --");
            ConsoleHelper.PrintRegressionMetricsHeader();
            for (var i = 0; i < topRuns.Count(); i++)
            {
                var run = topRuns.ElementAt(i);
                ConsoleHelper.PrintIterationMetrics(i + 1, run.TrainerName, run.ValidationMetrics, run.RuntimeInSeconds);
            }
        }

    }
}
