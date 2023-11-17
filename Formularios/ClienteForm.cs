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

namespace Formularios
{
    public partial class ClienteForm : Form
    {
        private MainForm formMain;
        private ValidarNombreDelegate delegadoValidarNombre;

        public ClienteForm(MainForm mainForm)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.formMain = mainForm;
            this.delegadoValidarNombre = new ValidarNombreDelegate(nombre => Regex.IsMatch(nombre, "^[a-zA-Z]+$") ? nombre : null);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = this.delegadoValidarNombre(this.txtNombre.Text);
            string apellido = this.delegadoValidarNombre(this.txtApellido.Text);
            string dni = ValidarDni(this.txtDni.Text);
            string telefono = ValidarTelefono(this.txtTelefono.Text);

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(telefono))
            {
                MessageBox.Show("Datos erróneos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int.TryParse(dni, out int numDni);
                Cliente nuevoCliente = new Cliente(nombre, apellido, numDni, telefono);
                ClienteDAO clientesDAO = new ClienteDAO("Clientes");
                clientesDAO.Guardar(nuevoCliente);
                this.formMain.ListaClientes.Add(nuevoCliente);
                this.Close();
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

        private void ClienteForm_Load(object sender, EventArgs e)
        {

        }
    }
}
