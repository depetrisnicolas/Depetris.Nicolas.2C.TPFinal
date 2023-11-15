using Entidades.excepciones;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.sql
{
    public class VehiculoDAO
    {
        private static string stringConnection;

        static VehiculoDAO()
        {
            VehiculoDAO.stringConnection = "Server=.;Database=Depetris.Nicolas.2C.TPFinal;Trusted_Connection=True;";
        }


        public static void Guardar(Vehiculo vehiculo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(VehiculoDAO.stringConnection))
                {
                    string query = "INSERT INTO VEHICULOS (MARCA, MODELO, ANIO, TIPO, PATENTE, DISPONIBLE) " +
                                   "VALUES (@MARCA, @MODELO, @ANIO, @TIPO, @PATENTE, @DISPONIBLE); SELECT @@IDENTITY";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MARCA", vehiculo.Marca);
                    command.Parameters.AddWithValue("@MODELO", vehiculo.Modelo);
                    command.Parameters.AddWithValue("@ANIO", vehiculo.Anio);
                    command.Parameters.AddWithValue("@TIPO", vehiculo.Tipo);
                    command.Parameters.AddWithValue("@PATENTE", vehiculo.Patente);
                    command.Parameters.AddWithValue("@DISPONIBLE", vehiculo.Disponible);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static List<Vehiculo> Leer()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(VehiculoDAO.stringConnection))
                {
                    List<Vehiculo> listaVehiculos = new List<Vehiculo>();
                    string query = "SELECT * FROM VEHICULOS WHERE DISPONIBLE=1";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Vehiculo vehiculo = new Vehiculo(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3),
                                reader.GetString(4));
                            listaVehiculos.Add(vehiculo);
                        }
                        return listaVehiculos;
                    }
                    else
                    {
                        throw new ElementoNoEncontradoException("Tabla Vacia");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BaseDeDatosException("Error al obtener un elemento por ID", ex);
            }
        }
    }
}
