using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace DBEjemplo
{
	public partial class wAlumnos : Gtk.Window
	{
		MySqlCommand comand;
		MySqlConnection con;
		MySqlDataReader dr;
		public wAlumnos () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			con = new MySqlConnection ("Server=localhost;" +
				"Database=prestamos;Uid=phpmyadmin;Pwd=rafaja77");
			tvAlumnos (vwAlumnos);
			vwAlumnos.Model = dAlumnos ();
			cmbCarrera.AppendText ("IC");
			cmbCarrera.AppendText ("ICE");
			cmbCarrera.AppendText ("ENF");
			cmbCarrera.AppendText ("PSI");

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

	
		protected void OnBtnGuardarClicked (object sender, EventArgs e)
		{
			con.Open ();
			comand = new MySqlCommand ("INSERT INTO `alumnos`(" +
				"`alumno_nombre`, `alumno_apellidop`, `alumno_apellidom`, " +
				"`alumno_carrera`) " +
				"VALUES (?nm,?ap,?am,?car)",con);
			//comand.Parameters.Add ("?id", MySqlDbType.Int32).Value = id ();
			comand.Parameters.Add ("?nm", MySqlDbType.VarChar).Value = txtNombre.Text;
			comand.Parameters.Add ("?ap", MySqlDbType.VarChar).Value = txtAP.Text;
			comand.Parameters.Add ("?am", MySqlDbType.VarChar).Value = txtAM.Text;
			comand.Parameters.Add ("?car", MySqlDbType.VarChar).Value = cmbCarrera.ActiveText;
			if (comand.ExecuteNonQuery()>0) {
				con.Close ();
				limpiar ();
				vwAlumnos.Model = dAlumnos ();
			} else {
				Console.WriteLine ("Problema");
			}
		}

		private void limpiar(){
			txtAM.Text = "";
			txtAP.Text = "";
			txtNombre.Text = "";
		}
	}
}

