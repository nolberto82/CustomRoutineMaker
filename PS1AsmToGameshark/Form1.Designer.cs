
namespace PS1AsmToGameshark
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
			this.textGS = new System.Windows.Forms.TextBox();
			this.btnAsm = new System.Windows.Forms.Button();
			this.btnAssemble = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.textAsm = new System.Windows.Forms.TextBox();
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.statusAsm = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnNew = new System.Windows.Forms.Button();
			this.textAddress = new System.Windows.Forms.TextBox();
			this.labelAddress = new System.Windows.Forms.Label();
			this.statusBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// textGS
			// 
			this.textGS.Location = new System.Drawing.Point(561, 105);
			this.textGS.Margin = new System.Windows.Forms.Padding(5);
			this.textGS.Multiline = true;
			this.textGS.Name = "textGS";
			this.textGS.Size = new System.Drawing.Size(534, 669);
			this.textGS.TabIndex = 1;
			// 
			// btnAsm
			// 
			this.btnAsm.Location = new System.Drawing.Point(561, 56);
			this.btnAsm.Margin = new System.Windows.Forms.Padding(5);
			this.btnAsm.Name = "btnAsm";
			this.btnAsm.Size = new System.Drawing.Size(263, 39);
			this.btnAsm.TabIndex = 2;
			this.btnAsm.Text = "Save Asm";
			this.btnAsm.UseVisualStyleBackColor = true;
			this.btnAsm.Click += new System.EventHandler(this.btnAsm_Click);
			// 
			// btnAssemble
			// 
			this.btnAssemble.Location = new System.Drawing.Point(834, 56);
			this.btnAssemble.Margin = new System.Windows.Forms.Padding(5);
			this.btnAssemble.Name = "btnAssemble";
			this.btnAssemble.Size = new System.Drawing.Size(263, 39);
			this.btnAssemble.TabIndex = 3;
			this.btnAssemble.Text = "Assemble";
			this.btnAssemble.UseVisualStyleBackColor = true;
			this.btnAssemble.Click += new System.EventHandler(this.btnAssemble_Click);
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(287, 56);
			this.btnOpen.Margin = new System.Windows.Forms.Padding(5);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(263, 39);
			this.btnOpen.TabIndex = 4;
			this.btnOpen.Text = "Open Asm";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// textAsm
			// 
			this.textAsm.AcceptsTab = true;
			this.textAsm.Location = new System.Drawing.Point(14, 105);
			this.textAsm.Margin = new System.Windows.Forms.Padding(5);
			this.textAsm.Multiline = true;
			this.textAsm.Name = "textAsm";
			this.textAsm.Size = new System.Drawing.Size(534, 669);
			this.textAsm.TabIndex = 5;
			this.textAsm.Click += new System.EventHandler(this.textAsm_Click);
			this.textAsm.TextChanged += new System.EventHandler(this.textAsm_TextChanged);
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusAsm});
			this.statusBar.Location = new System.Drawing.Point(0, 795);
			this.statusBar.Name = "statusBar";
			this.statusBar.Padding = new System.Windows.Forms.Padding(2, 0, 23, 0);
			this.statusBar.Size = new System.Drawing.Size(1110, 22);
			this.statusBar.TabIndex = 6;
			this.statusBar.Text = "statusStrip1";
			// 
			// statusAsm
			// 
			this.statusAsm.Name = "statusAsm";
			this.statusAsm.Size = new System.Drawing.Size(82, 17);
			this.statusAsm.Text = "Line Number: ";
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(14, 56);
			this.btnNew.Margin = new System.Windows.Forms.Padding(5);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(263, 39);
			this.btnNew.TabIndex = 7;
			this.btnNew.Text = "New File";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// textAddress
			// 
			this.textAddress.Location = new System.Drawing.Point(561, 19);
			this.textAddress.Margin = new System.Windows.Forms.Padding(5);
			this.textAddress.Name = "textAddress";
			this.textAddress.Size = new System.Drawing.Size(534, 30);
			this.textAddress.TabIndex = 8;
			this.textAddress.Text = "80000000";
			this.textAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelAddress
			// 
			this.labelAddress.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelAddress.Location = new System.Drawing.Point(14, 19);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(534, 30);
			this.labelAddress.TabIndex = 9;
			this.labelAddress.Text = "Set Address For The Custom Routine On The Textbox";
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1110, 817);
			this.Controls.Add(this.labelAddress);
			this.Controls.Add(this.textAddress);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.textAsm);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.btnAssemble);
			this.Controls.Add(this.btnAsm);
			this.Controls.Add(this.textGS);
			this.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(5);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "PS1 Mips Asm To Gameshark Converter";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox textGS;
		private System.Windows.Forms.Button btnAsm;
		private System.Windows.Forms.Button btnAssemble;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.TextBox textAsm;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.ToolStripStatusLabel statusAsm;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.TextBox textAddress;
		private System.Windows.Forms.Label labelAddress;
	}
}

