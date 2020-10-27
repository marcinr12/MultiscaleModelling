namespace MultiscaleModelling
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
			this.gridControl = new MultiscaleModelling.GridControl();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.startButton = new System.Windows.Forms.Button();
			this.iterationButton = new System.Windows.Forms.Button();
			this.randomNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.clearButton = new System.Windows.Forms.Button();
			this.randomButton = new System.Windows.Forms.Button();
			this.GridCheckBox = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SizeYNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.SizeXNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.randomNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeYNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeXNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl
			// 
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.GridCellHeight = 0;
			this.gridControl.GridCellWidth = 0;
			this.gridControl.IsGridShowed = false;
			this.gridControl.Location = new System.Drawing.Point(203, 3);
			this.gridControl.Name = "gridControl";
			this.gridControl.Size = new System.Drawing.Size(292, 356);
			this.gridControl.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.gridControl, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.03704F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.96296F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(498, 417);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.startButton);
			this.groupBox1.Controls.Add(this.iterationButton);
			this.groupBox1.Controls.Add(this.randomNumericUpDown);
			this.groupBox1.Controls.Add(this.clearButton);
			this.groupBox1.Controls.Add(this.randomButton);
			this.groupBox1.Controls.Add(this.GridCheckBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.SizeYNumericUpDown);
			this.groupBox1.Controls.Add(this.SizeXNumericUpDown);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(194, 356);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(9, 227);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(94, 29);
			this.startButton.TabIndex = 9;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// iterationButton
			// 
			this.iterationButton.Location = new System.Drawing.Point(9, 192);
			this.iterationButton.Name = "iterationButton";
			this.iterationButton.Size = new System.Drawing.Size(94, 29);
			this.iterationButton.TabIndex = 8;
			this.iterationButton.Text = "Iteration";
			this.iterationButton.UseVisualStyleBackColor = true;
			this.iterationButton.Click += new System.EventHandler(this.IterationButton_Click);
			// 
			// randomNumericUpDown
			// 
			this.randomNumericUpDown.Location = new System.Drawing.Point(110, 124);
			this.randomNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.randomNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.randomNumericUpDown.Name = "randomNumericUpDown";
			this.randomNumericUpDown.Size = new System.Drawing.Size(78, 27);
			this.randomNumericUpDown.TabIndex = 7;
			this.randomNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// clearButton
			// 
			this.clearButton.Location = new System.Drawing.Point(9, 157);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(94, 29);
			this.clearButton.TabIndex = 6;
			this.clearButton.Text = "Clear";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// randomButton
			// 
			this.randomButton.Location = new System.Drawing.Point(9, 122);
			this.randomButton.Name = "randomButton";
			this.randomButton.Size = new System.Drawing.Size(94, 29);
			this.randomButton.TabIndex = 5;
			this.randomButton.Text = "Random";
			this.randomButton.UseVisualStyleBackColor = true;
			this.randomButton.Click += new System.EventHandler(this.RandomButton_Click);
			// 
			// GridCheckBox
			// 
			this.GridCheckBox.AutoSize = true;
			this.GridCheckBox.Location = new System.Drawing.Point(9, 26);
			this.GridCheckBox.Name = "GridCheckBox";
			this.GridCheckBox.Size = new System.Drawing.Size(98, 24);
			this.GridCheckBox.TabIndex = 4;
			this.GridCheckBox.Text = "Show grid";
			this.GridCheckBox.UseVisualStyleBackColor = true;
			this.GridCheckBox.CheckedChanged += new System.EventHandler(this.GridCheckBox_CheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 91);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "SizeY";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "SizeX:";
			// 
			// SizeYNumericUpDown
			// 
			this.SizeYNumericUpDown.Location = new System.Drawing.Point(79, 89);
			this.SizeYNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.SizeYNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.SizeYNumericUpDown.Name = "SizeYNumericUpDown";
			this.SizeYNumericUpDown.Size = new System.Drawing.Size(109, 27);
			this.SizeYNumericUpDown.TabIndex = 1;
			this.SizeYNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.SizeYNumericUpDown.ValueChanged += new System.EventHandler(this.SizeYNumericUpDown_ValueChanged);
			// 
			// SizeXNumericUpDown
			// 
			this.SizeXNumericUpDown.Location = new System.Drawing.Point(79, 56);
			this.SizeXNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.SizeXNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.SizeXNumericUpDown.Name = "SizeXNumericUpDown";
			this.SizeXNumericUpDown.Size = new System.Drawing.Size(109, 27);
			this.SizeXNumericUpDown.TabIndex = 0;
			this.SizeXNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.SizeXNumericUpDown.ValueChanged += new System.EventHandler(this.SizeXNumericUpDown_ValueChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(498, 417);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(500, 400);
			this.Name = "Form1";
			this.Text = "Form1";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.randomNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeYNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeXNumericUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private GridControl gridControl;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown SizeYNumericUpDown;
		private System.Windows.Forms.NumericUpDown SizeXNumericUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox GridCheckBox;
		private System.Windows.Forms.NumericUpDown randomNumericUpDown;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.Button randomButton;
		private System.Windows.Forms.Button iterationButton;
		private System.Windows.Forms.Button startButton;
	}
}

