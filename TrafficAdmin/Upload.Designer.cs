namespace TrafficAdmin
{
    partial class Upload
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
            label1 = new Label();
            panel2 = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            uploadBtn = new Button();
            textBox1 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            violationTxt = new TextBox();
            submtiBtn = new Button();
            label5 = new Label();
            dateTimePicker1 = new DateTimePicker();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(18, 71, 127);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(0, -4);
            panel1.Name = "panel1";
            panel1.Size = new Size(224, 615);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(22, 25);
            label1.Name = "label1";
            label1.Size = new Size(149, 38);
            label1.TabIndex = 3;
            label1.Text = "Violations";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(212, 157, 19);
            panel2.Location = new Point(218, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(16, 623);
            panel2.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(644, 21);
            label2.Name = "label2";
            label2.Size = new Size(239, 38);
            label2.TabIndex = 3;
            label2.Text = "Upload Violation";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(626, 85);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(280, 199);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // uploadBtn
            // 
            uploadBtn.Location = new Point(725, 299);
            uploadBtn.Name = "uploadBtn";
            uploadBtn.Size = new Size(94, 29);
            uploadBtn.TabIndex = 5;
            uploadBtn.Text = "Upload";
            uploadBtn.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(730, 343);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(176, 27);
            textBox1.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(666, 350);
            label3.Name = "label3";
            label3.Size = new Size(44, 20);
            label3.TabIndex = 7;
            label3.Text = "Place";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(641, 429);
            label4.Name = "label4";
            label4.Size = new Size(69, 20);
            label4.TabIndex = 9;
            label4.Text = "Violation";
            // 
            // violationTxt
            // 
            violationTxt.Location = new Point(730, 426);
            violationTxt.Name = "violationTxt";
            violationTxt.Size = new Size(176, 27);
            violationTxt.TabIndex = 8;
            // 
            // submtiBtn
            // 
            submtiBtn.BackColor = Color.FromArgb(212, 157, 19);
            submtiBtn.FlatAppearance.BorderSize = 0;
            submtiBtn.FlatStyle = FlatStyle.Flat;
            submtiBtn.ForeColor = SystemColors.ButtonHighlight;
            submtiBtn.Location = new Point(725, 479);
            submtiBtn.Name = "submtiBtn";
            submtiBtn.Size = new Size(94, 29);
            submtiBtn.TabIndex = 10;
            submtiBtn.Text = "Submit";
            submtiBtn.UseVisualStyleBackColor = false;
            submtiBtn.Click += submtiBtn_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(666, 390);
            label5.Name = "label5";
            label5.Size = new Size(42, 20);
            label5.TabIndex = 12;
            label5.Text = "Time";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(730, 385);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(176, 27);
            dateTimePicker1.TabIndex = 13;
            // 
            // Upload
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1300, 563);
            Controls.Add(dateTimePicker1);
            Controls.Add(label5);
            Controls.Add(submtiBtn);
            Controls.Add(label4);
            Controls.Add(violationTxt);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(uploadBtn);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(panel1);
            Name = "Upload";
            Text = "Citizens";
            Load += Violation_Load_1;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private PictureBox pictureBox1;
        private Button uploadBtn;
        private TextBox textBox1;
        private Label label3;
        private Label label4;
        private TextBox violationTxt;
        private Button submtiBtn;
        private Label label5;
        private DateTimePicker dateTimePicker1;
    }
}