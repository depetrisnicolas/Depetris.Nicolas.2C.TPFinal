using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades.excepciones;
using Entidades.metodoExtension;
using Entidades.interfaz;

namespace Entidades.sql
{
    public class ClienteDAO : IConexionABase, IGuardar<Cliente>
    {
        private static string stringConnection;
        private string tabla;

        static ClienteDAO()
        {
            ClienteDAO.stringConnection = "Server=.;Database=Depetris.Nicolas.2C.TPFinal;Trusted_Connection=True;";
        }

        public ClienteDAO(string tabla)
        {
            this.tabla = tabla;
        }

        public static string StringConnection { get => ClienteDAO.stringConnection; set => ClienteDAO.stringConnection = value; }

        public void Guardar(Cliente cliente)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClienteDAO.StringConnection))
                {
                    string query = "INSERT INTO CLIENTES (NOMBRE, APELLIDO, DNI, TELEFONO) " +
                                   "VALUES (@NOMBRE, @APELLIDO, @DNI, @TELEFONO); SELECT @@IDENTITY";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NOMBRE", cliente.Nombre);
                    command.Parameters.AddWithValue("@APELLIDO", cliente.Apellido);
                    command.Parameters.AddWithValue("@DNI", cliente.Dni);
                    command.Parameters.AddWithValue("@TELEFONO", cliente.Telefono.AgregarPrefijo("011"));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new BaseDeDatosException($"Error al guardar cliente en tabla {this.tabla}", ex);
            }
        }

        public static List<Cliente> LeerClientes()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClienteDAO.StringConnection))
                {
                    List<Cliente> listaClientes = new List<Cliente>();
                    string query = "SELECT * FROM CLIENTES";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4));
                            listaClientes.Add(cliente);
                        }
                        return listaClientes;
                    }
                    else
                    {
                        throw new ElementoNoEncontradoException("Tabla Vacia");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BaseDeDatosException("Error al obtener informacion desde la base de datos CLIENTES", ex);
            }
        }
    }
}