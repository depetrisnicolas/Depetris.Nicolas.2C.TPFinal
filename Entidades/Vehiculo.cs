using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Vehiculo
    {
        private string marca;
        private string modelo;
        private int anio;
        private string tipo;
        private string patente;
        private bool disponible;

        public Vehiculo(string marca, string modelo, int anio, string tipo, string patente, bool disponible)
        {
            this.marca = marca;
            this.modelo = modelo;
            this.anio = anio;
            this.tipo = tipo;
            this.patente = patente;
            this.disponible = disponible;
        }

        public string Marca { get => this.marca; set => this.marca = value; }
        public string Modelo { get => this.modelo; set => this.modelo = value; }
        public int Anio { get => this.anio; set => this.anio = value; }
        public string Tipo { get => this.tipo; set => this.tipo = value; }
        public string Patente { get => this.patente; set => this.patente = value; }
        public bool Disponible { get => this.disponible; set => this.disponible = value; }


        public override string ToString()
        {
            return $"{this.Tipo} - {this.Marca} {this.Modelo} - {this.Patente}";
        }
    }
}
