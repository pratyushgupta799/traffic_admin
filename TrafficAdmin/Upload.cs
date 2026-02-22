using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.ML;

namespace TrafficAdmin
{
    public partial class Upload : Form
    {
        private DataTable violationTable;
        MLContext mlContext;
        ITransformer model;
        PredictionEngine<TrafficData, TrafficPrediction> predictionEngine;

        public Upload()
        {
            InitializeComponent();
        }

        private void Violation_Load(object sender, EventArgs e)
        {

        }

        private void nameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void typeTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Violation_Load_1(object sender, EventArgs e)
        {
            TrainModel();
        }

        private void TrainModel()
        {
            mlContext = new MLContext();

            IDataView dataView = mlContext.Data.LoadFromTextFile<TrafficData>(
                "traffic_data.csv",
                hasHeader: true,
                separatorChar: ','
            );

            var pipeline = mlContext.Transforms.Conversion
                .MapValueToKey("Label", nameof(TrafficData.Category))
                .Append(mlContext.Transforms.Text.FeaturizeText("Features", nameof(TrafficData.Text)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            model = pipeline.Fit(dataView);

            predictionEngine = mlContext.Model.CreatePredictionEngine<TrafficData, TrafficPrediction>(model);
        }

        private void submtiBtn_Click(object sender, EventArgs e)
        {
            string description = violationTxt.Text;

            var input = new TrafficData
            {
                Text = description,
            };

            var prediction = predictionEngine.Predict(input);

            MessageBox.Show("Category: " + prediction.PredictedCategory);
        }
    }
}