
namespace CustomRoutineMaker
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private global::System.ComponentModel.IContainer components = null;

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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textPS2 = new System.Windows.Forms.TextBox();
            this.textPnach = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // textGS
            // 
            this.textGS.Location = new System.Drawing.Point(383, 79);
            this.textGS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textGS.Multiline = true;
            this.textGS.Name = "textGS";
            this.textGS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textGS.Size = new System.Drawing.Size(411, 530);
            this.textGS.TabIndex = 1;
            // 
            // btnAsm
            // 
            this.btnAsm.Location = new System.Drawing.Point(383, 38);
            this.btnAsm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAsm.Name = "btnAsm";
            this.btnAsm.Size = new System.Drawing.Size(190, 32);
            this.btnAsm.TabIndex = 2;
            this.btnAsm.Text = "Save Asm";
            this.btnAsm.UseVisualStyleBackColor = true;
            // 
            // btnAssemble
            // 
            this.btnAssemble.Location = new System.Drawing.Point(604, 38);
            this.btnAssemble.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAssemble.Name = "btnAssemble";
            this.btnAssemble.Size = new System.Drawing.Size(190, 32);
            this.btnAssemble.TabIndex = 3;
            this.btnAssemble.Text = "Assemble";
            this.btnAssemble.UseVisualStyleBackColor = true;
            this.btnAssemble.Click += new System.EventHandler(this.btnAssemble_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(197, 38);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(180, 32);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "Open Asm";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // textAsm
            // 
            this.textAsm.AcceptsTab = true;
            this.textAsm.Location = new System.Drawing.Point(11, 79);
            this.textAsm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textAsm.Multiline = true;
            this.textAsm.Name = "textAsm";
            this.textAsm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textAsm.Size = new System.Drawing.Size(366, 530);
            this.textAsm.TabIndex = 5;
            this.textAsm.Click += new System.EventHandler(this.textAsm_Click);
            this.textAsm.TextChanged += new System.EventHandler(this.textAsm_TextChanged);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusAsm});
            this.statusBar.Location = new System.Drawing.Point(0, 610);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(2, 0, 18, 0);
            this.statusBar.Size = new System.Drawing.Size(1184, 22);
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
            this.btnNew.Location = new System.Drawing.Point(11, 38);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(180, 32);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "New File";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(14, 10);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(363, 26);
            this.comboBox1.TabIndex = 10;
            // 
            // textPS2
            // 
            this.textPS2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPS2.Location = new System.Drawing.Point(800, 79);
            this.textPS2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textPS2.Multiline = true;
            this.textPS2.Name = "textPS2";
            this.textPS2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textPS2.Size = new System.Drawing.Size(372, 274);
            this.textPS2.TabIndex = 11;
            // 
            // textPnach
            // 
            this.textPnach.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPnach.Location = new System.Drawing.Point(800, 357);
            this.textPnach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textPnach.Multiline = true;
            this.textPnach.Name = "textPnach";
            this.textPnach.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textPnach.Size = new System.Drawing.Size(372, 252);
            this.textPnach.TabIndex = 12;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(800, 39);
            this.btnConvert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(372, 32);
            this.btnConvert.TabIndex = 13;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(800, 11);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(372, 26);
            this.comboBox2.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 632);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.textPnach);
            this.Controls.Add(this.textPS2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.textAsm);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnAssemble);
            this.Controls.Add(this.btnAsm);
            this.Controls.Add(this.textGS);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "CustomRoutineMaker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private global::System.Windows.Forms.TextBox textGS;
		private global::System.Windows.Forms.Button btnAsm;
		private global::System.Windows.Forms.Button btnAssemble;
		private global::System.Windows.Forms.Button btnOpen;
		private global::System.Windows.Forms.TextBox textAsm;
		private global::System.Windows.Forms.StatusStrip statusBar;
		private global::System.Windows.Forms.ToolStripStatusLabel statusAsm;
		private global::System.Windows.Forms.Button btnNew;
		private global::System.Windows.Forms.ComboBox comboBox1;
        private global::System.Windows.Forms.TextBox textPS2;
        private global::System.Windows.Forms.TextBox textPnach;
        private global::System.Windows.Forms.Button btnConvert;
        private global::System.Windows.Forms.ComboBox comboBox2;
    }
}

