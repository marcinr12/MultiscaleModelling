using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Convert;

namespace MultiscaleModelling
{
	public partial class Form1 : Form
	{
		public CancellationTokenSource SimulationCancellationTokenSource { get; private set; } = new CancellationTokenSource();
		public Form1()
		{
			InitializeComponent();
			SizeXNumericUpDown_Leave(null, null);
			SizeYNumericUpDown_Leave(null, null);

			SizeXNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			SizeYNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			randomNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			inclusionsNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			radiusNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;

			exportTextToolStripMenuItem.Click += ExportTextToolStripMenuItem_Click;
			importTextToolStripMenuItem.Click += ImportTextToolStripMenuItem_Click;

			exportBmpToolStripMenuItem.Click += ExportBmpToolStripMenuItem_Click;
			importBmpToolStripMenuItem.Click += ImportBmpToolStripMenuItem_Click;

			gridControl.Matrix.SetRandomCells(10);

			bcComboBox.Items.AddRange(EnumsNames.BcNames.Values.ToArray());
			bcComboBox.SelectedItem = EnumsNames.BcNames[Bc.Absorbing];
		}
		private void ImportBmpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "BMP files|*.bmp"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Bitmap bitmap = new Bitmap(openFileDialog.FileName);
					gridControl.LoadMatrix(bitmap);
					SizeYNumericUpDown.Value = gridControl.Matrix.RowsCount;
					SizeXNumericUpDown.Value = gridControl.Matrix.ColumnsCount;
					gridControl.Draw();
				}
				catch (Exception exception)
				{
					Trace.WriteLine("ImportBmpToolStripMenuItem_Click" + exception.Message);
				}
			}
		}
		private void ExportBmpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "BMP files (*.bmp)|*.bmp|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = true;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
				gridControl.Matrix.ToBitmap().Save(saveFileDialog.FileName, ImageFormat.Bmp);
		}
		private void ImportTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "TXT files|*.txt"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				List<(int Id, int Phase, int IndexX, int IndexY)> cells = new List<(int Id, int Phase, int IndexX, int IndexY)>();
				try
				{
					using StreamReader sr = new StreamReader(openFileDialog.FileName);
					string line;

					sr.ReadLine(); // skip first txt line
					while ((line = sr.ReadLine()) != null)
					{			
						string[] data = line.Split(" ");
						if (data.Length != 4)
							continue;

						if (!int.TryParse(data[0], out int indexX))
							continue;
						if (!int.TryParse(data[1], out int indexY))
							continue;
						if (!int.TryParse(data[2], out int phase))
							continue;
						if (!int.TryParse(data[3], out int id))
							continue;

						cells.Add((Id: id, Phase: phase, IndexX: indexX, IndexY: indexY));				
					}
					gridControl.LoadMatrix(cells);
					SizeYNumericUpDown.Value = gridControl.Matrix.RowsCount;
					SizeXNumericUpDown.Value = gridControl.Matrix.ColumnsCount;
					gridControl.Draw();
				}
				catch (Exception exception)
				{
					Trace.WriteLine("ImportTextToolStripMenuItem_Click" + exception.Message);
				}
			}
		}
		private void ExportTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog()
			{
				Filter = "TXT files|*.txt"
			};

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
				using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
					sw.Write(gridControl.Matrix.ToString());
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
		private void SizeXNumericUpDown_Leave(object sender, EventArgs e)
		{
			gridControl.GridCellWidth = ToInt32(SizeXNumericUpDown.Value);
		}
		private void SizeYNumericUpDown_Leave(object sender, EventArgs e)
		{
			gridControl.GridCellHeight = ToInt32(SizeYNumericUpDown.Value);
		}
		private void GridCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			gridControl.IsGridShowed = GridCheckBox.Checked;
		}
		private void RandomButton_Click(object sender, EventArgs e)
		{
			randomButton.Enabled = false;
			clearButton.Enabled = false;
			iterationButton.Enabled = false;
			startButton.Enabled = false;

			Task.Run(() =>
			{
				try
				{
					gridControl.Matrix.SetRandomCells(ToInt32(randomNumericUpDown.Value));
					gridControl.Draw();
				}
				finally
				{
					randomButton.Invoke(new Action(() =>
					{
						randomButton.Enabled = true;
						clearButton.Enabled = true;
						iterationButton.Enabled = true;
						startButton.Enabled = true;
					}));
				}
			});
			
		}
		private void ClearButton_Click(object sender, EventArgs e)
		{
			randomButton.Enabled = false;
			clearButton.Enabled = false;
			iterationButton.Enabled = false;
			startButton.Enabled = false;

			Task.Run(() =>
			{
				try
				{
					gridControl.Matrix.Erase();
					gridControl.Draw();
				}
				finally
				{
					clearButton.Invoke(new Action(() =>
					{
						randomButton.Enabled = true;
						clearButton.Enabled = true;
						iterationButton.Enabled = true;
						startButton.Enabled = true;
					}));		
				}
			});	
		}
		private void IterationButton_Click(object sender, EventArgs e)
		{
			randomButton.Enabled = false;
			clearButton.Enabled = false;
			iterationButton.Enabled = false;
			startButton.Enabled = false;

			Task.Run(() =>
			{
				try
				{
					gridControl.Matrix.InitialCalculations();
					gridControl.Matrix.CalculateNextGeneration();
					gridControl.Draw();
				}
				finally
				{
					iterationButton.Invoke(new Action(() =>
					{
						randomButton.Enabled = true;
						clearButton.Enabled = true;
						iterationButton.Enabled = true;
						startButton.Enabled = true;
					}));
				}
			});		
		}
		private void StartButton_Click(object sender, EventArgs e)
		{
			randomButton.Enabled = false;
			clearButton.Enabled = false;
			iterationButton.Enabled = false;
			startButton.Enabled = false;
			SimulationCancellationTokenSource = new CancellationTokenSource();

			Task.Run(() =>
			{
				try
				{
					bool prevIsAnimation = animationCheckBox.Checked;
					Stopwatch sw = new Stopwatch();
					sw.Restart();
					if (gridControl.Matrix.GetCells().Where(c => c.Id != 0).FirstOrDefault() is Cell)
					{
						gridControl.Matrix.InitialCalculations();
						while (gridControl.Matrix.GetCells().Where(c => c.Id == 0).FirstOrDefault() is Cell)
						{
							if (SimulationCancellationTokenSource.IsCancellationRequested)
								break;

							LinkedList<Cell> a = gridControl.Matrix.CalculateNextGeneration();
							if (animationCheckBox.Checked && animationCheckBox.Checked == prevIsAnimation)
							{
								gridControl.Draw(a);
							}
							else if (animationCheckBox.Checked)
							{
								gridControl.Draw();
								prevIsAnimation = animationCheckBox.Checked;
							}
							else
								prevIsAnimation = animationCheckBox.Checked;
						}
						Trace.WriteLine($"Simulation took: {sw.ElapsedMilliseconds}ms");
						gridControl.Draw();
					}
				}
				finally
				{
					startButton.Invoke(new Action(() =>
					{
						randomButton.Enabled = true;
						clearButton.Enabled = true;
						iterationButton.Enabled = true;
						startButton.Enabled = true;
					}));
				}
			}, SimulationCancellationTokenSource.Token);
		}
		private void BcComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox comboBox = sender as ComboBox;

			if (comboBox.SelectedItem.ToString() == EnumsNames.BcNames[Bc.Absorbing])
				gridControl.Matrix.BoundaryCondition = Bc.Absorbing;
			else if (comboBox.SelectedItem.ToString() == EnumsNames.BcNames[Bc.Periodic])
				gridControl.Matrix.BoundaryCondition = Bc.Periodic;
		}
		private void AddInclusionsButton_Click(object sender, EventArgs e)
		{
			bool isAnyCellEmpty = gridControl.Matrix.GetCells().Where(c => c.Id == 0).FirstOrDefault() is Cell;
			bool isAnyCellFilled = gridControl.Matrix.GetCells().Where(c => c.Id != 0).FirstOrDefault() is Cell;

			if (!isAnyCellEmpty)
			{
				// Every cell is filled
				gridControl.Matrix.AddInclusions(ToInt32(inclusionsNumericUpDown.Value), ToInt32(radiusNumericUpDown.Value), InclusionsType.OnBorder);
				gridControl.Draw();
			}
			else if(!isAnyCellFilled)
			{
				// Every cell is empty
				gridControl.Matrix.AddInclusions(ToInt32(inclusionsNumericUpDown.Value), ToInt32(radiusNumericUpDown.Value), InclusionsType.Random);
				gridControl.Draw();
			}
			else
			{
				// Grid is partially filled
				Trace.WriteLine("Grid is not filled!");
			}
		}
		private void TerminateButton_Click(object sender, EventArgs e)
		{
			SimulationCancellationTokenSource.Cancel();
		}
	}
}
