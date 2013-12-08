using System;
using Gtk;
using GuiTestLib;

namespace Experiment1GTK
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			MainWindow win = new MainWindow(new GuiTracker("Experiment1", GuiTracker.Framework.Mono, GuiTracker.Toolkit.Gtk));
			win.Show();
			Application.Run();
		}
	}
}
