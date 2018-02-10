using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;
using DBEjemplo;


public partial class MainWindow: Gtk.Window{
	
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	
	}




	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnBtnArticulosClicked (object sender, EventArgs e)
	{
		wArticulos av = new wArticulos ();
		av.Show ();

	}

	protected void OnBtnAlumnosClicked (object sender, EventArgs e)
	{
		
		wAlumnos alvi = new wAlumnos ();
		alvi.Show ();
	}

	protected void OnBtnPrestamosClicked (object sender, EventArgs e)
	{
		
	}
}
