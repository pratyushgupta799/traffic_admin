namespace TrafficAdmin
{
    partial class Citizens
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            filterBtn = new Button();
            label4 = new Label();
            aadhaarTxt = new TextBox();
            label3 = new Label();
            nameTxt = new TextBox();
            label1 = new Label();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            label2 = new Label();
            resetBtn = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(18, 71, 127);
            panel1.Controls.Add(resetBtn);
            panel1.Controls.Add(filterBtn);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(aadhaarTxt);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(nameTxt);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(0, -4);
            panel1.Name = "panel1";
            panel1.Size = new Size(224, 615);
            panel1.TabIndex = 1;
            // 
            // filterBtn
            // 
            filterBtn.BackColor = Color.FromArgb(212, 157, 19);
            filterBtn.FlatAppearance.BorderSize = 0;
            filterBtn.FlatStyle = FlatStyle.Flat;
            filterBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            filterBtn.ForeColor = SystemColors.ButtonHighlight;
            filterBtn.Location = new Point(56, 259);
            filterBtn.Name = "filterBtn";
            filterBtn.Size = new Size(94, 29);
            filterBtn.TabIndex = 8;
            filterBtn.Text = "Filter";
            filterBtn.UseVisualStyleBackColor = false;
            filterBtn.Click += filterBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Light", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(22, 167);
            label4.Name = "label4";
            label4.Size = new Size(76, 25);
            label4.TabIndex = 7;
            label4.Text = "Aadhaar";
            // 
            // aadhaarTxt
            // 
            aadhaarTxt.Location = new Point(22, 195);
            aadhaarTxt.Name = "aadhaarTxt";
            aadhaarTxt.Size = new Size(160, 27);
            aadhaarTxt.TabIndex = 6;
            aadhaarTxt.TextChanged += aadhaarTxt_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Light", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(22, 106);
            label3.Name = "label3";
            label3.Size = new Size(58, 25);
            label3.TabIndex = 5;
            label3.Text = "Name";
            // 
            // nameTxt
            // 
            nameTxt.Location = new Point(22, 134);
            nameTxt.Name = "nameTxt";
            nameTxt.Size = new Size(160, 27);
            nameTxt.TabIndex = 4;
            nameTxt.TextChanged += nameTxt_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(22, 25);
            label1.Name = "label1";
            label1.Size = new Size(118, 38);
            label1.TabIndex = 3;
            label1.Text = "Citizens";
            label1.Click += label1_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(212, 157, 19);
            panel2.Location = new Point(218, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(16, 623);
            panel2.TabIndex = 2;
            panel2.Paint += panel2_Paint;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(255, 78);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1020, 473);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(255, 9);
            label2.Name = "label2";
            label2.Size = new Size(159, 38);
            label2.TabIndex = 3;
            label2.Text = "Citizen List";
            // 
            // resetBtn
            // 
            resetBtn.BackColor = Color.FromArgb(212, 157, 19);
            resetBtn.FlatAppearance.BorderSize = 0;
            resetBtn.FlatStyle = FlatStyle.Flat;
            resetBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            resetBtn.ForeColor = SystemColors.ButtonHighlight;
            resetBtn.Location = new Point(56, 309);
            resetBtn.Name = "resetBtn";
            resetBtn.Size = new Size(94, 29);
            resetBtn.TabIndex = 9;
            resetBtn.Text = "Reset";
            resetBtn.UseVisualStyleBackColor = false;
            resetBtn.Click += resetBtn_Click;
            // 
            // Citizens
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1300, 563);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Name = "Citizens";
            Text = "Citizens";
            Load += Citizens_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private DataGridView dataGridView1;
        private Label label2;
        private TextBox nameTxt;
        private Label label3;
        private Label label4;
        private TextBox aadhaarTxt;
        private Button filterBtn;
        private Button resetBtn;
    }
}