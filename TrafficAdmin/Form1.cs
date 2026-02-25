using MathNet.Numerics;
using Newtonsoft.Json;
using OfficeOpenXml;
using ScottPlot;
using System.Data;
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

        private async void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = await LoadFromApi();
            LoadDashboard(dt);

            //DataTable dt = LoadExcel();
            //DataTable dt = LoadFromApi().Result;

            //// ---- TOTAL VIOLATIONS
            //int totalViolations = dt.Rows.Count;
            //violationCountTxt.Text = totalViolations.ToString();

            //// ---- TOTAL FINE + DUE
            //double totalFine = 0;

            //foreach (DataRow row in dt.Rows)
            //{
            //    double fine = Convert.ToDouble(row["fine_amount"]);
            //    totalFine += fine;
            //}

            //fineDueTxt.Text = totalFine.ToString("₹0");

            //// ---- GROUP BY CITY
            //var cityGroups = dt.AsEnumerable()
            //    .GroupBy(r => r["city"].ToString())
            //    .OrderByDescending(g => g.Count())
            //    .Take(5)
            //    .ToList();

            //double[] cityCounts = cityGroups.Select(g => (double)g.Count()).ToArray();
            //string[] cities = cityGroups.Select(g => g.Key).ToArray();
            //double[] bins = Enumerable.Range(0, cities.Length).Select(x => (double)x).ToArray();

            //formsPlot1.Plot.Clear();
            //formsPlot1.Plot.Add.Bars(bins, cityCounts);

            //ScottPlot.Tick[] ticks = new ScottPlot.Tick[cities.Length];
            //for (int i = 0; i < cities.Length; i++)
            //    ticks[i] = new ScottPlot.Tick(i, cities[i]);

            //formsPlot1.Plot.Axes.Bottom.TickGenerator =
            //    new ScottPlot.TickGenerators.NumericManual(ticks);

            //formsPlot1.Plot.Title("Top Cities Violations");
            //formsPlot1.Refresh();

            //// ---- GROUP BY HOUR
            //var hourGroups = dt.AsEnumerable()
            //    .GroupBy(r =>
            //    {
            //        DateTime time = DateTime.Parse(r["date_time"].ToString());
            //        return time.Hour;
            //    })
            //    .OrderBy(g => g.Key)
            //    .ToList();

            //double[] xs = hourGroups.Select(g => (double)g.Key).ToArray();
            //double[] ys = hourGroups.Select(g => (double)g.Count()).ToArray();

            //formsPlot2.Plot.Clear();
            //formsPlot2.Plot.Add.Scatter(xs, ys);
            //formsPlot2.Plot.Title("Violations vs Time");
            //formsPlot2.Plot.XLabel("Hour");
            //formsPlot2.Plot.YLabel("Violations");
            //formsPlot2.Refresh();
        }

        private DataTable LoadExcel()
        {
            string filePath = @"C:\Users\praty\Desktop\TrafficAdmin\face_api\violations.xlsx";

            ExcelPackage.License.SetNonCommercialPersonal("TrafficAdmin");

            DataTable dt = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var ws = package.Workbook.Worksheets[0];
                int rows = ws.Dimension.End.Row;
                int cols = ws.Dimension.End.Column;

                for (int c = 1; c <= cols; c++)
                    dt.Columns.Add(ws.Cells[1, c].Text);

                for (int r = 2; r <= rows; r++)
                {
                    DataRow row = dt.NewRow();
                    for (int c = 1; c <= cols; c++)
                        row[c - 1] = ws.Cells[r, c].Text;

                    dt.Rows.Add(row);
                }
            }

            return dt;
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

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("cam and ai");
            string pythonExe = @"C:\Users\praty\AppData\Local\Programs\Python\Python311\python.exe";
            string faceApiDir = @"C:\Users\praty\Desktop\TrafficAdmin\face_api";
            string guiScript = System.IO.Path.Combine(faceApiDir, "gui_app.py");

            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = pythonExe;
            psi.Arguments = "\"" + guiScript + "\"";
            psi.WorkingDirectory = faceApiDir;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            System.Diagnostics.Process.Start(psi);
        }

        private void violationCountTxt_Click(object sender, EventArgs e)
        {

        }

        private void finesCollectedTxt_Click(object sender, EventArgs e)
        {

        }

        private void fineDueTxt_Click(object sender, EventArgs e)
        {

        }

        private async void button7_Click(object sender, EventArgs e)
        {
            //LoadExcel();
            DataTable dt = await LoadFromApi();
            LoadDashboard(dt);
        }

        private async Task<DataTable> LoadFromApi()
        {
            string url = "https://purple-mud-c6ef.pratyush-gupta.workers.dev/";

            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(url);

                // Convert JSON to DataTable
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                return dt;
            }
        }

        private void LoadDashboard(DataTable dt)
        {
            // TOTAL VIOLATIONS
            violationCountTxt.Text = dt.Rows.Count.ToString();

            double fineCollected = dt.AsEnumerable()
                .Where(r => r["status"].ToString() == "Paid")
                .Sum(r => Convert.ToDouble(r["fine_amount"]));

            double fineDue = dt.AsEnumerable()
                .Where(r => r["status"].ToString() == "Pending")
                .Sum(r => Convert.ToDouble(r["fine_amount"]));

            finesCollectedTxt.Text = fineCollected.ToString("₹0");
            fineDueTxt.Text = fineDue.ToString("₹0");

            // GROUP BY CITY
            var cityGroups = dt.AsEnumerable()
                .GroupBy(r => r["city"].ToString())
                .OrderByDescending(g => g.Count())
                .Take(5)
                .ToList();

            double[] cityCounts = cityGroups.Select(g => (double)g.Count()).ToArray();
            string[] cities = cityGroups.Select(g => g.Key).ToArray();
            double[] bins = Enumerable.Range(0, cities.Length).Select(x => (double)x).ToArray();

            formsPlot1.Plot.Clear();

            var bar = formsPlot1.Plot.Add.Bars(bins, cityCounts);
            //bar.Bars. = 0.6;

            ScottPlot.Tick[] ticks = new ScottPlot.Tick[cities.Length];
            for (int i = 0; i < cities.Length; i++)
                ticks[i] = new ScottPlot.Tick(i, cities[i]);

            formsPlot1.Plot.Axes.Bottom.TickGenerator =
                new ScottPlot.TickGenerators.NumericManual(ticks);

            formsPlot1.Plot.Title("Top Cities Violations");
            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();

            // GROUP BY HOUR
            var hourGroups = dt.AsEnumerable()
                .GroupBy(r => DateTime.Parse(r["date_time"].ToString()).Hour)
                .OrderBy(g => g.Key)
                .ToList();

            double[] xs = hourGroups.Select(g => (double)g.Key).ToArray();
            double[] ys = hourGroups.Select(g => (double)g.Count()).ToArray();

            formsPlot2.Plot.Clear();

            var scatter = formsPlot2.Plot.Add.Scatter(xs, ys);
            scatter.MarkerSize = 5;

            formsPlot2.Plot.Title("Violations vs Time");
            formsPlot2.Plot.XLabel("Hour");
            formsPlot2.Plot.YLabel("Violations");

            formsPlot2.Plot.Axes.AutoScale();
            formsPlot2.Refresh();
        }
    }
}
