namespace TrafficAdmin
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            label1 = new Label();
            panel3 = new Panel();
            label3 = new Label();
            label2 = new Label();
            panel4 = new Panel();
            label4 = new Label();
            label5 = new Label();
            panel5 = new Panel();
            label6 = new Label();
            label7 = new Label();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            formsPlot2 = new ScottPlot.WinForms.FormsPlot();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(18, 71, 127);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(-9, 96);
            panel1.Name = "panel1";
            panel1.Size = new Size(354, 609);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, -2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(344, 92);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(212, 157, 19);
            panel2.Location = new Point(1, 96);
            panel2.Name = "panel2";
            panel2.Size = new Size(344, 10);
            panel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(396, 36);
            label1.Name = "label1";
            label1.Size = new Size(199, 38);
            label1.TabIndex = 2;
            label1.Text = "Top Summary";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(18, 71, 127);
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(label3);
            panel3.Controls.Add(label2);
            panel3.ForeColor = Color.White;
            panel3.Location = new Point(374, 96);
            panel3.Name = "panel3";
            panel3.Size = new Size(332, 201);
            panel3.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(30, 78);
            label3.Name = "label3";
            label3.Size = new Size(226, 81);
            label3.TabIndex = 1;
            label3.Text = "84,569";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(15, 9);
            label2.Name = "label2";
            label2.Size = new Size(241, 31);
            label2.TabIndex = 0;
            label2.Text = "Total Violation Today";
            // 
            // panel4
            // 
            panel4.BackColor = Color.White;
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(label4);
            panel4.Controls.Add(label5);
            panel4.ForeColor = Color.White;
            panel4.Location = new Point(725, 96);
            panel4.Name = "panel4";
            panel4.Size = new Size(338, 201);
            panel4.TabIndex = 4;
            panel4.Paint += panel4_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(47, 78);
            label4.Name = "label4";
            label4.Size = new Size(242, 81);
            label4.TabIndex = 1;
            label4.Text = "1.3 lacs";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(15, 9);
            label5.Name = "label5";
            label5.Size = new Size(165, 31);
            label5.TabIndex = 0;
            label5.Text = "Fine Collected";
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.BorderStyle = BorderStyle.Fixed3D;
            panel5.Controls.Add(label6);
            panel5.Controls.Add(label7);
            panel5.ForeColor = Color.White;
            panel5.Location = new Point(1091, 96);
            panel5.Name = "panel5";
            panel5.Size = new Size(337, 201);
            panel5.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(44, 78);
            label6.Name = "label6";
            label6.Size = new Size(242, 81);
            label6.TabIndex = 1;
            label6.Text = "0.5 lacs";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(15, 9);
            label7.Name = "label7";
            label7.Size = new Size(108, 31);
            label7.TabIndex = 0;
            label7.Text = "Fine Due";
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1.25F;
            formsPlot1.Location = new Point(374, 367);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(513, 264);
            formsPlot1.TabIndex = 6;
            formsPlot1.Load += formsPlot1_Load;
            // 
            // formsPlot2
            // 
            formsPlot2.DisplayScale = 1.25F;
            formsPlot2.Location = new Point(937, 367);
            formsPlot2.Name = "formsPlot2";
            formsPlot2.Size = new Size(491, 264);
            formsPlot2.TabIndex = 7;
            // 
            // button1
            // 
            button1.Location = new Point(42, 64);
            button1.Name = "button1";
            button1.Size = new Size(312, 39);
            button1.TabIndex = 0;
            button1.Text = "Database";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1455, 704);
            Controls.Add(formsPlot2);
            Controls.Add(formsPlot1);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(label1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Label label1;
        private Panel panel3;
        private Label label2;
        private Label label3;
        private Panel panel4;
        private Label label4;
        private Label label5;
        private Panel panel5;
        private Label label6;
        private Label label7;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private ScottPlot.WinForms.FormsPlot formsPlot2;
        private Button button1;
    }
}
