using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaNotas.Model
{
    public class Nota
    {
        //Atributos
        public static int LastID;
        [PrimaryKey]
        public int ID { get; set; }
        public int MateriaID { get; set; }
        public float Valor { get; set; }
        public int Peso { get; set; }

        //Construtores
        public Nota()
        {
            LastID += 1;
            this.ID = LastID;
            this.Valor = 0;
            this.Peso = 1;
        }
        public Nota(int id)
        {
            LastID += 1;
            this.ID = LastID;
            this.MateriaID = id;
            this.Valor = 0;
            this.Peso = 1;
        }
        public Nota(float valor, int peso, int id)
        {
            LastID += 1;
            this.ID = LastID;
            this.Valor = valor;
            this.Peso = peso;
            this.MateriaID = id;
        }
        
    }
}
