using Entidades;
using Entidades.sql;

namespace Pruebas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Cliente> listaClientes = ClienteDAO.LeerClientes();
            foreach (Cliente cliente in listaClientes)
            {
                Console.WriteLine(cliente);
            }

        }
    }
}