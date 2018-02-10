using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace DBEjemplo
{
	public partial class wArticulos : Gtk.Window
	{
		MySqlCommand comand;
		MySqlConnection con;
		MySqlDataReader dr;
		public wArticulos () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			con = new MySqlConnection ("Server=localhost;" +
				"Database=prestamos;Uid=phpmyadmin;Pwd=rafaja77");		
			tvArticulos (vwConsultas);
			vwConsultas.Model = dArticulos ();
		}

		ListStore dArticulos(){
			ListStore data = new ListStore (typeof(int),typeof(string),
				typeof(string));
			con.Open ();
			comand = new MySqlCommand ("SELECT * FROM articulos",con);
			dr = comand.ExecuteReader ();
			while(dr.Read()){
				data.AppendValues (int.Parse( dr[0].ToString()),
					dr[1].ToString(),dr[2].ToString());
			}
			dr.Close ();
			con.Close ();
			return data;
		}
		void tvArticulos(TreeView tv){
			tv.AppendColumn ("ID",new CellRendererText(),"text",0);
			tv.AppendColumn ("Nombre",new CellRendererText(),"text",1);
			tv.AppendColumn ("Descripción",new CellRendererText(),"text",2);
		}

		protected void OnBtnAgregarClicked (object sender, EventArgs e)
		{
			con.Open ();
			comand = new MySqlCommand ("INSERT INTO `articulos`(`articulo_nombre`, " +
				"`articulo_descripcion`) " +
				"VALUES (?nm,?dsc)",con);
			//comand.Parameters.Add ("?id", MySqlDbType.Int32).Value = id ();
			comand.Parameters.Add ("?nm", MySqlDbType.VarChar).Value = txtNombre.Text;
			comand.Parameters.Add ("?dsc", MySqlDbType.VarChar).Value = txtDesc.Text;
			if (comand.ExecuteNonQuery()>0) {
				con.Close ();
				limpiar ();
				vwConsultas.Model = dArticulos ();
			} else {
				Console.WriteLine ("Problema");
			}
		}

		private void limpiar(){
			txtDesc.Text = "";
			txtNombre.Text = "";
		}

	}
}

