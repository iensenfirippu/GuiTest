using System;
using Gtk;
using GuiTestLib;

namespace Experiment3GTK
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			MainWindow win = new MainWindow(new GuiTracker("Experiment3", GuiTracker.Framework.Mono, GuiTracker.Toolkit.Gtk));
			win.Show();
			Application.Run();
		}
	}
}
