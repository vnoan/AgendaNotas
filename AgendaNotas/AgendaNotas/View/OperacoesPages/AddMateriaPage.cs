using AgendaNotas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using AgendaNotas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AgendaNotas.View
{
    public class AddMateriaPage : OperacaoMateria
    {
        private Entry qtNota = new Entry { Text = "1", Keyboard = Keyboard.Numeric };

        public AddMateriaPage() : base()
        {
            Title = "Adicionar Materias";
            Grid.Children.Add(new Label { Text = "Quantidade de notas:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start }, 0, 0);
            Grid.Children.Add(qtNota, 1, 0);

            eNome.Completed += ((sender, e) => qtNota.Focus());

            bAction.Clicked += BAdd_Clicked;
            bAction.Text = "Adicionar Matéria";
        }

        private void BAdd_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(eNome.Text) || string.IsNullOrEmpty(qtNota.Text) || int.Parse(qtNota.Text) < 0)
            {
                lbLog.Text = "MATÉRIA NÃO ADICIONADA";
                return;
            }
            foreach (Materia m in App.Materias)
            {
                if (m.Nome == eNome.Text)
                {
                    lbLog.Text = "MATÉRIA JÁ EXISTE";
                    return;
                }
            }

            App.Materias.Add(new Materia(eNome.Text, int.Parse(qtNota.Text)));
            Navigation.PopToRootAsync();
        }

    }
}
