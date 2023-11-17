using Entidades;
using Entidades.sql;
using System.Text.Json;

namespace Pruebas
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            string escritorioPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Especificar el nombre del archivo JSON y la ruta completa
            string archivoJson = "vehiculosDisponibles.json";
            string rutaCompleta = Path.Combine(escritorioPath, archivoJson);

            // Lee el contenido del archivo JSON utilizando la ruta completa
            string jsonString = File.ReadAllText(rutaCompleta);

            // Deserializa una lista de objetos de tipo Vehiculo a partir de JSON.
            List<Vehiculo> listaVehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(jsonString);

            // Itera sobre la lista de vehículos y muestra la información
            foreach (Vehiculo vehiculo in listaVehiculos)
            {
                Console.WriteLine($"Marca: {vehiculo.Marca}");
                Console.WriteLine($"Modelo: {vehiculo.Modelo}");
                Console.WriteLine($"Patente: {vehiculo.Patente}");
                Console.WriteLine();
            }

        }
    }
}