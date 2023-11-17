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
using Entidades.metodoExtension;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

namespace Formularios
{
    public partial class ReservaForm : Form
    {
        private MainForm formMain;
        private List<Reserva> listaReservas;
        public BuscarPorDniDelegate delegadoBuscarPorDni;


        public ReservaForm(MainForm mainForm)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.formMain = mainForm;
            this.delegadoBuscarPorDni = new BuscarPorDniDelegate(BuscarPorDni);
        }

        public List<Reserva> ListaReservas { get => this.listaReservas; set => this.listaReservas = value; }

        private void ReservaForm_Load(object sender, EventArgs e)
        {
            this.dtpDesde.MinDate = DateTime.Now;
            this.dtpHasta.MinDate = DateTime.Now.AddDays(1);
            this.ListaReservas = ReservaDAO.LeerReservas();
            this.CargarListaReservas();
            this.CargarListaVehiculosDisp();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = this.txtDniCliente.Text;
                int.TryParse(dni, out int numDni);
                Cliente clienteBuscado = this.delegadoBuscarPorDni(numDni, this.formMain.ListaClientes);
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

        private void CargarListaVehiculosDisp()
        {
            //this.lstVehiculosDisp.Items.Clear();
            //foreach (Vehiculo vehiculo in this.formMain.ListaVehiculos)
            //{
            //    if (vehiculo.Disponible == true)
            //    {
            //        this.lstVehiculosDisp.Items.Add(vehiculo);
            //    }
            //}

            this.lstVehiculosDisp.Items.Clear();

            // Filtra los vehículos disponibles y ordena por Tipo utilizando LINQ
            var vehiculosDisponiblesOrdenados = this.formMain.ListaVehiculos
                .Where(vehiculo => vehiculo.Disponible)
                .OrderBy(vehiculo => vehiculo.Tipo);

            // Agrega los vehículos ordenados al ListBox
            foreach (Vehiculo vehiculo in vehiculosDisponiblesOrdenados)
            {
                this.lstVehiculosDisp.Items.Add(vehiculo);
            }

        }

        private void btnConfirmarReserva_Click(object sender, EventArgs e)
        {
            if (this.lstVehiculosDisp.SelectedItem is not null)
            {
                int.TryParse(this.txtDniCliente.Text, out int numDni);
                Cliente clienteBuscado = this.BuscarPorDni(numDni, this.formMain.ListaClientes);

                Vehiculo vehiculoSelecc = (Vehiculo)this.lstVehiculosDisp.SelectedItem;
                vehiculoSelecc.Disponible = false;
                VehiculoDAO.Modificar(vehiculoSelecc, vehiculoSelecc.Patente);

                Reserva nuevaReserva = new Reserva(clienteBuscado, clienteBuscado.Dni, vehiculoSelecc, vehiculoSelecc.Patente,
                    this.dtpDesde.Value, this.dtpHasta.Value, true);

                ReservaDAO reservaDAO = new ReservaDAO("Reservas");
                reservaDAO.Guardar(nuevaReserva);
                this.ListaReservas.Add(nuevaReserva);
                MessageBox.Show("La reserva se realizó con éxito", "Reserva exitosa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            this.CargarListaReservas();
            this.CargarListaVehiculosDisp();
        }

        private void CargarListaReservas()
        {
            this.lstReservas.Items.Clear();
            foreach (Reserva reserva in this.ListaReservas.FiltrarReservasVigentes())
            {
                this.lstReservas.Items.Add(reserva);
            }
        }

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            if (this.lstReservas.SelectedItem is not null)
            {
                Reserva reservaSeleccionada = (Reserva)this.lstReservas.SelectedItem;
                reservaSeleccionada.Vigente = false;
                ReservaDAO.Modificar(reservaSeleccionada, reservaSeleccionada.DniCliente);
                Vehiculo vehiculoAModificar = reservaSeleccionada.Vehiculo;
                vehiculoAModificar.Disponible = true;
                VehiculoDAO.Modificar(vehiculoAModificar, reservaSeleccionada.PatenteVehiculo);
                this.formMain.ListaVehiculos = VehiculoDAO.LeerVehiculos();
                MessageBox.Show("La reserva se canceló con éxito", "Reserva cancelada", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            this.CargarListaVehiculosDisp();
            this.CargarListaReservas();

        }

        private void btnExportarReservas_Click(object sender, EventArgs e)
        {
            ExportarJson(this.ListaReservas.FiltrarReservasVigentes());
        }

        private void ExportarJson(List<Reserva> listaReservas)
        {
            string escritorioPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string carpetaDeLaSolucion = Path.Combine(escritorioPath, "ArchivosIntegrador");
            if (!Directory.Exists(carpetaDeLaSolucion))
            {
                Directory.CreateDirectory(carpetaDeLaSolucion);
            }
            string archivoJson = "reservasVigentes.json";
            string rutaCompleta = Path.Combine(carpetaDeLaSolucion, archivoJson);

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string jsonListaReservas = JsonSerializer.Serialize(listaReservas, options);
            File.WriteAllText(rutaCompleta, jsonListaReservas);
        }

        private void btnImportarVehiculos_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files(*.json)|*.json";
            openFileDialog.ShowDialog();
            ImportarConfig(openFileDialog.FileName, this.formMain.ListaVehiculos);
            this.formMain.ListaVehiculos = VehiculoDAO.LeerVehiculos();
            this.CargarListaVehiculosDisp();
        }

        private void ImportarConfig(string path, List<Vehiculo> listaVehiculosDisp)
        {
            try
            {
                string jsonString = File.ReadAllText(path);

                // Deserializa una lista de objetos de tipo Vehiculo a partir de JSON.
                List<Vehiculo> listaVehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(jsonString);

                foreach (Vehiculo vehiculo in listaVehiculos)
                {
                    if (!this.ValidarPatente(vehiculo, this.formMain.ListaVehiculos))
                    {
                        VehiculoDAO vehiculoDAO = new VehiculoDAO("Vehiculos");
                        vehiculoDAO.Guardar(vehiculo);
                    }
                }
            }

            catch (JsonException)
            {
                MessageBox.Show("El archivo de configuración no se encuentra en el formato correcto.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MostrarMensajeDeError(ex);
            }
        }

        private bool ValidarPatente(Vehiculo vehiculo, List<Vehiculo> listaVehiculos)
        {
            return listaVehiculos.Any(item => item.Patente == vehiculo.Patente);
        }

        private void MostrarMensajeDeError(Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(ex.Message);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(ex.StackTrace);

            MessageBox.Show(stringBuilder.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
