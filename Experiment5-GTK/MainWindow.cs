using System;
using System.Collections.Generic;
using System.IO;
using Gtk;
using GuiTestLib;

public partial class MainWindow: Gtk.Window
{
	private const bool AUTOCLOSE = true;
	private const int COLUMNS = 2;
	private const int ROWS = 10;
	private const int LABELWIDTH = 100;
	private const int TEXTWIDTH = 150;

	private string _imagepath = Directory.GetCurrentDirectory().ToString() + "/Resources/";

	private List<Label> _labels = new List<Label>();
	private List<Entry> _textboxes = new List<Entry>();
	private List<Button> _buttons = new List<Button>();
	//private Notebook _notebook = new Notebook();

	private int _tabnumber = 0;
	private int _controlspainted = 0;
	private int _imageindex = 0;

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
		foreach (MenuItem item in menubar1.Children)
		{
			item.Action.Label = _gt.Random.ShortString;

			for (int x = 0; x < 5; x++)
			{
				//_imageindex++;
				//if (_imageindex > 100) { _imageindex = 1; }
				//Image image = new Image(_imagepath + _imageindex.ToString("000") + ".png");
				string label = _gt.Random.ShortString;

				Gtk.Action subitem = new Gtk.Action(label, label, label, "gtk_save");
				//subitem.IconName = _imagepath + _imageindex.ToString("000") + ".png";
				//subitem.CreateIcon(Gtk.IconSize.Menu);
				//subitem.ExposeEvent += OnMenuExposed;

				item.Action.ActionGroup.Add(subitem);
			}
		}

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
					_tabletext.Attach(label, x, x+1, y, y+1, AttachOptions.Fill, AttachOptions.Fill, 5, 0);
					_labels.Add(label);
					
					Label label2 = new Label(_gt.Random.ShortString);
					label2.WidthRequest = LABELWIDTH;
					label2.SetAlignment(1f, 0.5f); 
					label2.Justify = Justification.Right;
					//label.ExposeEvent += OnWidgetExposed; // The expose event of the label is never fired
					_tablebutton.Attach(label2, x, x+1, y, y+1, AttachOptions.Fill, AttachOptions.Fill, 5, 0);
					_labels.Add(label2);
				}
				else
				{
					Entry entry = new Entry(_gt.Random.ShortString);
					entry.WidthRequest = TEXTWIDTH;
					//entry.ExposeEvent += OnWidgetExposed;
					_tabletext.Attach(entry, x, x+1, y, y+1);
					_textboxes.Add(entry);
					
					_imageindex++;
					if (_imageindex > 100) { _imageindex = 1; }
					string filename = _imagepath + _imageindex.ToString("000") + ".png";
					Button button = new Button();
					button.Add(ImageButtonContent(filename, _gt.Random.ShortString));
					button.WidthRequest = TEXTWIDTH;
					button.HeightRequest = 42;
					//button.ExposeEvent += OnWidgetExposed;
					_tablebutton.Attach(button, x, x+1, y, y+1);
					_buttons.Add(button);
				}
			}
		}
		
		_tabletext.WidthRequest = 270;
		_tablebutton.WidthRequest = 270;
		this.ShowAll();
		this.Maximize();
	}

	static Widget ImageButtonContent(string filename, string text)
	{
		/* Create box for image and label */
		HBox box = new HBox(false, 0);
		box.BorderWidth =  2;

		/* Now on to the image stuff */
		Image image = new Image(filename);

		/* Create a label for the button */
		Label label = new Label(text);

		/* Pack the image and label into the box */
		box.PackStart(image, false, false, 3);
		box.PackStart(label, false, false, 3);

		image.Show();
		label.Show();

		return box;
	}

	public void LoadTab()
	{
		/*_tabnumber++;
		switch (_tabnumber)
		{
			case 1:
				Experiment1 exp1 = new Experiment1(this, _gt);
				exp1.Dock = DockStyle.Fill;
				_notebook.Controls[0].Controls.Add(exp1);
				_notebook.SelectedIndex = 0;
				break;
				case 2:
				Experiment2 exp2 = new Experiment2(this, _gt);
				exp2.Dock = DockStyle.Fill;
				_notebook.Controls[1].Controls.Add(exp2);
				_notebook.SelectedIndex = 1;
				break;
				case 3:
				Experiment3 exp3 = new Experiment3(this, _gt);
				exp3.Dock = DockStyle.Fill;
				_notebook.Controls[2].Controls.Add(exp3);
				_notebook.SelectedIndex = 2;
				break;
				case 4:
				Experiment4 exp4 = new Experiment4(this, _gt);
				exp4.Dock = DockStyle.Fill;
				_notebook.Controls[3].Controls.Add(exp4);
				_notebook.SelectedIndex = 3;
				break;
				case 5:
				_gt.Usage.TakeSnapshot("draw end");
				_gt.Stop();

				if (AUTOCLOSE) { this.Close(); }
				else { Console.WriteLine("finished"); }
				break;
		}*/
	}

	protected void OnWidgetExposed(object sender, ExposeEventArgs a)
	{
		/*controlsloaded++;
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
		}*/
	}

	public void NewValues()
	{
		/*foreach (Label label in _labels)
		{
			label.Text = _gt.Random.ShortString;
		}
		foreach (TextBox text in _textboxes)
		{
			text.Text = _gt.Random.ShortString;
		}
		foreach (Button button in _buttons)
		{
			_imageindex++;
			if (_imageindex > 100) { _imageindex = 1; }
			button.Image = Image.FromFile(_imagepath + _imageindex.ToString("000") + ".png");
			button.Text = _gt.Random.ShortString;
		}*/
	}

	public void FocusMenu()
	{
		/*foreach (ToolStripMenuItem item in _menu.Items)
		{
			item.ShowDropDown();
			foreach (ToolStripMenuItem subitem in item.DropDownItems)
			{
				subitem.Select();
			}
			item.HideDropDown();
		}*/
	}

	public void ExperimentDone()
	{
		NewValues();
	}

	public void OnMenuExposed(object sender, ExposeEventArgs args)
	{
		/*_controlspainted++;
		if (_tabnumber == 0 && _controlspainted == 1)
		{
			_gt.Usage.TakeSnapshot("draw start");
		}
		else if (_controlspainted == 20 || _controlspainted == 185 || _controlspainted == 345 || _controlspainted == 510)
		{
			FocusMenu();
		}
		else if (_controlspainted == 165)
		{
			toolStripStatusLabel1.Text = _gt.Random.LongString;
			toolStripProgressBar1.ProgressBar.Value = 5;
			LoadTab();
		}
		else if (_controlspainted == 325)
		{
			toolStripStatusLabel1.Text = _gt.Random.LongString;
			toolStripProgressBar1.ProgressBar.Value = 25;
			LoadTab();
		}
		else if (_controlspainted == 490)
		{
			toolStripStatusLabel1.Text = _gt.Random.LongString;
			toolStripProgressBar1.ProgressBar.Value = 50;
			LoadTab();
		}
		else if (_controlspainted == 655)
		{
			toolStripStatusLabel1.Text = _gt.Random.LongString;
			toolStripProgressBar1.ProgressBar.Value = 75;
			LoadTab();
		}
		else if (_controlspainted == 675)
		{
			toolStripStatusLabel1.Text = _gt.Random.LongString;
			toolStripProgressBar1.ProgressBar.Value = 100;
			LoadTab();
		}*/
		/*else
			{
				Console.WriteLine(_controlspainted + ": " + sender.GetType());
			}*/
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
