using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entidades.excepciones;
using Entidades.metodoGenerico;
using Entidades.interfaz;

namespace Entidades.sql
{
    public class ReservaDAO : IConexionABase, IGuardar<Reserva>
    {
        private static string stringConnection;
        private string tabla;

        static ReservaDAO()
        {
            ReservaDAO.stringConnection = "Server=.;Database=Depetris.Nicolas.2C.TPFinal;Trusted_Connection=True;";
        }

        public ReservaDAO(string tabla)
        {
            this.tabla = tabla;
        }

        public static string StringConnection { get => ReservaDAO.stringConnection; set => ReservaDAO.stringConnection = value; }

        public void Guardar(Reserva reserva)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ReservaDAO.StringConnection))
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
                throw new BaseDeDatosException($"Error al guardar reserva en la tabla {this.tabla}", ex);
            }

        }

        public static List<Reserva> LeerReservas()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ReservaDAO.StringConnection))
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
                            Cliente cliente = MetodoGenerico.LeerElementoPorPropiedad(reader.GetInt32(1), ClienteDAO.LeerClientes(),
                                c => c.Dni);
                            Vehiculo vehiculo = MetodoGenerico.LeerElementoPorPropiedad(reader.GetString(3), VehiculoDAO.LeerVehiculos(), 
                                v => v.Patente);

                            Reserva reserva = new Reserva(cliente, reader.GetInt32(1), vehiculo, reader.GetString(3), reader.GetDateTime(4), reader.GetDateTime(5), 
                                reader.GetBoolean(8));
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
                throw new BaseDeDatosException("Error al obtener informacion desde la base de datos RESERVAS", ex);
            }
        }

        public static void Modificar(Reserva reservaEditada, int clienteDni)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ReservaDAO.StringConnection))
                {
                    string query = "UPDATE RESERVAS set VIGENTE=@vigente WHERE CLIENTE_DNI=@dniCliente";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("dniCliente", clienteDni);
                    command.Parameters.AddWithValue("vigente", reservaEditada.Vigente);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new BaseDeDatosException("Error al actualizar la reserva", ex);
            }
        }
    }
}
