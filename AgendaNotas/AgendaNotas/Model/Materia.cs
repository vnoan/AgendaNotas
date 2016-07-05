using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AgendaNotas.Model
{
    public class Materia : INotifyPropertyChanged
    {
        public List<Nota> provas;
        public event PropertyChangedEventHandler PropertyChanged;

        private string _nome;
        public string nome
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
        

        public float media
        {
         get
            {
                float aux1 = 0, aux2 = 0;
                foreach(Nota p in provas)
                {
                    aux1 += (p.valor * p.peso);
                    aux2 += p.peso;
                }
                return aux1 / aux2;
            }      
        }
   
        public Materia(string nome)
        {
            this.provas = new List<Nota>();
            this.nome = nome;
        }

        public Materia(string nome, int qtNotas)
        {
            provas = new List<Nota>();
            for (int i = 0; i < qtNotas; i++)
            {
                provas.Add(new Nota());
            }
            this.nome = nome;
        }

        public void addProva(float nota, int peso)
        {
            provas.Add(new Nota(nota, peso));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("provas"));
        }

        public override string ToString()
        {
            return nome;
        }

    }
    //Um trabalho é considerado uma prova !
    public class Nota
    {
        public float valor;
        public int peso;

        public Nota()
        {
            this.valor = 0;
            this.peso = 1;
        }

        public Nota (float valor, int peso)
        {
            this.valor = valor;
            this.peso = peso;
        }
    }



    

    
}
