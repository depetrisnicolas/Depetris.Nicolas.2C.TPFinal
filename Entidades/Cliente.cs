namespace Entidades
{
    public class Cliente
    {
        private string nombre;
        private string apellido;
        private int dni;
        private int telefono;

        public Cliente(string nombre, string apellido, int dni, int telefono)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.dni = dni;
            this.telefono = telefono;
        }

        public string Nombre { get => this.nombre; set => this.nombre = value; }
        public string Apellido { get => this.apellido; set => this.apellido = value; }
        public int Dni { get => this.dni; set => this.dni = value; }
        public int Telefono { get => this.telefono; set => this.telefono = value; }

        public override string ToString() 
        {
            return $"{this.Nombre} {this.Apellido}";
        }
    }
}