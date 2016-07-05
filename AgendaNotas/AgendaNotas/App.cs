using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaNotas.Model;
using AgendaNotas.View;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace AgendaNotas
{
	public class App : Application
	{
        public static ObservableCollection<Materia> Materias = new ObservableCollection<Materia>();

		public App ()
		{
            // The root page of your application
            var tab = new Tabbed();
            MainPage = new NavigationPage(tab);
            Materias.CollectionChanged += Materias_CollectionChanged;
		}

       private void Materias_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged();
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
            // Tem que carregar as materias de onde elas estao salvas para o vetor
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
