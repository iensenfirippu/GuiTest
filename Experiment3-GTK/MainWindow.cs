using System;
using System.Collections.Generic;
using System.IO;
using Gtk;
using GuiTestLib;

public partial class MainWindow: Gtk.Window
{
	private const bool AUTOCLOSE = false;
	private const int COLUMNS =	2;
	private const int ROWS = 10;
	private const int LABELWIDTH = 300;
	private const int TEXTWIDTH = 300;
	private const int TEXTHEIGHT = 50;

	private int _controlspainted = 0;
	private int _repetitions = 0;
	private int _next = 0;
	private bool _writing = false;
	private List<Label> _labels = new List<Label>();
	private List<Entry> _textboxes = new List<Entry>();
	private string _filedirectory = Directory.GetCurrentDirectory().ToString() + "/Files/";
	
	private GuiTracker _gt;
	
	public MainWindow(GuiTracker gt): base (Gtk.WindowType.Toplevel)
	{
		Build();
		_gt = gt;

		_gt.Usage.TakeSnapshot("init start");
		DoStuff();
		_gt.Usage.TakeSnapshot("init end");
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
					//label.ExposeEvent += OnWidgetExposed; // The expose event of the label is never fired
					table.Attach(label, x, x+1, y, y+1, AttachOptions.Fill, AttachOptions.Fill, 5, 0);
					_labels.Add(label);
				}
				else
				{
					Entry text = new Entry();
					text.WidthRequest = TEXTWIDTH;
					text.HeightRequest = TEXTHEIGHT;
					text.ExposeEvent += OnWidgetExposed;
					text.TextInserted += OnTextInserted;
					table.Attach(text, x, x+1, y, y+1, AttachOptions.Fill, AttachOptions.Fill, 5, 0);
					_textboxes.Add(text);
				}
			}
		}

		fv.Put(table, 0, 0);
		sw.AddWithViewport(fv);
		this.Add(sw);
		this.Maximize();

		this.ShowAll();
	}

	public void OnWidgetExposed(object sender, ExposeEventArgs a)
	{
		_controlspainted++;

		if (_repetitions == 10)
		{
			_repetitions++;
			_gt.Usage.TakeSnapshot("draw end");

			if (AUTOCLOSE)
			{
				_gt.Stop();
				
				Application.Quit();
				a.RetVal = true;
			}
			else
			{
				//Console.WriteLine("finished");
				foreach (Entry text in _textboxes)
				{
					text.ExposeEvent -= OnWidgetExposed;
				}
			}
		}
		else if (_controlspainted == 1)
		{
			_gt.Usage.TakeSnapshot("draw start");
		}
		else if (_controlspainted > 10)
		{
			if (!_writing)
			{
				_textboxes[_next].Text = string.Empty;
				_textboxes[_next].InsertText(_gt.Random.LongString);
			}
		}
	}

	public void OnTextInserted(object sender, EventArgs args)
	{
		_writing = true;
		Entry Sender = (Entry)sender;
		//string oldcontent = string.Empty;

		if (Sender.Text != string.Empty)
		{
			int index = _textboxes.IndexOf(Sender);

			string filepath = _filedirectory + "file" + (index + 1).ToString("00") + ".txt";
			_labels[_next].Text = File.ReadAllText(filepath);

			File.Delete(filepath);
			File.WriteAllText(filepath, Sender.Text);

			if (index == 9)
			{
				_next = 0;
				_repetitions++;
			}
			else
			{
				_next++;
			}
		}
		_writing = false;
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
