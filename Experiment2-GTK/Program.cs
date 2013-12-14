using System;
using Gtk;
using GuiTestLib;

namespace Experiment2GTK
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			MainWindow win = new MainWindow(new GuiTracker("Experiment2", GuiTracker.Framework.Mono, GuiTracker.Toolkit.Gtk));
			win.Show();
			Application.Run();
		}
	}
}
