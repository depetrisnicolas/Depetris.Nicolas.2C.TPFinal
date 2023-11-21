using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Entidades;
using Entidades.sql;
using System.Data.SqlClient;
using Entidades.excepciones;
using System.Diagnostics.Eventing.Reader;

namespace Formularios
{
    public partial class ClienteForm : Form
    {
        //ATRIBUTOS
        private MainForm formularioMain;
        private ValidarCaractAlfabeticosDelegate delegadoValidarSoloLetras;

        //CONSTRUCTOR
        public ClienteForm(MainForm mainForm)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.formularioMain = mainForm;
            this.delegadoValidarSoloLetras = new ValidarCaractAlfabeticosDelegate(cadena => 
                Regex.IsMatch(cadena, "^[a-zA-Z]+$") ? cadena : null);
        }

        /// <summary>
        /// Realiza la validación de los datos ingresados y guarda un nuevo cliente en la base de datos.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = this.delegadoValidarSoloLetras(this.txtNombre.Text);
            string apellido = this.delegadoValidarSoloLetras(this.txtApellido.Text);
            string dni = this.ValidarDni(this.txtDni.Text);
            string telefono = this.ValidarTelefono(this.txtTelefono.Text);

            this.LimpiarErrores();

            try
            {
                this.ValidarDatosCliente(nombre, apellido, dni, telefono);
            }
            catch (ClienteExistenteException ex) 
            {
                MessageBox.Show(ex.Message, "Alta Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
        }

        /// <summary>
        /// Valida los datos del cliente y guarda la información en la base de datos.
        /// </summary>
        /// <param name="nombre">Nombre del cliente.</param>
        /// <param name="apellido">Apellido del cliente.</param>
        /// <param name="dni">DNI del cliente.</param>
        /// <param name="telefono">Número de teléfono del cliente.</param>
        /// <exception cref="ClienteExistenteException">Se lanza si el cliente cargado ya existe en la base de datos.</exception>
        private void ValidarDatosCliente(string nombre, string apellido, string dni, string telefono)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                this.lblErrorNombre.Text = "Nombre inválido";
            }
            else if (string.IsNullOrEmpty(apellido))
            {
                this.lblErrorApellido.Text = "Apellido inválido";
            }
            else if (string.IsNullOrEmpty(dni))
            {
                this.lblErrorDni.Text = "DNI inválido";
            }
            else if (string.IsNullOrEmpty(telefono))
            {
                this.lblErrorCel.Text = "Celular inválido";
            }
            else
            {
                try
                {
                    int.TryParse(dni, out int numDni);
                    Cliente nuevoCliente = new Cliente(nombre, apellido, numDni, telefono);

                    //Si todavía no hay clientes guardados en la base de datos
                    if (this.formularioMain.ListaClientes is null)
                    {
                        this.formularioMain.ListaClientes = new List<Cliente>();
                        ClienteDAO clientesDAO = new ClienteDAO("Clientes");
                        clientesDAO.Guardar(nuevoCliente);
                        this.formularioMain.ListaClientes.Add(nuevoCliente);
                        MessageBox.Show("El cliente se guardó correctamente", "Alta Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                    //Si la lista de clientes ya fue creada verifica que el cliente no exista
                    else if (!this.ValidarClienteExistente(nuevoCliente, this.formularioMain.ListaClientes))
                    {
                        this.formularioMain.ListaClientes = ClienteDAO.LeerClientes();
                        ClienteDAO clientesDAO = new ClienteDAO("Clientes");
                        clientesDAO.Guardar(nuevoCliente);
                        this.formularioMain.ListaClientes.Add(nuevoCliente);
                        MessageBox.Show("El cliente se guardó correctamente", "Alta Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                    else
                    {
                        throw new ClienteExistenteException("El cliente ya existe en la base de datos");
                    }
                }
                catch(BaseDeDatosException)
                {
                    MessageBox.Show("Error al guardar el cliente en una base inexistente", "Alta Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        /// <summary>
        /// Valida el formato del Dni.
        /// </summary>
        /// <param name="dni">Número de DNI a validar.</param>
        /// <returns>El número de DNI validado o null si el formato es inválido.</returns>
        private string ValidarDni(string dni)
        {
            if (!Regex.IsMatch(dni, @"^\d{8}$"))
            {
                return null;
            }
            return dni;
        }

        /// <summary>
        /// Valida el formato de un número de teléfono.
        /// </summary>
        /// <param name="telefono">Número de teléfono a validar.</param>
        /// <returns>El número de teléfono validado o null si el formato es inválido.</returns>
        private string ValidarTelefono(string telefono)
        {
            if (!Regex.IsMatch(telefono, @"^\d{8}$"))
            {
                return null;
            }
            return telefono;
        }

        /// <summary>
        /// Valida si un cliente ya existe en una lista de clientes mediante su número de dni.
        /// </summary>
        /// <param name="cliente">Cliente a verificar.</param>
        /// <param name="listaClientes">Lista de clientes en la que se realiza la búsqueda.</param>
        /// <returns>
        /// <c>true</c> si el cliente ya existe en la lista, <c>false</c> si no existe.
        /// </returns>
        private bool ValidarClienteExistente(Cliente cliente, List<Cliente> listaClientes)
        {
            return listaClientes.Any(item => item.Dni == cliente.Dni);
        }

        /// <summary>
        /// Limpia los mensajes de error en el formulario de clientes.
        /// </summary>
        private void LimpiarErrores()
        {
            this.lblErrorNombre.Text = "";
            this.lblErrorApellido.Text = "";
            this.lblErrorDni.Text = "";
            this.lblErrorCel.Text = "";
        }
    }
}
