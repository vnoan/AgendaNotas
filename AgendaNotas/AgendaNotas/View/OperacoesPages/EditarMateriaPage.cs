using AgendaNotas.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AgendaNotas.View
{
    public class EditarMateriaPage : OperacaoMateria
    {
        private Materia _materia;
        public EditarMateriaPage(Materia m)
        {
            this._materia = m;

            eNome.Text = m.Nome;
            eNome.Completed += ENome_Completed;
            Title = "Editar " + m.Nome;

            bAction.Clicked += BConfirma_Clicked;
            bAction.Text = "Confirmar";

            ToolbarItems.Add(new ToolbarItem("+ Nota", null, addNota));

            int top = 0;
            foreach (Nota p in m.Provas)
            {
                Grid.Children.Add(new Label { Text = "Nota:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start }, 0, top);
                Grid.Children.Add(new Entry { Text = p.Valor.ToString(), Keyboard = Keyboard.Numeric }, 1, top);
                Grid.Children.Add(new Label { Text = "Peso:", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start }, 2, top);
                Grid.Children.Add(new Entry { Text = p.Peso.ToString(), Keyboard = Keyboard.Numeric }, 3, top);
                Grid.Children.Add(new Button { Text = "X" }, 4, top);
                top++;
            }

        }


        private void ENome_Completed(object sender, EventArgs e)
        {
            var v = Grid.Children.First((ent) => ent.GetType() == typeof(Entry));
            v.Focus();
        }

        private void BConfirma_Clicked(object sender, EventArgs e)
        {
            //Remove a materia.
            App.Materias.Remove(_materia);

            // Testa o nome inserido
            if (!string.IsNullOrEmpty(eNome.Text))
                _materia.Nome = eNome.Text;
            else
            {
                App.Materias.Add(_materia);
                lbLog.Text = "NÃO EDITADO";
                return;
            }
            //Pega o enumerator das entrys.
            var entrys = Grid.Children.OfType<Entry>();
            var enumerator = entrys.GetEnumerator();
            //Seta os valores das entrys para as matérias            
            foreach (Nota n in _materia.Provas)
            {
                enumerator.MoveNext();
                n.Valor = float.Parse(enumerator.Current.Text, CultureInfo.InvariantCulture);
                enumerator.MoveNext();
                n.Peso = int.Parse(enumerator.Current.Text, CultureInfo.InvariantCulture);
            }
            //Readd a materia e volta pra lista
            App.Materias.Add(_materia);
            Navigation.PopToRootAsync();
        }

        private void addNota()
        {
            Navigation.PushAsync(new AddNotasPage(_materia));
        }
    }

}
