﻿namespace MultiscaleModelling
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
			this.pickModeGroupBox = new System.Windows.Forms.GroupBox();
			this.pickDualPhaseRadioButton = new System.Windows.Forms.RadioButton();
			this.pickGbRadioButton = new System.Windows.Forms.RadioButton();
			this.viewGroupBox = new System.Windows.Forms.GroupBox();
			this.grainBoundariesCheckBox = new System.Windows.Forms.CheckBox();
			this.dualPhasePanel = new System.Windows.Forms.Panel();
			this.secondGrowthButton = new System.Windows.Forms.Button();
			this.clearPhaseButton = new System.Windows.Forms.Button();
			this.grainBoundaryPanel = new System.Windows.Forms.Panel();
			this.thicknessNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.showGbButton = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.ClearGbButton = new System.Windows.Forms.Button();
			this.dualPhaseRadioButton = new System.Windows.Forms.RadioButton();
			this.substructureRadioButton = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rulesPanel = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.probabilityNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.rule4checkBox = new System.Windows.Forms.CheckBox();
			this.rule1CheckBox = new System.Windows.Forms.CheckBox();
			this.rule3CheckBox = new System.Windows.Forms.CheckBox();
			this.rule2CheckBox = new System.Windows.Forms.CheckBox();
			this.shapeControlCheckBox = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.terminateButton = new System.Windows.Forms.Button();
			this.inclusionsGroupBox = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.inclusionTypeComboBox = new System.Windows.Forms.ComboBox();
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.gbLabel = new System.Windows.Forms.Label();
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
			this.pickModeGroupBox.SuspendLayout();
			this.viewGroupBox.SuspendLayout();
			this.dualPhasePanel.SuspendLayout();
			this.grainBoundaryPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.thicknessNumericUpDown)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.rulesPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.probabilityNumericUpDown)).BeginInit();
			this.inclusionsGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.radiusNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.inclusionsNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.randomNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeYNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeXNumericUpDown)).BeginInit();
			this.panel1.SuspendLayout();
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
			this.gridControl.Location = new System.Drawing.Point(213, 3);
			this.gridControl.Name = "gridControl";
			this.gridControl.ShowGrainBoundaries = false;
			this.gridControl.Size = new System.Drawing.Size(733, 1000);
			this.gridControl.TabIndex = 0;
			this.gridControl.ViewMode = MultiscaleModelling.ViewMode.Substracture;
			this.gridControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GridControl_MouseClick);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.gridControl, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(949, 1046);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.pickModeGroupBox);
			this.groupBox1.Controls.Add(this.viewGroupBox);
			this.groupBox1.Controls.Add(this.groupBox2);
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
			this.groupBox1.Size = new System.Drawing.Size(204, 1000);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// pickModeGroupBox
			// 
			this.pickModeGroupBox.Controls.Add(this.pickDualPhaseRadioButton);
			this.pickModeGroupBox.Controls.Add(this.pickGbRadioButton);
			this.pickModeGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pickModeGroupBox.Location = new System.Drawing.Point(3, 307);
			this.pickModeGroupBox.Name = "pickModeGroupBox";
			this.pickModeGroupBox.Size = new System.Drawing.Size(198, 83);
			this.pickModeGroupBox.TabIndex = 22;
			this.pickModeGroupBox.TabStop = false;
			this.pickModeGroupBox.Text = "Pick mode";
			// 
			// pickDualPhaseRadioButton
			// 
			this.pickDualPhaseRadioButton.AutoSize = true;
			this.pickDualPhaseRadioButton.Location = new System.Drawing.Point(6, 56);
			this.pickDualPhaseRadioButton.Name = "pickDualPhaseRadioButton";
			this.pickDualPhaseRadioButton.Size = new System.Drawing.Size(104, 24);
			this.pickDualPhaseRadioButton.TabIndex = 1;
			this.pickDualPhaseRadioButton.TabStop = true;
			this.pickDualPhaseRadioButton.Text = "Dual phase";
			this.pickDualPhaseRadioButton.UseVisualStyleBackColor = true;
			this.pickDualPhaseRadioButton.CheckedChanged += new System.EventHandler(this.PickDualPhaseRadioButton_CheckedChanged);
			// 
			// pickGbRadioButton
			// 
			this.pickGbRadioButton.AutoSize = true;
			this.pickGbRadioButton.Location = new System.Drawing.Point(6, 26);
			this.pickGbRadioButton.Name = "pickGbRadioButton";
			this.pickGbRadioButton.Size = new System.Drawing.Size(132, 24);
			this.pickGbRadioButton.TabIndex = 0;
			this.pickGbRadioButton.TabStop = true;
			this.pickGbRadioButton.Text = "Grain boundary";
			this.pickGbRadioButton.UseVisualStyleBackColor = true;
			this.pickGbRadioButton.CheckedChanged += new System.EventHandler(this.PickGbRadioButton_CheckedChanged);
			// 
			// viewGroupBox
			// 
			this.viewGroupBox.Controls.Add(this.grainBoundariesCheckBox);
			this.viewGroupBox.Controls.Add(this.dualPhasePanel);
			this.viewGroupBox.Controls.Add(this.grainBoundaryPanel);
			this.viewGroupBox.Controls.Add(this.dualPhaseRadioButton);
			this.viewGroupBox.Controls.Add(this.substructureRadioButton);
			this.viewGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.viewGroupBox.Location = new System.Drawing.Point(3, 390);
			this.viewGroupBox.Name = "viewGroupBox";
			this.viewGroupBox.Size = new System.Drawing.Size(198, 221);
			this.viewGroupBox.TabIndex = 21;
			this.viewGroupBox.TabStop = false;
			this.viewGroupBox.Text = "View";
			// 
			// grainBoundariesCheckBox
			// 
			this.grainBoundariesCheckBox.AutoSize = true;
			this.grainBoundariesCheckBox.Location = new System.Drawing.Point(6, 121);
			this.grainBoundariesCheckBox.Name = "grainBoundariesCheckBox";
			this.grainBoundariesCheckBox.Size = new System.Drawing.Size(144, 24);
			this.grainBoundariesCheckBox.TabIndex = 6;
			this.grainBoundariesCheckBox.Text = "Grain boundaries";
			this.grainBoundariesCheckBox.UseVisualStyleBackColor = true;
			this.grainBoundariesCheckBox.CheckedChanged += new System.EventHandler(this.GrainBoundariesCheckBox_CheckedChanged);
			// 
			// dualPhasePanel
			// 
			this.dualPhasePanel.Controls.Add(this.secondGrowthButton);
			this.dualPhasePanel.Controls.Add(this.clearPhaseButton);
			this.dualPhasePanel.Location = new System.Drawing.Point(37, 85);
			this.dualPhasePanel.Name = "dualPhasePanel";
			this.dualPhasePanel.Size = new System.Drawing.Size(155, 30);
			this.dualPhasePanel.TabIndex = 5;
			// 
			// secondGrowthButton
			// 
			this.secondGrowthButton.Location = new System.Drawing.Point(0, 0);
			this.secondGrowthButton.Name = "secondGrowthButton";
			this.secondGrowthButton.Size = new System.Drawing.Size(77, 29);
			this.secondGrowthButton.TabIndex = 4;
			this.secondGrowthButton.Text = "2nd";
			this.secondGrowthButton.UseVisualStyleBackColor = true;
			this.secondGrowthButton.Click += new System.EventHandler(this.SecondGrowthButton_Click);
			// 
			// clearPhaseButton
			// 
			this.clearPhaseButton.Location = new System.Drawing.Point(88, 0);
			this.clearPhaseButton.Name = "clearPhaseButton";
			this.clearPhaseButton.Size = new System.Drawing.Size(67, 29);
			this.clearPhaseButton.TabIndex = 3;
			this.clearPhaseButton.Text = "Clear";
			this.clearPhaseButton.UseVisualStyleBackColor = true;
			this.clearPhaseButton.Click += new System.EventHandler(this.ClearPhaseButton_Click);
			// 
			// grainBoundaryPanel
			// 
			this.grainBoundaryPanel.Controls.Add(this.thicknessNumericUpDown);
			this.grainBoundaryPanel.Controls.Add(this.showGbButton);
			this.grainBoundaryPanel.Controls.Add(this.label8);
			this.grainBoundaryPanel.Controls.Add(this.ClearGbButton);
			this.grainBoundaryPanel.Location = new System.Drawing.Point(37, 150);
			this.grainBoundaryPanel.Name = "grainBoundaryPanel";
			this.grainBoundaryPanel.Size = new System.Drawing.Size(155, 65);
			this.grainBoundaryPanel.TabIndex = 4;
			// 
			// thicknessNumericUpDown
			// 
			this.thicknessNumericUpDown.Location = new System.Drawing.Point(88, 36);
			this.thicknessNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.thicknessNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.thicknessNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.thicknessNumericUpDown.Name = "thicknessNumericUpDown";
			this.thicknessNumericUpDown.Size = new System.Drawing.Size(67, 27);
			this.thicknessNumericUpDown.TabIndex = 17;
			this.thicknessNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// showGbButton
			// 
			this.showGbButton.Location = new System.Drawing.Point(0, 0);
			this.showGbButton.Name = "showGbButton";
			this.showGbButton.Size = new System.Drawing.Size(82, 29);
			this.showGbButton.TabIndex = 0;
			this.showGbButton.Text = "Show";
			this.showGbButton.UseVisualStyleBackColor = true;
			this.showGbButton.Click += new System.EventHandler(this.ShowGbButton_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(0, 38);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(74, 20);
			this.label8.TabIndex = 18;
			this.label8.Text = "Thickness:";
			// 
			// ClearGbButton
			// 
			this.ClearGbButton.Location = new System.Drawing.Point(88, 0);
			this.ClearGbButton.Name = "ClearGbButton";
			this.ClearGbButton.Size = new System.Drawing.Size(67, 29);
			this.ClearGbButton.TabIndex = 1;
			this.ClearGbButton.Text = "Clear";
			this.ClearGbButton.UseVisualStyleBackColor = true;
			this.ClearGbButton.Click += new System.EventHandler(this.ClearGbButton_Click);
			// 
			// dualPhaseRadioButton
			// 
			this.dualPhaseRadioButton.AutoSize = true;
			this.dualPhaseRadioButton.Location = new System.Drawing.Point(7, 55);
			this.dualPhaseRadioButton.Name = "dualPhaseRadioButton";
			this.dualPhaseRadioButton.Size = new System.Drawing.Size(104, 24);
			this.dualPhaseRadioButton.TabIndex = 1;
			this.dualPhaseRadioButton.Text = "Dual phase";
			this.dualPhaseRadioButton.UseVisualStyleBackColor = true;
			this.dualPhaseRadioButton.CheckedChanged += new System.EventHandler(this.ViewModeRadioButton_CheckedChanged);
			// 
			// substructureRadioButton
			// 
			this.substructureRadioButton.AutoSize = true;
			this.substructureRadioButton.Location = new System.Drawing.Point(7, 26);
			this.substructureRadioButton.Name = "substructureRadioButton";
			this.substructureRadioButton.Size = new System.Drawing.Size(112, 24);
			this.substructureRadioButton.TabIndex = 0;
			this.substructureRadioButton.Text = "Substructure";
			this.substructureRadioButton.UseVisualStyleBackColor = true;
			this.substructureRadioButton.CheckedChanged += new System.EventHandler(this.ViewModeRadioButton_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rulesPanel);
			this.groupBox2.Controls.Add(this.shapeControlCheckBox);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox2.Location = new System.Drawing.Point(3, 611);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(198, 223);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Growth type";
			// 
			// rulesPanel
			// 
			this.rulesPanel.Controls.Add(this.label7);
			this.rulesPanel.Controls.Add(this.probabilityNumericUpDown);
			this.rulesPanel.Controls.Add(this.rule4checkBox);
			this.rulesPanel.Controls.Add(this.rule1CheckBox);
			this.rulesPanel.Controls.Add(this.rule3CheckBox);
			this.rulesPanel.Controls.Add(this.rule2CheckBox);
			this.rulesPanel.Enabled = false;
			this.rulesPanel.Location = new System.Drawing.Point(21, 59);
			this.rulesPanel.Name = "rulesPanel";
			this.rulesPanel.Size = new System.Drawing.Size(149, 156);
			this.rulesPanel.TabIndex = 16;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(0, 123);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(84, 20);
			this.label7.TabIndex = 5;
			this.label7.Text = "Probability:";
			// 
			// probabilityNumericUpDown
			// 
			this.probabilityNumericUpDown.Location = new System.Drawing.Point(90, 120);
			this.probabilityNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.probabilityNumericUpDown.Name = "probabilityNumericUpDown";
			this.probabilityNumericUpDown.Size = new System.Drawing.Size(50, 27);
			this.probabilityNumericUpDown.TabIndex = 4;
			this.probabilityNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// rule4checkBox
			// 
			this.rule4checkBox.AutoSize = true;
			this.rule4checkBox.Checked = true;
			this.rule4checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.rule4checkBox.Location = new System.Drawing.Point(0, 91);
			this.rule4checkBox.Name = "rule4checkBox";
			this.rule4checkBox.Size = new System.Drawing.Size(72, 24);
			this.rule4checkBox.TabIndex = 3;
			this.rule4checkBox.Text = "Rule 4";
			this.rule4checkBox.UseVisualStyleBackColor = true;
			// 
			// rule1CheckBox
			// 
			this.rule1CheckBox.AutoSize = true;
			this.rule1CheckBox.Checked = true;
			this.rule1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.rule1CheckBox.Location = new System.Drawing.Point(0, 0);
			this.rule1CheckBox.Name = "rule1CheckBox";
			this.rule1CheckBox.Size = new System.Drawing.Size(72, 24);
			this.rule1CheckBox.TabIndex = 0;
			this.rule1CheckBox.Text = "Rule 1";
			this.rule1CheckBox.UseVisualStyleBackColor = true;
			// 
			// rule3CheckBox
			// 
			this.rule3CheckBox.AutoSize = true;
			this.rule3CheckBox.Checked = true;
			this.rule3CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.rule3CheckBox.Location = new System.Drawing.Point(0, 60);
			this.rule3CheckBox.Name = "rule3CheckBox";
			this.rule3CheckBox.Size = new System.Drawing.Size(72, 24);
			this.rule3CheckBox.TabIndex = 2;
			this.rule3CheckBox.Text = "Rule 3";
			this.rule3CheckBox.UseVisualStyleBackColor = true;
			// 
			// rule2CheckBox
			// 
			this.rule2CheckBox.AutoSize = true;
			this.rule2CheckBox.Checked = true;
			this.rule2CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.rule2CheckBox.Location = new System.Drawing.Point(0, 29);
			this.rule2CheckBox.Name = "rule2CheckBox";
			this.rule2CheckBox.Size = new System.Drawing.Size(72, 24);
			this.rule2CheckBox.TabIndex = 1;
			this.rule2CheckBox.Text = "Rule 2";
			this.rule2CheckBox.UseVisualStyleBackColor = true;
			// 
			// shapeControlCheckBox
			// 
			this.shapeControlCheckBox.AutoSize = true;
			this.shapeControlCheckBox.Location = new System.Drawing.Point(8, 29);
			this.shapeControlCheckBox.Name = "shapeControlCheckBox";
			this.shapeControlCheckBox.Size = new System.Drawing.Size(123, 24);
			this.shapeControlCheckBox.TabIndex = 0;
			this.shapeControlCheckBox.Text = "Shape control";
			this.shapeControlCheckBox.UseVisualStyleBackColor = true;
			this.shapeControlCheckBox.CheckedChanged += new System.EventHandler(this.ShapeControlCheckBox_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 126);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(30, 20);
			this.label5.TabIndex = 14;
			this.label5.Text = "BC:";
			// 
			// terminateButton
			// 
			this.terminateButton.BackColor = System.Drawing.Color.Red;
			this.terminateButton.Location = new System.Drawing.Point(7, 261);
			this.terminateButton.Name = "terminateButton";
			this.terminateButton.Size = new System.Drawing.Size(86, 29);
			this.terminateButton.TabIndex = 13;
			this.terminateButton.Text = "Terminate";
			this.terminateButton.UseVisualStyleBackColor = false;
			this.terminateButton.Click += new System.EventHandler(this.TerminateButton_Click);
			// 
			// inclusionsGroupBox
			// 
			this.inclusionsGroupBox.Controls.Add(this.label6);
			this.inclusionsGroupBox.Controls.Add(this.inclusionTypeComboBox);
			this.inclusionsGroupBox.Controls.Add(this.addInclusionsButton);
			this.inclusionsGroupBox.Controls.Add(this.radiusNumericUpDown);
			this.inclusionsGroupBox.Controls.Add(this.inclusionsNumericUpDown);
			this.inclusionsGroupBox.Controls.Add(this.label4);
			this.inclusionsGroupBox.Controls.Add(this.label3);
			this.inclusionsGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.inclusionsGroupBox.Location = new System.Drawing.Point(3, 834);
			this.inclusionsGroupBox.Name = "inclusionsGroupBox";
			this.inclusionsGroupBox.Size = new System.Drawing.Size(198, 163);
			this.inclusionsGroupBox.TabIndex = 12;
			this.inclusionsGroupBox.TabStop = false;
			this.inclusionsGroupBox.Text = "Inclusions";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(43, 20);
			this.label6.TabIndex = 6;
			this.label6.Text = "Type:";
			// 
			// inclusionTypeComboBox
			// 
			this.inclusionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.inclusionTypeComboBox.FormattingEnabled = true;
			this.inclusionTypeComboBox.Location = new System.Drawing.Point(78, 93);
			this.inclusionTypeComboBox.Name = "inclusionTypeComboBox";
			this.inclusionTypeComboBox.Size = new System.Drawing.Size(103, 28);
			this.inclusionTypeComboBox.TabIndex = 5;
			// 
			// addInclusionsButton
			// 
			this.addInclusionsButton.Location = new System.Drawing.Point(14, 127);
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
			this.radiusNumericUpDown.Size = new System.Drawing.Size(104, 27);
			this.radiusNumericUpDown.TabIndex = 3;
			this.radiusNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// inclusionsNumericUpDown
			// 
			this.inclusionsNumericUpDown.Location = new System.Drawing.Point(78, 27);
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
			this.inclusionsNumericUpDown.Size = new System.Drawing.Size(104, 27);
			this.inclusionsNumericUpDown.TabIndex = 2;
			this.inclusionsNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 61);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 20);
			this.label4.TabIndex = 1;
			this.label4.Text = "Size:";
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
			this.bcComboBox.Location = new System.Drawing.Point(43, 124);
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
			this.animationCheckBox.Location = new System.Drawing.Point(99, 229);
			this.animationCheckBox.Name = "animationCheckBox";
			this.animationCheckBox.Size = new System.Drawing.Size(87, 24);
			this.animationCheckBox.TabIndex = 10;
			this.animationCheckBox.Text = "Animate";
			this.animationCheckBox.UseVisualStyleBackColor = true;
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(7, 226);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(86, 29);
			this.startButton.TabIndex = 9;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// iterationButton
			// 
			this.iterationButton.Location = new System.Drawing.Point(7, 192);
			this.iterationButton.Name = "iterationButton";
			this.iterationButton.Size = new System.Drawing.Size(86, 29);
			this.iterationButton.TabIndex = 8;
			this.iterationButton.Text = "Iteration";
			this.iterationButton.UseVisualStyleBackColor = true;
			this.iterationButton.Click += new System.EventHandler(this.IterationButton_Click);
			// 
			// randomNumericUpDown
			// 
			this.randomNumericUpDown.Location = new System.Drawing.Point(99, 158);
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
            3,
            0,
            0,
            0});
			// 
			// clearButton
			// 
			this.clearButton.Location = new System.Drawing.Point(100, 192);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(86, 29);
			this.clearButton.TabIndex = 6;
			this.clearButton.Text = "Clear";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// randomButton
			// 
			this.randomButton.Location = new System.Drawing.Point(7, 157);
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
			this.GridCheckBox.Location = new System.Drawing.Point(9, 27);
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
			this.label2.Location = new System.Drawing.Point(7, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "SizeY:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "SizeX:";
			// 
			// SizeYNumericUpDown
			// 
			this.SizeYNumericUpDown.Location = new System.Drawing.Point(77, 90);
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
            200,
            0,
            0,
            0});
			this.SizeYNumericUpDown.Leave += new System.EventHandler(this.SizeYNumericUpDown_Leave);
			// 
			// SizeXNumericUpDown
			// 
			this.SizeXNumericUpDown.Location = new System.Drawing.Point(77, 57);
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
            200,
            0,
            0,
            0});
			this.SizeXNumericUpDown.Leave += new System.EventHandler(this.SizeXNumericUpDown_Leave);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.gbLabel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(213, 1010);
			this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(733, 32);
			this.panel1.TabIndex = 2;
			// 
			// gbLabel
			// 
			this.gbLabel.AutoSize = true;
			this.gbLabel.Location = new System.Drawing.Point(0, 12);
			this.gbLabel.Name = "gbLabel";
			this.gbLabel.Size = new System.Drawing.Size(53, 20);
			this.gbLabel.TabIndex = 0;
			this.gbLabel.Text = "GB: ---";
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
			this.menuStrip1.Size = new System.Drawing.Size(949, 30);
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
			this.ClientSize = new System.Drawing.Size(949, 1076);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(959, 822);
			this.Name = "Form1";
			this.Text = "Multiscale Modelling";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.pickModeGroupBox.ResumeLayout(false);
			this.pickModeGroupBox.PerformLayout();
			this.viewGroupBox.ResumeLayout(false);
			this.viewGroupBox.PerformLayout();
			this.dualPhasePanel.ResumeLayout(false);
			this.grainBoundaryPanel.ResumeLayout(false);
			this.grainBoundaryPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.thicknessNumericUpDown)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.rulesPanel.ResumeLayout(false);
			this.rulesPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.probabilityNumericUpDown)).EndInit();
			this.inclusionsGroupBox.ResumeLayout(false);
			this.inclusionsGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.radiusNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.inclusionsNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.randomNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeYNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SizeXNumericUpDown)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
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
		private System.Windows.Forms.ComboBox inclusionTypeComboBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox shapeControlCheckBox;
		private System.Windows.Forms.CheckBox rule4checkBox;
		private System.Windows.Forms.CheckBox rule3CheckBox;
		private System.Windows.Forms.CheckBox rule2CheckBox;
		private System.Windows.Forms.CheckBox rule1CheckBox;
		private System.Windows.Forms.Panel rulesPanel;
		private System.Windows.Forms.NumericUpDown probabilityNumericUpDown;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown thicknessNumericUpDown;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label gbLabel;
		private System.Windows.Forms.Button ClearGbButton;
		private System.Windows.Forms.Button showGbButton;
		private System.Windows.Forms.GroupBox viewGroupBox;
		private System.Windows.Forms.RadioButton dualPhaseRadioButton;
		private System.Windows.Forms.RadioButton substructureRadioButton;
		private System.Windows.Forms.Button clearPhaseButton;
		private System.Windows.Forms.Panel grainBoundaryPanel;
		private System.Windows.Forms.Panel dualPhasePanel;
		private System.Windows.Forms.Button secondGrowthButton;
		private System.Windows.Forms.CheckBox grainBoundariesCheckBox;
		private System.Windows.Forms.GroupBox pickModeGroupBox;
		private System.Windows.Forms.RadioButton pickDualPhaseRadioButton;
		private System.Windows.Forms.RadioButton pickGbRadioButton;
	}
}

