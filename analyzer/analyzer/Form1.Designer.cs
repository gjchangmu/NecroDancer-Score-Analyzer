namespace WindowsFormsApplication1
{
	partial class Form1
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
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.lbStatus = new System.Windows.Forms.Label();
			this.cbRandom = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// lbStatus
			// 
			this.lbStatus.AutoSize = true;
			this.lbStatus.Font = new System.Drawing.Font("SimSun", 12F);
			this.lbStatus.ForeColor = System.Drawing.Color.MediumBlue;
			this.lbStatus.Location = new System.Drawing.Point(23, 60);
			this.lbStatus.Name = "lbStatus";
			this.lbStatus.Size = new System.Drawing.Size(224, 16);
			this.lbStatus.TabIndex = 0;
			this.lbStatus.Text = "Waiting for game process...";
			// 
			// cbRandom
			// 
			this.cbRandom.AutoSize = true;
			this.cbRandom.Font = new System.Drawing.Font("SimSun", 12F);
			this.cbRandom.Location = new System.Drawing.Point(26, 22);
			this.cbRandom.Name = "cbRandom";
			this.cbRandom.Size = new System.Drawing.Size(155, 20);
			this.cbRandom.TabIndex = 1;
			this.cbRandom.Text = "24 Random levels";
			this.cbRandom.UseVisualStyleBackColor = true;
			this.cbRandom.Visible = false;
			this.cbRandom.CheckedChanged += new System.EventHandler(this.cbRandom_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(446, 99);
			this.Controls.Add(this.cbRandom);
			this.Controls.Add(this.lbStatus);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Form1";
			this.Text = "Spelunky MEP Mod v0.1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label lbStatus;
		private System.Windows.Forms.CheckBox cbRandom;
	}
}

