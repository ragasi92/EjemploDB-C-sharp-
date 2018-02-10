using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;
using DBEjemplo;


public partial class MainWindow: Gtk.Window{
	MySqlCommand comand;
	MySqlConnection con;
	MySqlDataReader dr;
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		con = new MySqlConnection ("Server=localhost;" +
			"Database=prestamos;Uid=phpmyadmin;Pwd=rafaja77");		
		//tvArticulos (vwConsultas);
		
		
	
	}
	void tvAlumnos(TreeView tv){
		tv.AppendColumn ("Matricula",new CellRendererText(),"text",0);
		tv.AppendColumn ("Nombre",new CellRendererText(),"text",1);
		tv.AppendColumn ("Apellido m",new CellRendererText(),"text",2);
		tv.AppendColumn ("Apellido p",new CellRendererText(),"text",3);
		tv.AppendColumn ("Carrera",new CellRendererText(),"text",4);
	}



	ListStore dAlumnos(){
		ListStore data = new ListStore (typeof(int),typeof(string),
			typeof(string),typeof(string),typeof(string));
		data.Clear ();
		con.Open ();
		comand = new MySqlCommand ("SELECT * FROM alumnos",con);
		dr = comand.ExecuteReader ();
		while(dr.Read()){
			data.AppendValues (int.Parse( dr[0].ToString()),
				dr[1].ToString(),dr[2].ToString(),dr[3].ToString(),
				dr[4].ToString());
		}
		dr.Close ();
		con.Close ();
		return data;
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
		

	}

	protected void OnBtnPrestamosClicked (object sender, EventArgs e)
	{
		ventanaConsulta ds = new ventanaConsulta();
		ds.Show ();
	}
}
