﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Convert;

namespace MultiscaleModelling
{
	public partial class Form1 : Form
	{
		Calculation Calculation { get; set; }
		public Form1()
		{
			InitializeComponent();
			SizeXNumericUpDown.Value = 10;
			SizeYNumericUpDown.Value = 10;
			SizeXNumericUpDown.MouseWheel += NumericUpDown_MouseWheel; 
			SizeYNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;

			GridCheckBox.Checked = true;

			Calculation = new Calculation(gridControl.CellsMatrix);
			Calculation.SetRandomCells(10);
			gridControl.Refresh();
		}

		private void NumericUpDown_MouseWheel(object sender, MouseEventArgs e)
		{
			int increment = 1;

			NumericUpDown numericUpDown = sender as NumericUpDown;
			if (e is HandledMouseEventArgs hme)
				hme.Handled = true;

			if (e.Delta > 0 && numericUpDown.Value + increment <= numericUpDown.Maximum)
				numericUpDown.Value += 1;
			else if (e.Delta < 0 && numericUpDown.Value - increment >= numericUpDown.Minimum)
				numericUpDown.Value -= 1;
		}

		private void SizeXNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			gridControl.GridCellWidth = Convert.ToInt32(SizeXNumericUpDown.Value);
		}

		private void SizeYNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			gridControl.GridCellHeight = Convert.ToInt32(SizeYNumericUpDown.Value);
		}

		private void GridCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			gridControl.IsGridShowed = GridCheckBox.Checked;
		}

		private void RandomButton_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				Calculation.Clear();
				Calculation.SetRandomCells(ToInt32(randomNumericUpDown.Value));
				gridControl.Refresh();
			});
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				Calculation.Clear();
				gridControl.Refresh();
			});
		}

		private void IterationButton_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			  {
				  Calculation.CalculateNextGeneration();
				  gridControl.Refresh();
			  });
		}
	}
}
