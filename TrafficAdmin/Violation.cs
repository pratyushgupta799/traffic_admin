using System;
using System.Data;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace TrafficAdmin
{
    public partial class Violation : Form
    {
        private DataTable violationTable;

        public Violation()
        {
            InitializeComponent();
        }

        private void Violation_Load(object sender, EventArgs e)
        {

        }

        private void LoadSampleData()
        {
            violationTable = new DataTable();

            violationTable.Columns.Add("Violation ID");
            violationTable.Columns.Add("Driver Name");
            violationTable.Columns.Add("City");
            violationTable.Columns.Add("Location");
            violationTable.Columns.Add("Violation Type");
            violationTable.Columns.Add("Date & Time");
            violationTable.Columns.Add("Fine Amount");
            violationTable.Columns.Add("Payment Status");
            violationTable.Columns.Add("Evidence");

            violationTable.Rows.Add("V001", "Rahul Sharma", "Delhi", "Connaught Place", "Signal Jump", "21-02-2026 10:30 AM", "₹1000", "Pending", "View");
            violationTable.Rows.Add("V002", "Priya Verma", "Mumbai", "Bandra", "No Helmet", "20-02-2026 09:15 AM", "₹500", "Paid", "View");
            violationTable.Rows.Add("V003", "Amit Singh", "Lucknow", "Hazratganj", "Over Speeding", "19-02-2026 11:45 AM", "₹2000", "Pending", "View");
            violationTable.Rows.Add("V004", "Sneha Kapoor", "Chandigarh", "Sector 17", "Wrong Parking", "18-02-2026 02:20 PM", "₹800", "Paid", "View");
            violationTable.Rows.Add("V005", "Vikas Yadav", "Patna", "Boring Road", "Signal Jump", "17-02-2026 01:10 PM", "₹1000", "Pending", "View");
            violationTable.Rows.Add("V006", "Neha Joshi", "Jaipur", "MI Road", "No Seatbelt", "16-02-2026 04:00 PM", "₹500", "Paid", "View");
            violationTable.Rows.Add("V007", "Arjun Mehta", "Pune", "Shivaji Nagar", "Over Speeding", "15-02-2026 08:45 AM", "₹2000", "Pending", "View");
            violationTable.Rows.Add("V008", "Ritika Das", "Kolkata", "Park Street", "Wrong Lane", "14-02-2026 03:30 PM", "₹700", "Paid", "View");
            violationTable.Rows.Add("V009", "Karan Malhotra", "Bangalore", "MG Road", "Signal Jump", "13-02-2026 05:50 PM", "₹1000", "Pending", "View");
            violationTable.Rows.Add("V010", "Anjali Gupta", "Hyderabad", "Banjara Hills", "No Helmet", "12-02-2026 12:40 PM", "₹500", "Paid", "View");

            dataGridView1.DataSource = violationTable;
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            string nameFilter = nameTxt.Text.Trim().Replace("'", "''");
            string typeFilter = typeTxt.Text.Trim().Replace("'", "''");

            string filterExpression = "";

            if (!string.IsNullOrEmpty(nameFilter))
            {
                filterExpression += $"[name] LIKE '%{nameFilter}%'";
            }

            if (!string.IsNullOrEmpty(typeFilter))
            {
                if (filterExpression != "")
                    filterExpression += " AND ";

                filterExpression += $"[violation_type] LIKE '%{typeFilter}%'";
            }

            violationTable.DefaultView.RowFilter = filterExpression;
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            nameTxt.Text = "";
            typeTxt.Text = "";
            violationTable.DefaultView.RowFilter = "";
        }

        private void nameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void typeTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Violation_Load_1(object sender, EventArgs e)
        {
            //LoadExcelData();
            var dt = await LoadFromApi();
            dataGridView1.DataSource = violationTable;
        }

        private void LoadExcelData()
        {
            string filePath = @"C:\Users\praty\Desktop\TrafficAdmin\face_api\violations.xlsx";

            if (!File.Exists(filePath))
            {   
                MessageBox.Show("Excel file not found.");
                return;
            }

            ExcelPackage.License.SetNonCommercialPersonal("TrafficAdmin App");

            violationTable = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                // Add columns
                for (int col = 1; col <= colCount; col++)
                {
                    violationTable.Columns.Add(worksheet.Cells[1, col].Text);
                }

                // Add rows
                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow newRow = violationTable.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        newRow[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    violationTable.Rows.Add(newRow);
                }
            }

            dataGridView1.DataSource = violationTable;
        }

        private async Task<DataTable> LoadFromApi()
        {
            string url = "https://purple-mud-c6ef.pratyush-gupta.workers.dev/";

            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(url);

                // Convert JSON to DataTable
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                violationTable = dt;
                return dt;
            }
        }
    }
}