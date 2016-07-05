using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaNotas.Model;
using Xamarin.Forms;

namespace AgendaNotas.View
{
	public class Tabbed : TabbedPage
	{
        public Page materias;
		public Tabbed ()
		{
            materias = new ListaMateriaPage();
            Children.Add(materias);
            Title = "Notas";
		}
	}
}
