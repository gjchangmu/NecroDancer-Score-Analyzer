namespace memreader
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
			this.label1 = new System.Windows.Forms.Label();
			this.tbProcess = new System.Windows.Forms.TextBox();
			this.lbBase = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbLength = new System.Windows.Forms.TextBox();
			this.tbBase = new System.Windows.Forms.TextBox();
			this.cbRelative = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(37, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(146, 25);
			this.label1.TabIndex = 0;
			this.label1.Text = "Process Name:";
			// 
			// tbProcess
			// 
			this.tbProcess.Location = new System.Drawing.Point(303, 40);
			this.tbProcess.Name = "tbProcess";
			this.tbProcess.Size = new System.Drawing.Size(201, 29);
			this.tbProcess.TabIndex = 1;
			this.tbProcess.Text = "NecroDancer";
			this.tbProcess.TextChanged += new System.EventHandler(this.tbProcess_TextChanged);
			// 
			// lbBase
			// 
			this.lbBase.AutoSize = true;
			this.lbBase.Location = new System.Drawing.Point(37, 90);
			this.lbBase.Name = "lbBase";
			this.lbBase.Size = new System.Drawing.Size(195, 25);
			this.lbBase.TabIndex = 0;
			this.lbBase.Text = "Base Address (Hex):";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(37, 140);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(217, 25);
			this.label2.TabIndex = 0;
			this.label2.Text = "Address to Read (Hex):";
			// 
			// tbAddress
			// 
			this.tbAddress.Location = new System.Drawing.Point(303, 140);
			this.tbAddress.Name = "tbAddress";
			this.tbAddress.Size = new System.Drawing.Size(201, 29);
			this.tbAddress.TabIndex = 1;
			this.tbAddress.Text = "00000000";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(37, 240);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(204, 25);
			this.label3.TabIndex = 0;
			this.label3.Text = "Length to Read (Hex):";
			// 
			// tbLength
			// 
			this.tbLength.Location = new System.Drawing.Point(303, 240);
			this.tbLength.Name = "tbLength";
			this.tbLength.Size = new System.Drawing.Size(201, 29);
			this.tbLength.TabIndex = 1;
			this.tbLength.Text = "00000000";
			// 
			// tbBase
			// 
			this.tbBase.Enabled = false;
			this.tbBase.Location = new System.Drawing.Point(303, 90);
			this.tbBase.Name = "tbBase";
			this.tbBase.Size = new System.Drawing.Size(201, 29);
			this.tbBase.TabIndex = 1;
			this.tbBase.Text = "00000000";
			// 
			// cbRelative
			// 
			this.cbRelative.AutoSize = true;
			this.cbRelative.Checked = true;
			this.cbRelative.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbRelative.Location = new System.Drawing.Point(303, 182);
			this.cbRelative.Name = "cbRelative";
			this.cbRelative.Size = new System.Drawing.Size(178, 29);
			this.cbRelative.TabIndex = 2;
			this.cbRelative.Text = "Relative to Base";
			this.cbRelative.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(42, 309);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(462, 44);
			this.button1.TabIndex = 3;
			this.button1.Text = "Read the Memory Region and Save to mem.bin";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(42, 374);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(462, 44);
			this.button2.TabIndex = 3;
			this.button2.Text = "Diff with the Last Time and Save to dif.bin";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(560, 443);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cbRelative);
			this.Controls.Add(this.tbLength);
			this.Controls.Add(this.tbBase);
			this.Controls.Add(this.tbAddress);
			this.Controls.Add(this.tbProcess);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lbBase);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "memreader";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbProcess;
		private System.Windows.Forms.Label lbBase;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbAddress;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbLength;
		private System.Windows.Forms.TextBox tbBase;
		private System.Windows.Forms.CheckBox cbRelative;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}

