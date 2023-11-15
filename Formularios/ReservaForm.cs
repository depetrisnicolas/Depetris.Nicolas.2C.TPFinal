using Entidades.sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Entidades.excepciones;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;

namespace Formularios
{
    public partial class ReservaForm : Form
    {
        private MainForm formMain;
        private List<Reserva> listaReservas;

        public ReservaForm(MainForm mainForm)
        {
            InitializeComponent();
            this.formMain = mainForm;
        }

        public List<Reserva> ListaReservas { get => this.listaReservas; set => this.listaReservas = value; }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = this.txtDniCliente.Text;
                int.TryParse(dni, out int numDni);
                Cliente clienteBuscado = this.BuscarPorDni(numDni, this.formMain.ListaClientes);
                this.txtNombreYApellido.Text = clienteBuscado.ToString();
            }
            catch (ElementoNoEncontradoException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private Cliente BuscarPorDni(int numDni, List<Cliente> listaClientes)
        {
            try
            {
                foreach (Cliente cliente in listaClientes)
                {
                    if (cliente.Dni == numDni)
                    {
                        return cliente;
                    }
                }
                throw new ElementoNoEncontradoException("No se encontró ningún cliente con ese DNI.");
            }
            catch (BaseDeDatosException ex)
            {
                // Manejar la excepción, por ejemplo, mostrando un mensaje de error.
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Otra opción podría ser lanzar la excepción hacia arriba.
            }
        }

        private void lstVehiculosDisp_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void CargarListaVehiculosDisp()
        {
            this.lstVehiculosDisp.Items.Clear();
            foreach (Vehiculo vehiculo in this.formMain.ListaVehiculos)
            {
                if (vehiculo.Disponible == true)
                {
                    this.lstVehiculosDisp.Items.Add(vehiculo);
                }
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                this.CargarListaVehiculosDisp();
            }
            catch (BaseDeDatosException)
            {
                MessageBox.Show("No hay ningún elemento para leer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReservaForm_Load(object sender, EventArgs e)
        {
            this.dtpDesde.MinDate = DateTime.Now;
            this.dtpHasta.MinDate = DateTime.Now.AddDays(1);
            this.ListaReservas = ReservaDAO.LeerReservas();
        }

        private void btnConfirmarReserva_Click(object sender, EventArgs e)
        {
            if (this.lstVehiculosDisp.SelectedItem is not null)
            {
                string dni = this.txtDniCliente.Text;
                int.TryParse(dni, out int numDni);
                Cliente clienteBuscado = this.BuscarPorDni(numDni, this.formMain.ListaClientes);

                DateTime fechaInicio = this.dtpDesde.Value;
                DateTime fechaFin = this.dtpHasta.Value;

                Vehiculo vehiculoSelecc = (Vehiculo)this.lstVehiculosDisp.SelectedItem;
                vehiculoSelecc.Disponible = false;
                VehiculoDAO.Modificar(vehiculoSelecc, vehiculoSelecc.Patente);
                

                Reserva nuevaReserva = new Reserva(clienteBuscado, clienteBuscado.Dni, vehiculoSelecc, vehiculoSelecc.Patente, fechaInicio, fechaFin);
                MessageBox.Show("La reserva se realizó con éxito", "Reserva exitosa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ReservaDAO.Guardar(nuevaReserva);
                this.ListaReservas.Add(nuevaReserva);
            }
        }

        private void CargarListaReservas()
        {
            this.lstReservas.Items.Clear();
            foreach (Reserva reserva in this.ListaReservas)
            {
                this.lstReservas.Items.Add(reserva);
            }
        }

        private void btnVerReservas_Click(object sender, EventArgs e)
        {
            if (this.ListaReservas is not null)
            {
                this.CargarListaReservas();
            }
        }
    }
}
