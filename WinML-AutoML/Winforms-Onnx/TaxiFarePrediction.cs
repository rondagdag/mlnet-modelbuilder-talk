using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;
namespace WinForms_WinML_ONNX
{
    public partial class TaxiFarePrediction : Form
    {
        public TaxiFarePrediction()
        {
            InitializeComponent();
        }

        private static NamedOnnxValue GetOnnxValue<T>(string column, T value)
        {
            T[] inputData = new T[] { value };
            int[] dim = new int[] { 1, 1 };
            var tensor = new DenseTensor<T>(inputData, dim);
            var namedOnnxValue = NamedOnnxValue.CreateFromTensor<T>(column, tensor);
            return namedOnnxValue;
        }

        private void Predict()
        {
            var inputMeta = _session.InputMetadata;
            var container = new List<NamedOnnxValue>
            {
                GetOnnxValue("PassengerCount", float.Parse(passengerCountTB.Text)),
                GetOnnxValue("TripTime", float.Parse(tripDistanceTB.Text)),
                GetOnnxValue("TripDistance", float.Parse(tripDistanceTB.Text)),
                GetOnnxValue("FareAmount", 0f)
            };

            var result = _session.Run(container);

            var output = result.First(x => x.Name == "Score.output").AsTensor<float>().ToArray();
            var scores = result.Select(x => x.AsTensor<float>()).ToArray();
            var pred = output.Max();
            ShowResult(pred, output, 0);
        }


        private InferenceSession _session;
        private void LoadModel(string file)
        {
            _session = new InferenceSession(file);
            textUrl.Text = "LOADED!";
        }

        private string Stringify(float[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0) sb.Append("{\r\n\t");
                else if (i % 28 == 0)
                    sb.Append("\r\n\t");
                sb.Append($"{data[i],3:##0}, ");

            }
            sb.Append("\r\n}\r\n");
            return sb.ToString();
        }

        private void ShowResult(float prediction, float[] scores, double time, Func<double, double> conversion = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Scores:");

            for (int i = 0; i < scores.Length; i++)
            {
                double v = conversion == null ? scores[i] : conversion(scores[i]);
                sb.AppendLine($"\t{i}: {v}");
            }

            sb.AppendLine($"Prediction: {prediction}");
            // sb.AppendLine($"Time: {time}");
            labelPrediction.Text = prediction.ToString();

            textResponse.Text = "";
            textResponse.Text = sb.ToString();
        }

        private void Clear()
        {
            textResponse.Clear();
            labelPrediction.Text = "";
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK && File.Exists(openFile.FileName))
                LoadModel(openFile.FileName);
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void ButtonRecognize_Click(object sender, EventArgs e)
        {
            Predict();
        }

        private void TextResponse_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
