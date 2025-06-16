
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
            btnAsm = new System.Windows.Forms.Button();
            btnAssemble = new System.Windows.Forms.Button();
            btnOpen = new System.Windows.Forms.Button();
            textAsm = new System.Windows.Forms.TextBox();
            statusBar = new System.Windows.Forms.StatusStrip();
            statusAsm = new System.Windows.Forms.ToolStripStatusLabel();
            btnNew = new System.Windows.Forms.Button();
            comboBox1 = new System.Windows.Forms.ComboBox();
            textPS2 = new System.Windows.Forms.TextBox();
            textPnach = new System.Windows.Forms.TextBox();
            btnConvert = new System.Windows.Forms.Button();
            comboBox2 = new System.Windows.Forms.ComboBox();
            textGS = new System.Windows.Forms.TextBox();
            textInput = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            textResult = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textResult2 = new System.Windows.Forms.TextBox();
            statusBar.SuspendLayout();
            SuspendLayout();
            // 
            // btnAsm
            // 
            btnAsm.Location = new System.Drawing.Point(427, 38);
            btnAsm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnAsm.Name = "btnAsm";
            btnAsm.Size = new System.Drawing.Size(190, 32);
            btnAsm.TabIndex = 2;
            btnAsm.Text = "Save Asm";
            btnAsm.UseVisualStyleBackColor = true;
            // 
            // btnAssemble
            // 
            btnAssemble.Location = new System.Drawing.Point(644, 38);
            btnAssemble.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnAssemble.Name = "btnAssemble";
            btnAssemble.Size = new System.Drawing.Size(190, 32);
            btnAssemble.TabIndex = 3;
            btnAssemble.Text = "Assemble";
            btnAssemble.UseVisualStyleBackColor = true;
            btnAssemble.Click += btnAssemble_Click;
            // 
            // btnOpen
            // 
            btnOpen.Location = new System.Drawing.Point(241, 39);
            btnOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new System.Drawing.Size(180, 32);
            btnOpen.TabIndex = 4;
            btnOpen.Text = "Open Asm";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // textAsm
            // 
            textAsm.AcceptsTab = true;
            textAsm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textAsm.Font = new System.Drawing.Font("Consolas", 11.25F);
            textAsm.Location = new System.Drawing.Point(11, 79);
            textAsm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            textAsm.Multiline = true;
            textAsm.Name = "textAsm";
            textAsm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textAsm.Size = new System.Drawing.Size(410, 656);
            textAsm.TabIndex = 5;
            textAsm.Click += textAsm_Click;
            textAsm.TextChanged += textAsm_TextChanged;
            // 
            // statusBar
            // 
            statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { statusAsm });
            statusBar.Location = new System.Drawing.Point(0, 739);
            statusBar.Name = "statusBar";
            statusBar.Padding = new System.Windows.Forms.Padding(2, 0, 18, 0);
            statusBar.Size = new System.Drawing.Size(1184, 22);
            statusBar.TabIndex = 6;
            statusBar.Text = "statusStrip1";
            // 
            // statusAsm
            // 
            statusAsm.Name = "statusAsm";
            statusAsm.Size = new System.Drawing.Size(82, 17);
            statusAsm.Text = "Line Number: ";
            // 
            // btnNew
            // 
            btnNew.Location = new System.Drawing.Point(11, 38);
            btnNew.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnNew.Name = "btnNew";
            btnNew.Size = new System.Drawing.Size(180, 32);
            btnNew.TabIndex = 7;
            btnNew.Text = "New File";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(14, 10);
            comboBox1.Margin = new System.Windows.Forms.Padding(2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(407, 26);
            comboBox1.TabIndex = 10;
            // 
            // textPS2
            // 
            textPS2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textPS2.Font = new System.Drawing.Font("Consolas", 11.25F);
            textPS2.Location = new System.Drawing.Point(840, 79);
            textPS2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            textPS2.Multiline = true;
            textPS2.Name = "textPS2";
            textPS2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textPS2.Size = new System.Drawing.Size(332, 320);
            textPS2.TabIndex = 11;
            // 
            // textPnach
            // 
            textPnach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textPnach.Font = new System.Drawing.Font("Consolas", 11.25F);
            textPnach.Location = new System.Drawing.Point(840, 415);
            textPnach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            textPnach.Multiline = true;
            textPnach.Name = "textPnach";
            textPnach.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textPnach.Size = new System.Drawing.Size(332, 320);
            textPnach.TabIndex = 12;
            // 
            // btnConvert
            // 
            btnConvert.Location = new System.Drawing.Point(840, 39);
            btnConvert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new System.Drawing.Size(332, 32);
            btnConvert.TabIndex = 13;
            btnConvert.Text = "Convert";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // comboBox2
            // 
            comboBox2.Font = new System.Drawing.Font("Consolas", 11.25F);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(840, 11);
            comboBox2.Margin = new System.Windows.Forms.Padding(2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(332, 26);
            comboBox2.TabIndex = 14;
            // 
            // textGS
            // 
            textGS.Font = new System.Drawing.Font("Consolas", 9.75F);
            textGS.Location = new System.Drawing.Point(427, 80);
            textGS.Multiline = true;
            textGS.Name = "textGS";
            textGS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textGS.Size = new System.Drawing.Size(407, 609);
            textGS.TabIndex = 15;
            // 
            // textInput
            // 
            textInput.Font = new System.Drawing.Font("Consolas", 9.75F);
            textInput.Location = new System.Drawing.Point(427, 712);
            textInput.Name = "textInput";
            textInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textInput.Size = new System.Drawing.Size(126, 23);
            textInput.TabIndex = 16;
            textInput.TextChanged += textInput_TextChanged;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(427, 692);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(126, 15);
            label1.TabIndex = 17;
            label1.Text = "Address";
            // 
            // textResult
            // 
            textResult.Font = new System.Drawing.Font("Consolas", 9.75F);
            textResult.Location = new System.Drawing.Point(559, 712);
            textResult.Name = "textResult";
            textResult.ReadOnly = true;
            textResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textResult.Size = new System.Drawing.Size(126, 23);
            textResult.TabIndex = 18;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(559, 694);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(126, 15);
            label2.TabIndex = 19;
            label2.Text = "Reversed";
            // 
            // textResult2
            // 
            textResult2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            textResult2.Location = new System.Drawing.Point(691, 711);
            textResult2.Name = "textResult2";
            textResult2.ReadOnly = true;
            textResult2.Size = new System.Drawing.Size(128, 23);
            textResult2.TabIndex = 20;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1184, 761);
            Controls.Add(textResult2);
            Controls.Add(label2);
            Controls.Add(textResult);
            Controls.Add(label1);
            Controls.Add(textInput);
            Controls.Add(textGS);
            Controls.Add(comboBox2);
            Controls.Add(btnConvert);
            Controls.Add(textPnach);
            Controls.Add(textPS2);
            Controls.Add(comboBox1);
            Controls.Add(btnNew);
            Controls.Add(statusBar);
            Controls.Add(textAsm);
            Controls.Add(btnOpen);
            Controls.Add(btnAssemble);
            Controls.Add(btnAsm);
            Font = new System.Drawing.Font("Consolas", 11.25F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Custom Routine Creator";
            Load += Form1_Load;
            statusBar.ResumeLayout(false);
            statusBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
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
        private System.Windows.Forms.TextBox textGS;
        private System.Windows.Forms.TextBox textInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textResult2;
    }
}

