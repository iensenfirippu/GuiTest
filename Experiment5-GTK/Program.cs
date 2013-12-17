using System;
using Gtk;
using GuiTestLib;

namespace Experiment5GTK
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			MainWindow win = new MainWindow(new GuiTracker("Experiment5", GuiTracker.Framework.Mono, GuiTracker.Toolkit.Gtk));
			win.Show();
			Application.Run();
		}
	}
}
