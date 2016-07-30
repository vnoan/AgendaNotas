using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaNotas.Model;
using AgendaNotas.ViewModel;
using Xamarin.Forms;

namespace AgendaNotas.View
{
	public class MateriaPage : ContentPage
	{
        Materia m;
        Button bEditar = new Button { Text = "Editar Matéria" };
        StackLayout layout = new StackLayout();
        StackLayout info = new StackLayout();

        public MateriaPage (Materia mt)
        {
            m = mt;
            BindingContext = new MateriaPageVM(m, Navigation, this);

            var remove = new ToolbarItem("Remover",null, null);
            var provas = new ToolbarItem("Provas", null, null);
            remove.SetBinding(ToolbarItem.CommandProperty, "removerMateria");
            provas.SetBinding(ToolbarItem.CommandProperty, "addNota");
            
            ToolbarItems.Add(remove);
            ToolbarItems.Add(provas);
            
            layout.Children.Add(new Label { Text = "Nome: " + m.nome });
            layout.Children.Add(info);
            layout.Children.Add(bEditar);

            bEditar.SetBinding(Button.CommandProperty, "editarMateria");

            Padding = 30;
            Content = layout;
		}

        protected override void OnAppearing()
        {
            info.Children.Clear();
            int contador = 1;
            foreach(Nota p in m.provas)
            {
                info.Children.Add(new Label { Text = "nota " + contador.ToString() + " : " + p.valor.ToString() + " (" + p.peso.ToString() + ")" });
                contador++;
            }

            info.Children.Add(new Label {   Text = m.media == 0 ? "--" : m.media.ToString(),
                                            TextColor = m.media > 6 ? Color.Green : Color.Red });
        }
	}
}
