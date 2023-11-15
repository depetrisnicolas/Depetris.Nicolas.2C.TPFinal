using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades.excepciones;

namespace Entidades.sql
{
    public class ReservaDAO
    {
        private static string stringConnection;

        static ReservaDAO()
        {
            ReservaDAO.stringConnection = "Server=.;Database=Depetris.Nicolas.2C.TPFinal;Trusted_Connection=True;";
        }


        public static void Guardar(Reserva reserva)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ReservaDAO.stringConnection))
                {
                    string query = "INSERT INTO RESERVAS (CLIENTE, CLIENTE_DNI, VEHICULO, VEHICULO_PATENTE, FECHA_INICIO, FECHA_FIN, TARIFA_AUTO, TARIFA_CAMIONETA, VIGENTE) " +
                                   "VALUES (@CLIENTE, @CLIENTE_DNI, @VEHICULO, @VEHICULO_PATENTE, @FECHA_INICIO, @FECHA_FIN, @TARIFA_AUTO, @TARIFA_CAMIONETA, @VIGENTE); " +
                                   "SELECT @@IDENTITY";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CLIENTE", reserva.Cliente.ToString());
                    command.Parameters.AddWithValue("@CLIENTE_DNI", reserva.DniCliente);
                    command.Parameters.AddWithValue("@VEHICULO", reserva.Vehiculo.ToString());
                    command.Parameters.AddWithValue("@VEHICULO_PATENTE", reserva.Vehiculo.Patente);
                    command.Parameters.AddWithValue("@FECHA_INICIO", reserva.FechaInicio);
                    command.Parameters.AddWithValue("@FECHA_FIN", reserva.FechaFin);
                    command.Parameters.AddWithValue("@TARIFA_AUTO", Reserva.TarifaAuto);
                    command.Parameters.AddWithValue("@TARIFA_CAMIONETA", Reserva.TarifaCamioneta);
                    command.Parameters.AddWithValue("@VIGENTE", reserva.Vigente);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new BaseDeDatosException("Error al guardar reserva", ex);
            }

        }

        public static List<Reserva> LeerReservas()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ReservaDAO.stringConnection))
                {
                    List<Reserva> listaReservas = new List<Reserva>();
                    string query = "SELECT * FROM RESERVAS";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Reserva reserva = new Reserva(ClienteDAO.LeerClientePorDni(reader.GetInt32(1)), reader.GetInt32(1), 
                                VehiculoDAO.LeerVehiculoPorPatente(reader.GetString(3)), reader.GetString(3), reader.GetDateTime(4),
                                reader.GetDateTime(5));
                            listaReservas.Add(reserva);
                        }
                        return listaReservas;
                    }
                    else
                    {   
                        throw new ElementoNoEncontradoException("No hay reservas cargadas");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BaseDeDatosException("Error al obtener informacion desde la base de datos", ex);
            }
        }
    }
}
