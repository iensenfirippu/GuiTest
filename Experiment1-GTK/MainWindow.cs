using System;
using Gtk;
using GuiTestLib;

public partial class MainWindow: Gtk.Window
{
	private const int COLUMNS = 10;
	private const int ROWS = 20;
	private const int LABELWIDTH = 50;
	private const int ENTRYWIDTH = 50;
	
	private GuiTracker _gt = new GuiTracker("Experiment1", GuiTracker.Toolkit.MonoGtk);
	private int controlsloaded = 0;
	
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
		fv.BorderWidth = 5;
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
					//label.ExposeEvent += OnWidgetExposed;
					table.Attach(label, x, x+1, y, y+1, AttachOptions.Fill, AttachOptions.Fill, 5, 0);
				}
				else
				{
					Entry entry = new Entry(_gt.Random.ShortString);
					entry.WidthRequest = ENTRYWIDTH;
					entry.ExposeEvent += OnWidgetExposed;
					table.Attach(entry, x, x+1, y, y+1);
				}
			}
		}
		
		fv.Put(table, 0, 0);
		sw.AddWithViewport(fv);
		this.Add(sw);

		this.ShowAll();
	}
	
	protected void OnWidgetExposed(object sender, ExposeEventArgs a)
	{
		controlsloaded++;
		if (controlsloaded == 100)
		{
			_gt.Stop();
			//Console.WriteLine("file saved");
			this.OnDeleteEvent(this, new DeleteEventArgs());
		}
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
