using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AgendaNotas.Model
{
    public class Materia : INotifyPropertyChanged
    {
        //Properties
        
        public event PropertyChangedEventHandler PropertyChanged;
        public static int LastId;
        [Ignore]
        public List<Nota> Provas { get; set; }

        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _nome;
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("nome"));
            }
        }

        [Ignore]
        public float Media
        {
         get
            {
                float aux1 = 0, aux2 = 0;
                foreach(Nota p in Provas)
                {
                    aux1 += (p.Valor * p.Peso);
                    aux2 += p.Peso;
                }
                return aux1 / aux2;
            }      
        }

        //Constructors
        public Materia()
        {
            Materia.LastId += 1;
            this.Id = Materia.LastId;

        }
        public Materia(string nome)
        {
            Materia.LastId += 1;
            this.Id = Materia.LastId;
            this.Provas = new List<Nota>();
            this.Nome = nome;
        }
        public Materia(string nome, int qtNotas)
        {
            Materia.LastId += 1;
            this.Id = Materia.LastId;
            this.Provas = new List<Nota>();
            for (int i = 0; i < qtNotas; i++)
            {
                Provas.Add(new Nota(_id));
            }
            this.Nome = nome;

        }
        public Materia(string nome, List<Nota> provas)
        {
            Materia.LastId += 1;
            this.Id = Materia.LastId;
            this.Provas = provas;
            this.Nome = nome;
       }

        //Methods
        public void AddProva(float valor, int peso)
        {
            Nota n = new Nota(valor, peso, _id);
            
            //Quando eu add uma prova, a coleção nao altera, logo não salva no DB.
            Provas.Add(n);
            App.AddNota(n);     //Isso add a nota no DB
            
            //Avisa que houve alteração nas propriedades. 
            //A priori, Provas não é usado em outro lugar que requer este aviso
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Provas"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Media"));
        }
        public override string ToString()
        {
            // Formato:
            // nome = nota1@peso; nota2@peso2 /...
            string str = Nome + "=";
            foreach (Nota n in Provas)
            {
                str += n.Valor + "@" + n.Peso + ";" ;
            }
            return str;
        }

    }
    
}
