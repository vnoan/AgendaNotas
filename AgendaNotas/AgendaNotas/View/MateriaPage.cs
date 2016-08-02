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
        private Materia _materia;
        private Button BEditar = new Button { Text = "Editar Matéria" };
        private StackLayout _layout = new StackLayout();
        private StackLayout _info = new StackLayout();
        private MateriaPageVM _vm;

        public MateriaPage (Materia mt)
        {
            _materia = mt;
            _vm = new MateriaPageVM(_materia, Navigation, this);

            var tbiRemover = new ToolbarItem("Remover", null, _vm.RemoverMateria);
            var tbiProvas = new ToolbarItem("Provas", null, _vm.AddNota);
            
            ToolbarItems.Add(tbiRemover);
            ToolbarItems.Add(tbiProvas);
            
            _layout.Children.Add(new Label { Text = "Nome: " + _materia.Nome });
            _layout.Children.Add(_info);
            _layout.Children.Add(BEditar);

            BEditar.Clicked += _vm.EditarMateria;

            Padding = 10;
            Content = _layout;
            this.BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            _info.Children.Clear();
            int contador = 1;
            foreach(Nota p in _materia.Provas)
            {
                _info.Children.Add(new Label { Text = "nota " + contador.ToString() + " : " + p.Valor.ToString() + " (" + p.Peso.ToString() + ")" });
                contador++;
            }

            _info.Children.Add(new Label {   Text = _materia.Media == 0 ? "--" : _materia.Media.ToString(),
                                            TextColor = _materia.Media > 6 ? Color.Green : Color.Red });
        }
	}
}
