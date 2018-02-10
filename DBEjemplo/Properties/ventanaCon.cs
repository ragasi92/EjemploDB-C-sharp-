using System;

namespace DBEjemplo
{
	public partial class ventanaCon : Gtk.Window
	{
		public ventanaCon () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

