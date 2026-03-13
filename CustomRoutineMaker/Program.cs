using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomRoutineMaker
{
    internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		///
		[SupportedOSPlatform("windows")]
		[STAThread]
        private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
