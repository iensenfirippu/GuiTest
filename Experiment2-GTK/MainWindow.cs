using System;
using System.IO;
using Gtk;
using Gdk;
using GuiTestLib;

public partial class MainWindow: Gtk.Window
{
	private const bool AUTOCLOSE = true;
	private const int COLUMNS = 10;
	private const int ROWS = 20;
	private const int LABELWIDTH = 100;
	private const int BUTTONWIDTH = 150;

	private GuiTracker _gt;
	private int controlsloaded = 0;

	public MainWindow(GuiTracker tracker): base (Gtk.WindowType.Toplevel)
	{
		Build();
		_gt = tracker;

		_gt.Usage.TakeSnapshot("init start");
		DoStuff();
		_gt.Usage.TakeSnapshot("init end");
	}

	private void DoStuff()
	{
		string imagepath = Directory.GetCurrentDirectory().ToString() + "/Resources/";
		
		ScrolledWindow sw = new ScrolledWindow();
		sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
		Fixed fv = new Fixed();
		fv.BorderWidth = 5;
		Table table = new Table(ROWS, COLUMNS, false);

		int i = 0;
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
					//label.ExposeEvent += OnWidgetExposed; // The expose event of the label is never fired
					table.Attach(label, x, x+1, y, y+1, AttachOptions.Fill, AttachOptions.Fill, 5, 0);
				}
				else
				{
					i++;
					string filename = imagepath + i.ToString("000") + ".png";
					Button button = new Button();
					button.Add(ImageButtonContent(filename, _gt.Random.ShortString));
					button.ExposeEvent += OnWidgetExposed;
					table.Attach(button, x, x+1, y, y+1);
				}
			}
		}

		fv.Put(table, 0, 0);
		sw.AddWithViewport(fv);
		this.Add(sw);
		this.Fullscreen();

		this.ShowAll();
	}

	static Widget ImageButtonContent(string filename, string text)
	{
		/* Create box for image and label */
		HBox box = new HBox(false, 0);
		box.BorderWidth =  2;

		/* Now on to the image stuff */
		Gtk.Image image = new Gtk.Image(filename);

		/* Create a label for the button */
		Label label = new Label(text);

		/* Pack the image and label into the box */
		box.PackStart(image, false, false, 3);
		box.PackStart(label, false, false, 3);

		image.Show();
		label.Show();

		return box;
	}

	protected void OnWidgetExposed(object sender, ExposeEventArgs a)
	{
		controlsloaded++;
		if (controlsloaded == 1)
		{
			_gt.Usage.TakeSnapshot("draw start");
		}
		else if (controlsloaded == 100)
		{
			_gt.Usage.TakeSnapshot("draw end");
			_gt.Stop();

			if (AUTOCLOSE)
			{
				Application.Quit();
				a.RetVal = true;
			}
		}
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
