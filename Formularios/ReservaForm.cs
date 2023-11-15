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

namespace Formularios
{
    public partial class ReservaForm : Form
    {
        private MainForm formMain;

        public ReservaForm(MainForm mainForm)
        {
            InitializeComponent();
            this.formMain = mainForm;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string dni = this.txtDniCliente.Text;
            int.TryParse(dni, out int numDni);
            //Cliente clienteBuscado = this.BuscarPorDni(numDni, this.formMain.ListaClientes);
            Cliente clienteBuscado = ClienteDAO.LeerPorDni(numDni);
            this.txtNombreYApellido.Text = clienteBuscado.ToString();
        }

        //private Cliente BuscarPorDni(int numDni, List<Cliente> listaClientes)
        //{
        //    try
        //    {
        //        foreach (Cliente cliente in listaClientes)
        //        {
        //            if (cliente.Dni == numDni)
        //            {
        //                return cliente;
        //            }
        //        }
        //        throw new ElementoNoEncontradoException("No se encontró ningún cliente con ese DNI.");
        //    }
        //    catch (BaseDeDatosException ex)
        //    {
        //        // Manejar la excepción, por ejemplo, mostrando un mensaje de error.
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null; // Otra opción podría ser lanzar la excepción hacia arriba.
        //    }
        //}

        private void lstVehiculosDisp_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void CargarListaVehiculosDisp()
        {
            this.lstVehiculosDisp.Items.Clear();
            List<Vehiculo> listaVehiculos = VehiculoDAO.Leer();
            foreach (Vehiculo vehiculo in listaVehiculos)
            {
                this.lstVehiculosDisp.Items.Add(vehiculo);
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
        }

        private void btnConfirmarReserva_Click(object sender, EventArgs e)
        {
            if (this.lstVehiculosDisp.SelectedItem is not null)
            {
                float tarifa = 0;

                string dni = this.txtDniCliente.Text;
                int.TryParse(dni, out int numDni);
                Cliente clienteBuscado = ClienteDAO.LeerPorDni(numDni);

                DateTime fechaInicio = this.dtpDesde.Value;
                DateTime fechaFin = this.dtpHasta.Value;
                TimeSpan diferencia = fechaFin - fechaInicio;
                int diasDiferencia = (int)diferencia.TotalDays;

                Vehiculo vehiculoSelecc = (Vehiculo)this.lstVehiculosDisp.SelectedItem;
                Reserva nuevaReserva = new Reserva(clienteBuscado, vehiculoSelecc, fechaInicio, fechaFin);
                MessageBox.Show("La reserva se realizó con éxito", "Reserva exitosa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.lstReservas.Items.Add(nuevaReserva);
            }
        }

        private void lstReservas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
