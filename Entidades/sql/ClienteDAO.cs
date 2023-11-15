using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades.excepciones;

namespace Entidades.sql
{
    public class ClienteDAO
    {
        private static string stringConnection;

        static ClienteDAO()
        {
            ClienteDAO.stringConnection = "Server=.;Database=Depetris.Nicolas.2C.TPFinal;Trusted_Connection=True;";
        }


        public static void Guardar(Cliente cliente)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClienteDAO.stringConnection))
                {
                    string query = "INSERT INTO CLIENTES (NOMBRE, APELLIDO, DNI, TELEFONO) " +
                                   "VALUES (@NOMBRE, @APELLIDO, @DNI, @TELEFONO); SELECT @@IDENTITY";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NOMBRE", cliente.Nombre);
                    command.Parameters.AddWithValue("@APELLIDO", cliente.Apellido);
                    command.Parameters.AddWithValue("@DNI", cliente.Dni);
                    command.Parameters.AddWithValue("@TELEFONO", cliente.Telefono);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static Cliente LeerPorDni(int dni)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClienteDAO.stringConnection))
                {
                    string query = "SELECT * FROM CLIENTES WHERE DNI = @DNI";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DNI", dni);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Cliente(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4));
                    }
                    else
                    {
                        throw new ElementoNoEncontradoException("No existe ningún cliente con ese DNI");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BaseDeDatosException("Error al obtener un elemento por DNI", ex);
            }
        }

    }
}