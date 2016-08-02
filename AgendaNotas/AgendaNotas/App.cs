using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaNotas.Model;
using AgendaNotas.View;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SQLite;
using System.Diagnostics;

namespace AgendaNotas
{
    //Considerar a ideia de fazer uma versão 2.0
	public class App : Application
	{
        public static ObservableCollection<Materia> Materias = new ObservableCollection<Materia>();
        private static SQLiteConnection _db;

		public App ()
		{
            // The root page of your application
            var tab = new Tabbed();
            MainPage = new NavigationPage(tab);

		}

        private void Materias_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Tinha um problema aqui que era o carregamento inicial dos dados que disparava este método.
            //Setando isso no final do OnCreate resolve o problema
            Debug.WriteLine("Colecao alterada");
            if(e.NewItems == null)
            {
                //Uma materia está sendo removida da coleção
                foreach (Materia m in e.OldItems)
                {
                    _db.Delete(m);
                    foreach(Nota n in m.Provas)
                    {
                        Debug.WriteLine("Nota removida do db");
                        _db.Delete(n);
                    }
                }
            }
            else
            { 
                //Materia está sendo inserida na coleção
                foreach (Materia m in e.NewItems)
                {
                    Debug.WriteLine("Nota inserida no db");
                    _db.InsertOrReplace(m);
                    _db.InsertAll(m.Provas);
                }
            }
        }
        public static void AddNota(Nota n)
        {
            _db.Insert(n);
        }

        protected override void OnStart()
        {
            var sqliteFilename = "DBAgendaNotas";
            #if __ANDROID__
                //Just use whatever directory SpecialFolder.Personal returns
                string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            #else
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder instead
            #endif
            var path = Path.Combine(libraryPath, sqliteFilename);

            _db = new SQLiteConnection(path);
            /*
            _db.DropTable<Materia>();
            _db.DropTable<Nota>();
            */
            //Cria as tabelas se elas não existirem
            _db.CreateTable<Materia>();
            _db.CreateTable<Nota>();

            //Associa as notas às matérias.
            var _TableNotas = _db.Table<Nota>();

            _TableNotas.ToList<Nota>().ForEach((n) => Debug.WriteLine(n));

            foreach (Materia m in _db.Table<Materia>())
            {
                Debug.WriteLine(m.Id);
                var NotasDaMateria = from n in _TableNotas where n.MateriaID == m.Id select n;
                m.Provas = new List<Nota>(NotasDaMateria.ToList<Nota>());
                Materias.Add(m);
            }
            Nota.LastID = _TableNotas.Count();
            Materia.LastId = _db.Table<Materia>().Count();
            //Seto o método depois de carregar os dados.
            Materias.CollectionChanged += Materias_CollectionChanged;

        }
        

        protected override void OnSleep ()
		{

		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
       
	}
}
