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

namespace Formularios
{
    public partial class ClienteForm : Form
    {
        private MainForm formMain;
        private ValidarCaractAlfabeticosDelegate delegadoValidarSoloLetras;

        public ClienteForm(MainForm mainForm)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.formMain = mainForm;
            this.delegadoValidarSoloLetras = new ValidarCaractAlfabeticosDelegate(cadena => 
                Regex.IsMatch(cadena, "^[a-zA-Z]+$") ? cadena : null);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = this.delegadoValidarSoloLetras(this.txtNombre.Text);
            string apellido = this.delegadoValidarSoloLetras(this.txtApellido.Text);
            string dni = this.ValidarDni(this.txtDni.Text);
            string telefono = this.ValidarTelefono(this.txtTelefono.Text);

            this.lblErrorNombre.Text = "";
            this.lblErrorApellido.Text = "";
            this.lblErrorDni.Text = "";
            this.lblErrorCel.Text = "";

            try
            {
                this.ValidarDatosCliente(nombre, apellido, dni, telefono);
            }
            catch (ClienteExistenteException ex) 
            {
                MessageBox.Show(ex.Message, "Alta Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            

        }

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
                int.TryParse(dni, out int numDni);
                Cliente nuevoCliente = new Cliente(nombre, apellido, numDni, telefono);

                if (!this.ValidarClienteExistente(nuevoCliente, this.formMain.ListaClientes))
                {
                    ClienteDAO clientesDAO = new ClienteDAO("Clientes");
                    clientesDAO.Guardar(nuevoCliente);
                    this.formMain.ListaClientes.Add(nuevoCliente);
                    MessageBox.Show("El cliente se guardó correctamente", "Alta Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
                }
                else
                {
                    throw new ClienteExistenteException("El cliente ya existe en la base de datos");
                }
            }
        }

        private string ValidarDni(string dni)
        {
            if (!Regex.IsMatch(dni, @"^\d{8}$"))
            {
                return null;
            }
            return dni;
        }

        private string ValidarTelefono(string telefono)
        {
            if (!Regex.IsMatch(telefono, @"^\d{8}$"))
            {
                return null;
            }
            return telefono;
        }

        private bool ValidarClienteExistente(Cliente cliente, List<Cliente> listaClientes)
        {
            return listaClientes.Any(item => item.Dni == cliente.Dni);
        }
    }
}
