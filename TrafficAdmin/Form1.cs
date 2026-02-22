using MathNet.Numerics;
using ScottPlot;
using System.Drawing;

namespace TrafficAdmin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Top 5 cities and example traffic violation counts
            string[] cities = { "Pune", "Chandigarh", "New Delhi", "Mumbai", "Bengaluru" };
            double[] counts = { 1987000, 985000, 1165213, 62000, 0 };

            // Use cities as “bins” just like histogram bins
            // we create arrays of “bins” that match city indices
            double[] bins = { 0, 1, 2, 3, 4 };

            // Plot as bars
            var barPlot = formsPlot1.Plot.Add.Bars(bins, counts);

            // Optional: make bars slightly narrower (not required)
            foreach (var bar in barPlot.Bars)
                bar.Size = 0.8;

            // Apply labels by manually making tick generator
            ScottPlot.Tick[] ticks = new ScottPlot.Tick[cities.Length];
            for (int i = 0; i < cities.Length; i++)
                ticks[i] = new ScottPlot.Tick(position: i, label: cities[i]);

            // Assign manual tick generator to bottom axis
            formsPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);

            // Optional: hide default tick lines
            formsPlot1.Plot.Axes.Bottom.MajorTickStyle.Length = 0;

            // Add labels
            formsPlot1.Plot.YLabel("Traffic Violations");
            formsPlot1.Plot.XLabel("City");
            formsPlot1.Plot.Title("Top 5 Cities Traffic Violations");

            // Optional: reduce margin so labels show
            formsPlot1.Plot.Axes.Margins(bottom: 0);

            // Refresh display
            formsPlot1.Refresh();

            //

            // Example time of day vs violations
            // X = hours (0 to 23), Y = violation counts
            double[] xs = { 3, 6, 9, 12, 15, 18, 21 };
            double[] ys = { 50, 120, 300, 250, 400, 320, 200 };

            // 1) plot raw data as points
            var scatter = formsPlot2.Plot.Add.Scatter(xs, ys);
            scatter.MarkerSize = 7;
            scatter.LineWidth = 0;

            // 2) do a curve fit (linear or polynomial fit)
            (double a, double b) = Fit.Line(xs, ys);

            // Fit function
            double f(double x) => a + b * x;

            // compute fitted y values
            double[] ysFit = xs.Select(f).ToArray();

            // 3) plot fitted curve
            var line = formsPlot2.Plot.Add.ScatterLine(xs, ysFit);
            line.LineWidth = 2;
            line.LinePattern = LinePattern.Dashed;

            // labels
            formsPlot2.Plot.Title("Traffic Violations vs Time of Day");
            formsPlot2.Plot.XLabel("Hour of Day (0–23)");
            formsPlot2.Plot.YLabel("Violations");

            // refresh
            formsPlot2.Refresh();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Citizens citizens = new Citizens();
            citizens.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Violation violation = new Violation();
            violation.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Upload upload = new Upload();
            upload.Show();
        }
    }
}
