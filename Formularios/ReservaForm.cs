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
using System.Threading;

namespace Formularios
{
    public delegate void DelegateVerificacionPago(string mensaje);
    public delegate void DelegatePagoOk(string mensaje);
    public delegate void DelegatePagoOff();


    public partial class ReservaForm : Form
    {
        private MainForm formMain;
        private List<Reserva> listaReservas;

        //DELEGADO CREADO
        public BuscarPorDniDelegate delegadoBuscarPorDni;

        //EVENTOS
        public event DelegateVerificacionPago OnVerificacionPago;
        public event DelegatePagoOk OnPagoOk;
        public event DelegatePagoOff OnPagoOff;

        //CANCELACION HILO SECUNDARIO
        private CancellationTokenSource cancellation;

        public ReservaForm(MainForm mainForm)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.formMain = mainForm;
            this.delegadoBuscarPorDni = new BuscarPorDniDelegate(this.BuscarPorDni);
        }

        public List<Reserva> ListaReservas { get => this.listaReservas; set => this.listaReservas = value; }

        private void ReservaForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.dtpDesde.MinDate = DateTime.Now;
                this.dtpHasta.MinDate = DateTime.Now.AddDays(1);
                this.ListaReservas = ReservaDAO.LeerReservas();
                this.CargarListaReservas();
                this.CargarListaVehiculosDisp();
                this.OnVerificacionPago += this.MostrarVerificacionPago;
                this.OnPagoOk += this.MostrarPagoOk;
                this.OnPagoOff += this.BorrarPago;
            }
            catch(BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            catch(BaseDeDatosException ex)
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
            catch (ClienteExistenteException ex)
            {
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

            try
            {
                this.lstVehiculosDisp.Items.Clear();
                this.formMain.ListaVehiculos = VehiculoDAO.LeerVehiculos();

                // Filtra los vehículos disponibles y ordena por Tipo utilizando LINQ
                IOrderedEnumerable<Vehiculo>? vehiculosDisponiblesOrdenados = this.formMain.ListaVehiculos
                    .Where(vehiculo => vehiculo.Disponible)
                    .OrderBy(vehiculo => vehiculo.Tipo)
                    .ThenBy(vehiculo => vehiculo.Anio);


                // Agrega los vehículos ordenados al ListBox
                foreach (Vehiculo vehiculo in vehiculosDisponiblesOrdenados)
                {
                    this.lstVehiculosDisp.Items.Add(vehiculo);
                }
            }
            catch (BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Exportar Reservas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnConfirmarReserva_Click(object sender, EventArgs e)
        {
            try
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
                    if (this.ListaReservas is null)
                    {
                        this.ListaReservas = new List<Reserva>();
                    }
                    this.ListaReservas.Add(nuevaReserva);

                    this.ProcesarPago();
                }
                this.CargarListaReservas();
                this.CargarListaVehiculosDisp();
            }
            catch (BaseDeDatosException)
            {
                MessageBox.Show("No existe una base de datos RESERVAS", "Exportar Reservas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void CargarListaReservas()
        {
            this.lstReservas.Items.Clear();
            if(this.listaReservas is not null)
            {
                foreach (Reserva reserva in this.ListaReservas.FiltrarReservasVigentes())
                {
                    this.lstReservas.Items.Add(reserva);
                }
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
            if (this.ListaReservas is not null)
            {
                ExportarJson(this.ListaReservas.FiltrarReservasVigentes());
                MessageBox.Show("Las reservas se exportaron correctamente", "Exportar Reservas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("No hay reservas para exportar", "Exportar Reservas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void ExportarJson(List<Reserva> listaReservas)
        {
            string escritorioPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string carpetaDeLaSolucion = Path.Combine(escritorioPath, "ArchivosIntegrador");
            if (!Directory.Exists(carpetaDeLaSolucion))
            {
                Directory.CreateDirectory(carpetaDeLaSolucion);
            }

            string rutaCompleta = Path.Combine(carpetaDeLaSolucion, "reservasVigentes.json");
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
            try
            {
                this.formMain.ListaVehiculos = VehiculoDAO.LeerVehiculos();
                this.ImportarConfig(openFileDialog.FileName, this.formMain.ListaVehiculos);
            }
            catch (BaseDeDatosException)
            {
                this.ImportarConfigSiListaVacia(openFileDialog.FileName);
            }
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
                    if (!this.ValidarVehiculoExistente(vehiculo, this.formMain.ListaVehiculos))
                    {
                        VehiculoDAO vehiculoDAO = new VehiculoDAO("Vehiculos");
                        vehiculoDAO.Guardar(vehiculo);
                    }
                }
                MessageBox.Show("Vehiculos cargados correctamente", "Importar vehiculos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void ImportarConfigSiListaVacia(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);

                // Deserializa una lista de objetos de tipo Vehiculo a partir de JSON.
                List<Vehiculo> listaVehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(jsonString);

                foreach (Vehiculo vehiculo in listaVehiculos)
                {
                    VehiculoDAO vehiculoDAO = new VehiculoDAO("Vehiculos");
                    vehiculoDAO.Guardar(vehiculo);
                }
                MessageBox.Show("Vehiculos cargados correctamente", "Importar vehiculos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private bool ValidarVehiculoExistente(Vehiculo vehiculo, List<Vehiculo> listaVehiculos)
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

        private void ProcesarPago()
        {
            this.cancellation = new CancellationTokenSource();
            Task.Run(() =>
            {
                do
                {
                    if (this.OnVerificacionPago is not null && this.OnPagoOk is not null)
                    {
                        this.OnVerificacionPago.Invoke("Verificando medio de pago...");
                        Thread.Sleep(3500);
                        this.OnPagoOk.Invoke("El pago se realizó con éxito");
                        Thread.Sleep(2000);
                        this.OnPagoOff.Invoke();
                        this.cancellation.Cancel();
                    }
                } while (!this.cancellation.IsCancellationRequested);
            }, this.cancellation.Token);
        }


        private void MostrarVerificacionPago(string mensaje)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(() => this.MostrarVerificacionPago(mensaje));
            }
            else
            {
                this.lblPago.Visible = true;
                this.lblPago.ForeColor = Color.Red;
                this.lblPago.Text = mensaje;
            }
        }

        private void MostrarPagoOk(string mensaje)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(() => this.MostrarPagoOk(mensaje));
            }
            else
            {
                this.lblPago.ForeColor = Color.Green;
                this.lblPago.Text = mensaje;
            }
        }

        private void BorrarPago()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(() => this.BorrarPago());
            }
            else
            {
                this.lblPago.Visible = false;
            }
        }
    }
}
