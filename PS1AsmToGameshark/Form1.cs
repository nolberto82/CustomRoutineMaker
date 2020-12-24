using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS1AsmToGameshark
{
	public partial class Form1 : Form
	{
		string asm_filename;
		public Form1()
		{
			InitializeComponent();

			CheckForButtonState();

			UpdateStatusBar();
		}

		private void CheckForButtonState()
		{
			if (textAsm.Text == string.Empty)
			{
				btnAsm.Enabled = false;
			}
			else
			{
				btnAsm.Enabled = true;
			}
		}
		private void btnOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "ASM Files (*.asm)|*.asm";
			ofd.InitialDirectory = Environment.CurrentDirectory;

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				asm_filename = ofd.FileName;
				textAsm.Text = File.ReadAllText(ofd.FileName);
			}
		}

		private void btnAsm_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "ASM Files (*.asm)|*.asm";
			sfd.InitialDirectory = Environment.CurrentDirectory;

			if (asm_filename == "" || asm_filename == null)
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					asm_filename = sfd.FileName;
					File.WriteAllText(sfd.FileName, textAsm.Text);
				}
			}
			else
			{
				File.WriteAllText(asm_filename, textAsm.Text);
			}
		}

		private void textAsm_TextChanged(object sender, EventArgs e)
		{
			CheckForButtonState();
			UpdateStatusBar();
		}

		private void btnAssemble_Click(object sender, EventArgs e)
		{
			if (textAsm.Text != "")
			{
				ProcessStartInfo app = new ProcessStartInfo();
				app.WorkingDirectory = Environment.CurrentDirectory;
				app.FileName = "armips.exe";
				app.Arguments = asm_filename;
				app.UseShellExecute = false;
				app.RedirectStandardOutput = true;

				using (Process process = Process.Start(app))
				{
					using (StreamReader sr = process.StandardOutput)
					{
						textGS.Text = sr.ReadToEnd();
					}

					if (textGS.Text == "")
					{
						StringBuilder sb = new StringBuilder();		
						byte[] temp = File.ReadAllBytes("out.bin");
						uint addr = Convert.ToUInt32(textAddress.Text);

						for (int i = 0; i < temp.Length / 4; i++)
						{
							int[] number = new int[1];
							Buffer.BlockCopy(temp, i * 4, number, 0, 4);
							sb.AppendLine((addr + 0).ToString() + " " + (number[0] & 0xffff).ToString("X4"));
							sb.AppendLine((addr + 2).ToString() + " " + ((number[0] & 0xffff0000) >> 16).ToString("X4"));
							addr += 4;
						}
						textGS.Text = sb.ToString();
					}
				}
			}
			else
			{
				MessageBox.Show("No ASM File loaded", "Error");
			}
		}

		private void textAsm_Click(object sender, EventArgs e)
		{
			UpdateStatusBar();
		}

		private void UpdateStatusBar()
		{
			statusAsm.Text = "Line Number: " + textAsm.GetLineFromCharIndex(textAsm.SelectionStart).ToString();
			statusBar.Invalidate();
			statusBar.Refresh();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(".psx");
			sb.AppendLine(@".create ""out.bin"", 0x80000000");
			sb.AppendLine("\n");
			sb.AppendLine(".close");
			textAsm.Text = sb.ToString();
			asm_filename = "";
		}
	}
}
