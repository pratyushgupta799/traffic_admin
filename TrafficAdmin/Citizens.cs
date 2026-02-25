using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficAdmin
{
    public partial class Citizens : Form
    {
        private DataTable citizensTable;

        public Citizens()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            string nameFilter = nameTxt.Text.Trim().Replace("'", "''");
            string aadhaarFilter = aadhaarTxt.Text.Trim().Replace("'", "''");

            string filterExpression = "";

            if (!string.IsNullOrEmpty(nameFilter))
            {
                filterExpression += $"name LIKE '%{nameFilter}%'";
            }

            if (!string.IsNullOrEmpty(aadhaarFilter))
            {
                if (filterExpression != "")
                    filterExpression += " AND ";

                filterExpression += $"[Aadhaar Number] LIKE '%{aadhaarFilter}%'";
            }

            citizensTable.DefaultView.RowFilter = filterExpression;
        }

        private void nameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void aadhaarTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Citizens_Load(object sender, EventArgs e)
        {
            LoadExcelData();
        }

        private void LoadSampleData()
        {
            citizensTable = new DataTable();

            citizensTable.Columns.Add("Name");
            citizensTable.Columns.Add("Phone Number");
            citizensTable.Columns.Add("Aadhaar Number");
            citizensTable.Columns.Add("Address");
            citizensTable.Columns.Add("Driving License No");
            citizensTable.Columns.Add("Total Violations");
            citizensTable.Columns.Add("Amount Due");
            citizensTable.Columns.Add("Status");

            citizensTable.Rows.Add("Rahul Sharma", "9876543210", "123412341234", "Delhi", "DL-01-2021001", 2, "₹2000", "Pending");
            citizensTable.Rows.Add("Priya Verma", "9123456780", "234523452345", "Mumbai", "MH-02-2020456", 0, "₹0", "Clear");
            citizensTable.Rows.Add("Amit Singh", "9988776655", "345634563456", "Lucknow", "UP-32-2020789", 5, "₹7500", "Blacklisted");
            citizensTable.Rows.Add("Sneha Kapoor", "9012345678", "456745674567", "Chandigarh", "CH-01-2020321", 1, "₹1000", "Pending");
            citizensTable.Rows.Add("Vikas Yadav", "8899776655", "567856785678", "Patna", "BR-01-2020111", 3, "₹3000", "Pending");
            citizensTable.Rows.Add("Neha Joshi", "7766554433", "678967896789", "Jaipur", "RJ-14-2020222", 0, "₹0", "Clear");
            citizensTable.Rows.Add("Arjun Mehta", "6655443322", "789078907890", "Pune", "MH-12-2020333", 4, "₹5000", "Pending");
            citizensTable.Rows.Add("Ritika Das", "5544332211", "890189018901", "Kolkata", "WB-02-2020444", 0, "₹0", "Clear");
            citizensTable.Rows.Add("Karan Malhotra", "4433221100", "901290129012", "Bangalore", "KA-05-2020555", 6, "₹9000", "Blacklisted");
            citizensTable.Rows.Add("Anjali Gupta", "3322110099", "112233445566", "Hyderabad", "TS-09-2020666", 2, "₹2000", "Pending");

            dataGridView1.DataSource = citizensTable;
        }

        private void LoadExcelData()
        {
            string filePath = @"C:\Users\praty\Desktop\TrafficAdmin\face_api\traffic_db.xlsx";

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Excel file not found.");
                return;
            }

            ExcelPackage.License.SetNonCommercialPersonal("TrafficAdmin App");

            citizensTable = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                // Add columns
                for (int col = 1; col <= colCount; col++)
                {
                    citizensTable.Columns.Add(worksheet.Cells[1, col].Text);
                }

                // Add rows
                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow newRow = citizensTable.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        newRow[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    citizensTable.Rows.Add(newRow);
                }
            }

            dataGridView1.DataSource = citizensTable;
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            // Clear textboxes
            nameTxt.Text = "";
            aadhaarTxt.Text = "";

            // Remove filter
            citizensTable.DefaultView.RowFilter = "";
        }
    }
}
