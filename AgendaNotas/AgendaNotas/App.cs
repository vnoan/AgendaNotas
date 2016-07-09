using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaNotas.Model;
using AgendaNotas.View;
using AgendaNotas.Controller;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using PCLStorage;
using System.Diagnostics;

namespace AgendaNotas
{
	public class App : Application
	{
        public static ObservableCollection<Materia> Materias = new ObservableCollection<Materia>();
        private IFile file;

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


        protected override async void OnStart()
        {
            // Formato:
            // nome = nota1@peso; nota2@peso2 /...
            file = await Arquivos.LoadFile();
            string[] materias = await Arquivos.ReadOfFile(file);
            if (string.IsNullOrEmpty(materias[0]))
                return;
            foreach(string s in materias)
            {
                var provas = new List<Nota>();
                var materiaSplitted = s.Split('=');
                var notasSplitted = materiaSplitted[1].Split(';');     
                foreach(string nota in notasSplitted)
                {
                    var str = nota.Split('@');
                    provas.Add(new Nota(float.Parse(str[0]), int.Parse(str[1])));
                }
                Materias.Add(new Materia(materiaSplitted[0],provas));
            }
            

        }

        protected override async void OnSleep ()
		{
            // Formato:
            // nome = nota1@peso; nota2@peso2 /...
            var toWrite = new string[Materias.Count];
            var i = 0;
            string notas;
            foreach (Materia m in Materias)
            {
                notas = string.Empty;
                foreach(Nota n in m.provas)
                {
                    notas += n.valor + "@" + n.peso + ";";
                }
                toWrite[i] = m.nome + "=" + notas;
            }
            await Arquivos.WriteOnFile(file, toWrite);
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
       
	}
}
