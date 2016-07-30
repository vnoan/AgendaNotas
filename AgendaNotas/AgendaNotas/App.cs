using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgendaNotas.Model;
using AgendaNotas.View;
using AgendaNotas.Controller;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using PCLStorage;
using SQLite;
using System.Diagnostics;

namespace AgendaNotas
{
	public class App : Application
	{
        public static ObservableCollection<Materia> Materias = new ObservableCollection<Materia>();
        private SQLiteConnection _db;

		public App ()
		{
            // The root page of your application
            var tab = new Tabbed();
            MainPage = new NavigationPage(tab);
            Materias.CollectionChanged += Materias_CollectionChanged;
		}

        private void Materias_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach(Materia m in e.NewItems)
            {
                _db.InsertOrReplace(m);
            }
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
            _db.CreateTable<Materia>();

            foreach (Materia m in _db.Table<Materia>())
            {
                Materias.Add(m);
            }
            
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
