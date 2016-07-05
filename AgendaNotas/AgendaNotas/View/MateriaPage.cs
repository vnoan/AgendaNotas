using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaNotas.Model;
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
            ToolbarItems.Add(new ToolbarItem("Remover", null, removerMateria));
            ToolbarItems.Add(new ToolbarItem("Provas", null, addNota));

            layout.Children.Add(new Label { Text = "Nome: " + m.ToString() });
            layout.Children.Add(info);
            layout.Children.Add(bEditar);

            bEditar.Clicked += editarMateria;
            Padding = 30;
            Content = layout;
		}

        private void addNota()
        {
            Navigation.PushAsync(new ProvasPage(m));
        }

        private void editarMateria(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditarMateria(m));
        }

        async void removerMateria()
        {
            bool go = await DisplayAlert("Remoção", "Tem certeza que quer remover esta matéria?", "Sim", "Não");
            if (go)
            {
                App.Materias.Remove(m);
                await Navigation.PopAsync();
            }
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
