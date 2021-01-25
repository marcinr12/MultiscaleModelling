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
		private Task _startTask;
		private readonly int cellSizeBmp = 1;
		public Form1()
		{
			InitializeComponent();
			SizeXNumericUpDown_Leave(null, null);
			SizeYNumericUpDown_Leave(null, null);

			substructureRadioButton.Checked = true;

			SizeXNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			SizeYNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			randomNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			inclusionsNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			radiusNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			probabilityNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;
			thicknessNumericUpDown.MouseWheel += NumericUpDown_MouseWheel;

			exportTextToolStripMenuItem.Click += ExportTextToolStripMenuItem_Click;
			importTextToolStripMenuItem.Click += ImportTextToolStripMenuItem_Click;

			exportBmpToolStripMenuItem.Click += ExportBmpToolStripMenuItem_Click;
			importBmpToolStripMenuItem.Click += ImportBmpToolStripMenuItem_Click;

			List<Control> notNumericUpDownControls = GetAllControls(this).Where(x => x.GetType() != typeof(NumericUpDown)).ToList();
			notNumericUpDownControls.ForEach(c =>
			{
				c.Click += SizeXNumericUpDown_Leave;
				c.Click += SizeYNumericUpDown_Leave;
			});

			gridControl.Matrix.SetRandomCells(10);

			bcComboBox.Items.AddRange(EnumsNames.BcNames.Values.ToArray());
			bcComboBox.SelectedItem = EnumsNames.BcNames[Bc.Periodic];

			inclusionTypeComboBox.Items.AddRange(EnumsNames.InclusionsTypeNames.Values.ToArray());
			inclusionTypeComboBox.SelectedItem = EnumsNames.InclusionsTypeNames[InclusionsType.Round];

#if DEBUG
			animationCheckBox.Checked = false;
			StartButton_Click(null, null);
			_startTask.Wait();
			animationCheckBox.Checked = true;
#endif
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
					gridControl.LoadMatrix(bitmap, cellSizeBmp);
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
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				Filter = "BMP files (*.bmp)|*.bmp|All files (*.*)|*.*",
				FilterIndex = 1,
				RestoreDirectory = true
			};

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
				gridControl.Matrix.ToBitmap(cellSizeBmp).Save(saveFileDialog.FileName, ImageFormat.Bmp);
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
				Filter = "TXT files (*.txt)|*.txt|All files (*.*)|*.*",
				FilterIndex = 1,
				RestoreDirectory = true
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
			{
				hme.Handled = true;
			}

			if (e.Delta > 0 && numericUpDown.Value + increment <= numericUpDown.Maximum)
				numericUpDown.Value += increment;
			else if (e.Delta < 0 && numericUpDown.Value - increment >= numericUpDown.Minimum)
				numericUpDown.Value -= increment;
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
						gbLabel.Text = $"GB: ---";
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
					gridControl.Matrix.CalculateNextGeneration(shapeControlCheckBox.Checked, rule1CheckBox.Checked, rule2CheckBox.Checked, rule3CheckBox.Checked, rule4checkBox.Checked, ToInt32(probabilityNumericUpDown.Value));
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

			_startTask = Task.Run(() =>
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

							LinkedList<Cell> a = gridControl.Matrix.CalculateNextGeneration(shapeControlCheckBox.Checked, rule1CheckBox.Checked, rule2CheckBox.Checked, rule3CheckBox.Checked, rule4checkBox.Checked, ToInt32(probabilityNumericUpDown.Value));
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
					if (IsHandleCreated)
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

			if (comboBox.SelectedItem?.ToString() == EnumsNames.BcNames[Bc.Absorbing])
				gridControl.Matrix.BoundaryCondition = Bc.Absorbing;
			else if (comboBox.SelectedItem?.ToString() == EnumsNames.BcNames[Bc.Periodic])
				gridControl.Matrix.BoundaryCondition = Bc.Periodic;
		}
		private void AddInclusionsButton_Click(object sender, EventArgs e)
		{
			bool isAnyCellEmpty = gridControl.Matrix.GetCells().Where(c => c.Id == 0).FirstOrDefault() is Cell;
			bool isAnyCellFilled = gridControl.Matrix.GetCells().Where(c => c.Id != 0).FirstOrDefault() is Cell;

			InclusionsType inclusionsType = InclusionsType.Round;

			if (inclusionTypeComboBox.SelectedItem?.ToString() == EnumsNames.InclusionsTypeNames[InclusionsType.Round])
				inclusionsType = InclusionsType.Round;
			else if (inclusionTypeComboBox.SelectedItem?.ToString() == EnumsNames.InclusionsTypeNames[InclusionsType.Square])
				inclusionsType = InclusionsType.Square;

			if (!isAnyCellEmpty)
			{
				// Every cell is filled
				gridControl.Matrix.AddInclusions(ToInt32(inclusionsNumericUpDown.Value), ToInt32(radiusNumericUpDown.Value), InclusionsMode.Post, inclusionsType);
				gridControl.Draw();
			}
			else if (!isAnyCellFilled)
			{
				// Every cell is empty
				gridControl.Matrix.AddInclusions(ToInt32(inclusionsNumericUpDown.Value), ToInt32(radiusNumericUpDown.Value), InclusionsMode.Pre, inclusionsType);
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
		public IEnumerable<Control> GetAllControls(Control parentControl)
		{
			IEnumerable<Control> controls = parentControl.Controls.Cast<Control>();
			return controls.SelectMany(ctrl => GetAllControls(ctrl))
									  .Concat(controls);
		}
		private void ShapeControlCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			rulesPanel.Enabled = shapeControlCheckBox.Checked;
		}
		private void GridControl_MouseClick(object sender, MouseEventArgs e)
		{
			if (gridControl.Matrix.SelectedCell?.Id is null)
				return;
			int selectedCellId = gridControl.Matrix.SelectedCell.Id;
			Trace.WriteLine($"cell.Id:{selectedCellId}");

			if (pickGbRadioButton.Checked)
			{
				gridControl.Matrix.SetCellsBorders(ToInt32(thicknessNumericUpDown.Value), selectedCellId);
				gridControl.Draw();
			}
			else if (pickDualPhaseRadioButton.Checked)
			{
				gridControl.Matrix.SetDualPhase(selectedCellId);
				gridControl.ViewMode = ViewMode.DualPhase;
			}
		}
		private void ShowGbButton_Click(object sender, EventArgs e)
		{
			if (gridControl.Matrix.GetCells().Where(c => c.Id == 0).FirstOrDefault() is Cell)
			{
				Trace.WriteLine("Grid is not filled: BorderCheckBox_CheckedChanged");
				return;
			}

			int gb = gridControl.Matrix.SetCellsBorders(ToInt32(thicknessNumericUpDown.Value));
			gbLabel.Text = $"GB: {gb / (SizeXNumericUpDown.Value * SizeYNumericUpDown.Value) * 100}%";
			gridControl.Draw();
		}
		private void ClearGbButton_Click(object sender, EventArgs e)
		{
			gridControl.Matrix.ClearCellsBorders();
			gridControl.Draw();
		}
		private void ViewModeRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (!radioButton.Checked)
				return;

			if (ReferenceEquals(sender, substructureRadioButton))
			{
				gridControl.ViewMode = ViewMode.Substracture;
				dualPhasePanel.Enabled = false;
				grainBoundaryPanel.Enabled = false;
			}
			else if (ReferenceEquals(sender, dualPhaseRadioButton))
			{
				gridControl.ViewMode = ViewMode.DualPhase;
				dualPhasePanel.Enabled = true;
				grainBoundaryPanel.Enabled = false;
			}
		}
		private void GrainBoundariesCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			grainBoundaryPanel.Enabled = grainBoundariesCheckBox.Checked;
			gridControl.ShowGrainBoundaries = grainBoundariesCheckBox.Checked;
		}
		private void ClearPhaseButton_Click(object sender, EventArgs e)
		{
			gridControl.Matrix.ClearDualPhase();
			gridControl.ViewMode = ViewMode.DualPhase;
		}
		private void PickGbRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (pickGbRadioButton.Checked)
				grainBoundariesCheckBox.Checked = true;
		}
		private void PickDualPhaseRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (pickDualPhaseRadioButton.Checked)
				dualPhaseRadioButton.Checked = true;
		}
		private void SecondGrowthButton_Click(object sender, EventArgs e)
		{
			randomButton.Enabled = false;
			clearButton.Enabled = false;
			iterationButton.Enabled = false;
			startButton.Enabled = false;
			SimulationCancellationTokenSource = new CancellationTokenSource();

			_startTask = Task.Run(() =>
			{
				try
				{
					bool prevIsAnimation = animationCheckBox.Checked;
					Stopwatch sw = new Stopwatch();
					sw.Restart();
					if (gridControl.Matrix.GetCells().Where(c => c.Id != 0).FirstOrDefault() is Cell)
					{
						gridControl.Matrix.AssignFirstPhaseGrains();
						gridControl.Matrix.SetRandomCellsSecondGrainGrowth(ToInt32(randomNumericUpDown.Value));
						gridControl.Matrix.InitialSecondGrowthCalculations();
						gridControl.Draw();

						while (gridControl.Matrix.GetCells().Where(c => c.Id == 0).FirstOrDefault() is Cell)
						{
							if (SimulationCancellationTokenSource.IsCancellationRequested)
								break;

							LinkedList<Cell> a = gridControl.Matrix.CalculateNextGenerationSecondGrowth(shapeControlCheckBox.Checked, rule1CheckBox.Checked, rule2CheckBox.Checked, rule3CheckBox.Checked, rule4checkBox.Checked, ToInt32(probabilityNumericUpDown.Value));
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
					if (IsHandleCreated)
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
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			randomButton.Enabled = true;
			clearButton.Enabled = true;
			iterationButton.Enabled = true;
			startButton.Enabled = true;
		}
	}
}
