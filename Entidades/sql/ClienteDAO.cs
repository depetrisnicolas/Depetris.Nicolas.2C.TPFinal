﻿using System;
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

        public static List<Cliente> LeerClientes()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClienteDAO.stringConnection))
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
                            Cliente cliente = new Cliente(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4));
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
                throw new BaseDeDatosException("Error al obtener informacion desde la base de datos", ex);
            }
        }

        public static Cliente LeerClientePorDni(int dni)
        {
            try
            {
                List<Cliente> listaClientes = ClienteDAO.LeerClientes();

                foreach (Cliente cliente in listaClientes)
                {
                    if (cliente.Dni == dni)
                    {
                        return cliente;
                    }
                }

                throw new ElementoNoEncontradoException("Ningún cliente encontrado para ese DNI");
            }
            catch (Exception ex)
            {
                
                throw new ElementoNoEncontradoException("Error al buscar cliente", ex);
            }
        }

        public static Cliente LeerClientePor(int dni)
        {
            try
            {
                List<Cliente> listaClientes = ClienteDAO.LeerClientes();

                foreach (Cliente cliente in listaClientes)
                {
                    if (cliente.Dni == dni)
                    {
                        return cliente;
                    }
                }

                throw new ElementoNoEncontradoException("Ningún cliente encontrado para ese DNI");
            }
            catch (Exception ex)
            {

                throw new ElementoNoEncontradoException("Error al buscar cliente", ex);
            }
        }
    }
}