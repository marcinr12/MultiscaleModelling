using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Convert;

namespace MultiscaleModelling
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			SizeXNumericUpDown.Value = 20;
			SizeYNumericUpDown.Value = 20;
			SizeXNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			SizeYNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;

			gridControl.Matrix.SetRandomCells(10);

			bcComboBox.Items.AddRange(EnumsNames.BcNames.Values.ToArray());
			bcComboBox.SelectedItem = EnumsNames.BcNames[Bc.Absorbing];
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
			gridControl.GridCellWidth = ToInt32(SizeXNumericUpDown.Value);
		}

		private void SizeYNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			gridControl.GridCellHeight = ToInt32(SizeYNumericUpDown.Value);
		}

		private void GridCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			gridControl.IsGridShowed = GridCheckBox.Checked;
		}

		private void RandomButton_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				gridControl.Matrix.Erase();
				gridControl.Matrix.SetRandomCells(ToInt32(randomNumericUpDown.Value));
				gridControl.Draw();
			});
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				gridControl.Matrix.Erase();
				gridControl.Draw();
			});
		}

		private void IterationButton_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				gridControl.Matrix.CalculateNextGeneration();
				gridControl.Draw();
			});
		}

		private void StartButton_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				while(gridControl.Matrix.GetCells().Where(c => c.Id == 0).FirstOrDefault() is Cell)
				{
					gridControl.Matrix.CalculateNextGeneration();
					if(animationCheckBox.Checked)
						gridControl.Draw();
				}
				gridControl.Draw();
			});
		}

		private void BcComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;

			if (comboBox.SelectedItem.ToString() == EnumsNames.BcNames[Bc.Absorbing])
				gridControl.Matrix.BoundaryCondition = Bc.Absorbing;
			else if (comboBox.SelectedItem.ToString() == EnumsNames.BcNames[Bc.Periodic])
				gridControl.Matrix.BoundaryCondition = Bc.Periodic;
		}
	}
}
