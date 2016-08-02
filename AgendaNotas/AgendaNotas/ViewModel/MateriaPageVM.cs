using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using AgendaNotas;
using AgendaNotas.Model;
using AgendaNotas.View;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AgendaNotas.ViewModel
{
    public class MateriaPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Materia> ListaMateria
        {
            get
            {
                return App.Materias;
            }
        }
        private Materia _materia;
        private INavigation _nav;
        private Page _page;

        public MateriaPageVM(Materia m, INavigation nav, Page p)
        {
            _materia = m;
            _nav = nav;
            _page = p;
        }

        public async void RemoverMateria()
        {
            bool go = await _page.DisplayAlert("Remoção", "Tem certeza que quer remover esta matéria?", "Sim", "Não");
            if (go)
            {
                App.Materias.Remove(_materia);
                await _nav.PopAsync();
            }
        }
        public async void AddNota()
        {
            await _nav.PushAsync(new AddNotasPage(_materia));
        }
        public async void EditarMateria(object sender, EventArgs e)
        {
            await _nav.PushAsync(new EditarMateriaPage(_materia));
        }


    }
}
