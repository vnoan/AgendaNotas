using AgendaNotas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using AgendaNotas.View;
using System.Diagnostics;

namespace AgendaNotas.ViewModel
{
    public class ListaMateriaVM : INotifyPropertyChanged
    {
        public ObservableCollection<Materia> ListaMateria
        {
            get
            {
                return App.Materias;
            }
        }

        INavigation Nav;
        public ListaMateriaVM(INavigation nav)
        {
            Nav = nav;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Materia _materia;        
        public Materia materia
        {
            get
            {
                return _materia;
            }
            set
            {
                _materia = value;
                Debug.Write("PEGANDO A MATERIA " + (_materia  == null ? "vazia" : _materia.nome));
                if (value != null)
                {
                    Nav.PushAsync(new MateriaPage(materia));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListaMateria"));
                }
            }
        }

    }
}
