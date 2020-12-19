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
			this.label5 = new System.Windows.Forms.Label();
			this.terminateButton = new System.Windows.Forms.Button();
			this.inclusionsGroupBox = new System.Windows.Forms.GroupBox();
			this.addInclusionsButton = new System.Windows.Forms.Button();
			this.radiusNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.inclusionsNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.bcComboBox = new System.Windows.Forms.ComboBox();
			this.animationCheckBox = new System.Windows.Forms.CheckBox();
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.microstructureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importBmpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exportTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportBmpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.inclusionsGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.radiusNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.inclusionsNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.randomNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeYNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeXNumericUpDown)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridControl
			// 
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.EmptySpaceColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.gridControl.GridCellHeight = 0;
			this.gridControl.GridCellWidth = 0;
			this.gridControl.IsGridShowed = false;
			this.gridControl.Location = new System.Drawing.Point(203, 3);
			this.gridControl.Name = "gridControl";
			this.gridControl.Size = new System.Drawing.Size(644, 625);
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
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.03268F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.96732F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(850, 664);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.terminateButton);
			this.groupBox1.Controls.Add(this.inclusionsGroupBox);
			this.groupBox1.Controls.Add(this.bcComboBox);
			this.groupBox1.Controls.Add(this.animationCheckBox);
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
			this.groupBox1.Size = new System.Drawing.Size(194, 625);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 125);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(30, 20);
			this.label5.TabIndex = 14;
			this.label5.Text = "BC:";
			// 
			// terminateButton
			// 
			this.terminateButton.BackColor = System.Drawing.Color.Red;
			this.terminateButton.Location = new System.Drawing.Point(9, 296);
			this.terminateButton.Name = "terminateButton";
			this.terminateButton.Size = new System.Drawing.Size(86, 29);
			this.terminateButton.TabIndex = 13;
			this.terminateButton.Text = "Terminate";
			this.terminateButton.UseVisualStyleBackColor = false;
			this.terminateButton.Click += new System.EventHandler(this.TerminateButton_Click);
			// 
			// inclusionsGroupBox
			// 
			this.inclusionsGroupBox.Controls.Add(this.addInclusionsButton);
			this.inclusionsGroupBox.Controls.Add(this.radiusNumericUpDown);
			this.inclusionsGroupBox.Controls.Add(this.inclusionsNumericUpDown);
			this.inclusionsGroupBox.Controls.Add(this.label4);
			this.inclusionsGroupBox.Controls.Add(this.label3);
			this.inclusionsGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.inclusionsGroupBox.Location = new System.Drawing.Point(3, 493);
			this.inclusionsGroupBox.Name = "inclusionsGroupBox";
			this.inclusionsGroupBox.Size = new System.Drawing.Size(188, 129);
			this.inclusionsGroupBox.TabIndex = 12;
			this.inclusionsGroupBox.TabStop = false;
			this.inclusionsGroupBox.Text = "Inclusions";
			// 
			// addInclusionsButton
			// 
			this.addInclusionsButton.Location = new System.Drawing.Point(6, 92);
			this.addInclusionsButton.Name = "addInclusionsButton";
			this.addInclusionsButton.Size = new System.Drawing.Size(167, 29);
			this.addInclusionsButton.TabIndex = 4;
			this.addInclusionsButton.Text = "Add inclusions";
			this.addInclusionsButton.UseVisualStyleBackColor = true;
			this.addInclusionsButton.Click += new System.EventHandler(this.AddInclusionsButton_Click);
			// 
			// radiusNumericUpDown
			// 
			this.radiusNumericUpDown.Location = new System.Drawing.Point(78, 59);
			this.radiusNumericUpDown.Name = "radiusNumericUpDown";
			this.radiusNumericUpDown.Size = new System.Drawing.Size(95, 27);
			this.radiusNumericUpDown.TabIndex = 3;
			// 
			// inclusionsNumericUpDown
			// 
			this.inclusionsNumericUpDown.Location = new System.Drawing.Point(78, 26);
			this.inclusionsNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.inclusionsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.inclusionsNumericUpDown.Name = "inclusionsNumericUpDown";
			this.inclusionsNumericUpDown.Size = new System.Drawing.Size(95, 27);
			this.inclusionsNumericUpDown.TabIndex = 2;
			this.inclusionsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 61);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 20);
			this.label4.TabIndex = 1;
			this.label4.Text = "Radius:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 20);
			this.label3.TabIndex = 0;
			this.label3.Text = "Number:";
			// 
			// bcComboBox
			// 
			this.bcComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.bcComboBox.FormattingEnabled = true;
			this.bcComboBox.Location = new System.Drawing.Point(45, 122);
			this.bcComboBox.Name = "bcComboBox";
			this.bcComboBox.Size = new System.Drawing.Size(143, 28);
			this.bcComboBox.TabIndex = 11;
			this.bcComboBox.SelectedIndexChanged += new System.EventHandler(this.BcComboBox_SelectedIndexChanged);
			// 
			// animationCheckBox
			// 
			this.animationCheckBox.AutoSize = true;
			this.animationCheckBox.Checked = true;
			this.animationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.animationCheckBox.Location = new System.Drawing.Point(101, 264);
			this.animationCheckBox.Name = "animationCheckBox";
			this.animationCheckBox.Size = new System.Drawing.Size(87, 24);
			this.animationCheckBox.TabIndex = 10;
			this.animationCheckBox.Text = "Animate";
			this.animationCheckBox.UseVisualStyleBackColor = true;
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(9, 261);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(86, 29);
			this.startButton.TabIndex = 9;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// iterationButton
			// 
			this.iterationButton.Location = new System.Drawing.Point(9, 226);
			this.iterationButton.Name = "iterationButton";
			this.iterationButton.Size = new System.Drawing.Size(86, 29);
			this.iterationButton.TabIndex = 8;
			this.iterationButton.Text = "Iteration";
			this.iterationButton.UseVisualStyleBackColor = true;
			this.iterationButton.Click += new System.EventHandler(this.IterationButton_Click);
			// 
			// randomNumericUpDown
			// 
			this.randomNumericUpDown.Location = new System.Drawing.Point(101, 158);
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
			this.randomNumericUpDown.Size = new System.Drawing.Size(87, 27);
			this.randomNumericUpDown.TabIndex = 7;
			this.randomNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// clearButton
			// 
			this.clearButton.Location = new System.Drawing.Point(9, 191);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(86, 29);
			this.clearButton.TabIndex = 6;
			this.clearButton.Text = "Clear";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// randomButton
			// 
			this.randomButton.Location = new System.Drawing.Point(9, 156);
			this.randomButton.Name = "randomButton";
			this.randomButton.Size = new System.Drawing.Size(86, 29);
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
			this.label2.Size = new System.Drawing.Size(47, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "SizeY:";
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
            500,
            0,
            0,
            0});
			this.SizeYNumericUpDown.Leave += new System.EventHandler(this.SizeYNumericUpDown_Leave);
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
            500,
            0,
            0,
            0});
			this.SizeXNumericUpDown.Leave += new System.EventHandler(this.SizeXNumericUpDown_Leave);
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(850, 28);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.microstructureToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// microstructureToolStripMenuItem
			// 
			this.microstructureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importTextToolStripMenuItem,
            this.importBmpToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportTextToolStripMenuItem,
            this.exportBmpToolStripMenuItem});
			this.microstructureToolStripMenuItem.Name = "microstructureToolStripMenuItem";
			this.microstructureToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
			this.microstructureToolStripMenuItem.Text = "Microstructure";
			// 
			// importTextToolStripMenuItem
			// 
			this.importTextToolStripMenuItem.Name = "importTextToolStripMenuItem";
			this.importTextToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
			this.importTextToolStripMenuItem.Text = "Import (text file)";
			// 
			// importBmpToolStripMenuItem
			// 
			this.importBmpToolStripMenuItem.Name = "importBmpToolStripMenuItem";
			this.importBmpToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
			this.importBmpToolStripMenuItem.Text = "Import (bmp file)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
			// 
			// exportTextToolStripMenuItem
			// 
			this.exportTextToolStripMenuItem.Name = "exportTextToolStripMenuItem";
			this.exportTextToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
			this.exportTextToolStripMenuItem.Text = "Export (text file)";
			// 
			// exportBmpToolStripMenuItem
			// 
			this.exportBmpToolStripMenuItem.Name = "exportBmpToolStripMenuItem";
			this.exportBmpToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
			this.exportBmpToolStripMenuItem.Text = "Export (bmp file)";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(850, 692);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(500, 400);
			this.Name = "Form1";
			this.Text = "Multiscale Modelling";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.inclusionsGroupBox.ResumeLayout(false);
			this.inclusionsGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.radiusNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.inclusionsNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.randomNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeYNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeXNumericUpDown)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GridControl gridControl;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem microstructureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importBmpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportBmpToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button terminateButton;
		private System.Windows.Forms.GroupBox inclusionsGroupBox;
		private System.Windows.Forms.Button addInclusionsButton;
		private System.Windows.Forms.NumericUpDown radiusNumericUpDown;
		private System.Windows.Forms.NumericUpDown inclusionsNumericUpDown;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox bcComboBox;
		private System.Windows.Forms.CheckBox animationCheckBox;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button iterationButton;
		private System.Windows.Forms.NumericUpDown randomNumericUpDown;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.Button randomButton;
		private System.Windows.Forms.CheckBox GridCheckBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown SizeYNumericUpDown;
		private System.Windows.Forms.NumericUpDown SizeXNumericUpDown;
		private System.Windows.Forms.Label label5;
	}
}

