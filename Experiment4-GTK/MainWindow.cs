using System;
using System.Collections.Generic;
using System.IO;
using Gtk;
using GuiTestLib;
using Mono.Data.Sqlite;

public partial class MainWindow: Gtk.Window
{
	private const bool AUTOCLOSE = false;
	private const int COLUMNS = 2;
	private const int ROWS = 10;
	private const int LABELWIDTH = 300;
	private const int TEXTWIDTH = 300;
	private const int TEXTHEIGHT = 50;

	private TreeView _treeview;
	private string _connectionstring = "Data Source=" + Directory.GetCurrentDirectory().ToString() + "/Databases/Northwind.sl3;Version=3;";
	private int _querynumber = 0;
	private int _paintedcells = 0;
	private int _expectedcells = 0;
	private SqliteConnection _connection;

	private GuiTracker _gt;
	private bool yes = true;
	
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
		_connection = new SqliteConnection(_connectionstring);
		
		ScrolledWindow sw = new ScrolledWindow();
		sw.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
		Fixed fv = new Fixed();
		fv.BorderWidth = 5;

		_treeview = new TreeView();
		//_datagridview.CellPainting += OnCellPainting;

		fv.Put(_treeview, 0, 0);
		sw.AddWithViewport(fv);
		this.Add(sw);
		this.Maximize();
		this.ShowAll();

		LoadData();
	}

	public void LoadData()
	{
		_querynumber++;
		switch (_querynumber)
		{
			case 1:
				_expectedcells = 8;
				SqlQuery("SELECT CategoryName FROM Categories");
				break;
			/*case 2:
				_expectedcells = 30;
				SqlQuery("SELECT CustomerID, CompanyName, ContactName, ContactTitle, City, Country FROM Customers LIMIT 55,30");
				break;
			case 3:
				_expectedcells = 45;
				SqlQuery("SELECT LastName, FirstName, Title, City, Region, Country FROM Employees");
				break;
			case 4:
				_expectedcells = 55;
				SqlQuery("SELECT ProductID, ProductName, QuantityPerUnit, UnitPrice, UnitsInStock FROM Products WHERE ProductName LIKE 'G%'");
				break;
			case 5:
				_expectedcells = 174;
				SqlQuery("SELECT ContactTitle, ContactName, CompanyName, Address, City, Country FROM Suppliers");
				break;*/
			case 6:
				_gt.Usage.TakeSnapshot("draw end");
				
				if (AUTOCLOSE)
				{
					_gt.Stop();
					
					Application.Quit();
					//this.RetVal = true;
				}
				//else { Console.WriteLine("finished"); }
				break;
		}
	}

	public void SqlQuery(string sql)
	{
		while (_treeview.Columns.Length > 0)
		{
			_treeview.RemoveColumn(_treeview.Columns[0]);
		}
		
		SqliteCommand cmd = new SqliteCommand(sql, _connection);
		cmd.Connection.Open();
		try
		{
			SqliteDataReader reader = cmd.ExecuteReader();
			
			Type[] types = new Type[reader.FieldCount];
			for (int i = 0; i < reader.FieldCount; i++)
			{
				_treeview.AppendColumn(new TreeViewColumn((i + 1).ToString(), new CellRendererText(), "text", i));
				types[i] = reader.GetFieldType(i);
			}
			TreeStore treestore = new TreeStore(types);
			treestore.RowChanged += OnRowChanged;
			while (reader.Read())
			{
				object[] row = new object[reader.FieldCount];
				for (int i = 0; i < reader.FieldCount; i++)
				{
					if (types[i] == typeof(string)) { row[i] = reader.GetString(i); }
					else if (types[i] == typeof(int)) { row[i] = reader.GetInt32(i); }
					else { row[i] = reader.GetValue(i); }
				}
				treestore.AppendValues(row);
			}
			
			_treeview.Model = treestore;
			reader.Close();
		}
		catch (SqliteException ex)
		{
			//Console.WriteLine(_connectionstring + ": " + ex.Message + ": " + ex.InnerException);
		}
		cmd.Connection.Close();
		
		_treeview.ShowAll();
		this.ShowAll();
	}

	public void OnRowChanged(object sender, RowChangedArgs e)
	{
		_paintedcells++;
		if (_querynumber == 1 && _paintedcells == 1)
		{
			_gt.Usage.TakeSnapshot("draw start");
		}
		if (_paintedcells == _expectedcells)
		{
			_paintedcells = 0;
			LoadData();
		}
		else
		{
			//Console.WriteLine(_paintedcells);
		}
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
