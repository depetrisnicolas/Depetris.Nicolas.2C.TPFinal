using Entidades;
using Entidades.sql;

namespace Pruebas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Vehiculo> listaVehiculos = VehiculoDAO.LeerVehiculos();
            foreach (Vehiculo vehiculo in listaVehiculos)
            {
                Console.WriteLine(vehiculo.Disponible);
            }

        }
    }
}