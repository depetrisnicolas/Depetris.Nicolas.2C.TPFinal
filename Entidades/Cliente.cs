using Entidades.excepciones;
using Entidades.interfaz;

namespace Entidades
{
    

    public class Cliente 
    {
        //ATRIBUTOS
        private string nombre;
        private string apellido;
        private int dni;
        private string telefono;

        //CONSTRUCTOR
        public Cliente(string nombre, string apellido, int dni, string telefono)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            this.telefono = telefono;
        }

        //PROPIEDADES
        public string Nombre { get => this.nombre; set => this.nombre = value; }
        public string Apellido { get => this.apellido; set => this.apellido = value; }
        public int Dni { get => this.dni; set => this.dni = value; }
        public string Telefono { get => this.telefono; set => this.telefono = value; }

        //SOBRECARGA PARA MOSTRAR LOS DATOS DEL CLIENTE
        public override string ToString() 
        {
            return $"{this.Nombre} {this.Apellido}";
        }
    }
}