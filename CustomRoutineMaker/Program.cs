using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomRoutineMaker
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		///
		[SupportedOSPlatform("windows")]
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
