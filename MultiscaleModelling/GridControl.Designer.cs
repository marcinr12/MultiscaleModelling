namespace MultiscaleModelling
{
	partial class GridControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.outputPictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// outputPictureBox
			// 
			this.outputPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputPictureBox.Location = new System.Drawing.Point(0, 0);
			this.outputPictureBox.Name = "outputPictureBox";
			this.outputPictureBox.Size = new System.Drawing.Size(312, 304);
			this.outputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.outputPictureBox.TabIndex = 0;
			this.outputPictureBox.TabStop = false;
			this.outputPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputPictureBox_MouseClick);
			// 
			// GridControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.outputPictureBox);
			this.Name = "GridControl";
			this.Size = new System.Drawing.Size(312, 304);
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox outputPictureBox;
	}
}
