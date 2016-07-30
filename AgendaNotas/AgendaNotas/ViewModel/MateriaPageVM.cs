using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using AgendaNotas;
using AgendaNotas.Model;
using AgendaNotas.View;
using Xamarin.Forms;

namespace AgendaNotas.ViewModel
{
    public class MateriaPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Materia _materia;
        private INavigation _nav;
        private Page _page;

        public MateriaPageVM(Materia m, INavigation nav, Page p)
        {
            _materia = m;
            _nav = nav;
            _page = p;
        }

        private async void removerMateria()
        {
            bool go = await _page.DisplayAlert("Remoção", "Tem certeza que quer remover esta matéria?", "Sim", "Não");
            if (go)
            {
                App.Materias.Remove(_materia);
                await _nav.PopAsync();
            }
        }

        private async void addNota()
        {
            await _nav.PushAsync(new ProvasPage(_materia));
        }

        private async void editarMateria(object sender, EventArgs e)
        {
            await _nav.PushAsync(new EditarMateria(_materia));
        }


    }
}
