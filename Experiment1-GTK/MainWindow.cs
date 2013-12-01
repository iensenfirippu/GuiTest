using System;
using Gtk;
using GuiTestLib;

public partial class MainWindow: Gtk.Window
{
	private const int COLUMNS = 70; //10;
	private const int ROWS = 150; //20;
	private const int LABELWIDTH = 100;
	private const int ENTRYWIDTH = 100;
	
	private GuiTracker _gt = new GuiTracker("Experiment1-GTK");
	
	public MainWindow(): base (Gtk.WindowType.Toplevel)
	{
		Build();
		
		DoStuff();
	}
	
	private void DoStuff()
	{
		ScrolledWindow sw = new ScrolledWindow();
		sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
		Fixed fv = new Fixed();
		Table table = new Table(ROWS, COLUMNS, false);
		
		for (uint y = 0; y < ROWS; y++)
		{
			for (uint x = 0; x < COLUMNS; x++)
			{
				if (x % 2 == 0)
				{
					Label label = new Label(_gt.Random.ShortString);
					label.WidthRequest = LABELWIDTH;
					label.SetAlignment(1f, 0.5f); 
					label.Justify = Justification.Right;
					table.Attach(label, x, x+1, y, y+1, AttachOptions.Fill, AttachOptions.Fill, 5, 0);
				}
				else
				{
					Entry entry = new Entry(_gt.Random.ShortString);
					entry.WidthRequest = ENTRYWIDTH;
					table.Attach(entry, x, x+1, y, y+1);
				}
			}
		}
		
		fv.Put(table, 0, 0);
		sw.AddWithViewport(fv);
		this.Add(sw);

		this.ShowAll();
		
		_gt.Stop();
		Console.WriteLine("Executed in: {0}", _gt.ExecutionTime.ToString());
		Console.WriteLine("CPU Usage: {0}", (_gt.Usage.Cpu / System.Environment.ProcessorCount).ToString());
		Console.WriteLine("RAM Usage: {0}", _gt.Usage.Ram.ToString());
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
