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
    //DELEGADOS
    public delegate void DelegateVerificacionPago(string mensaje);
    public delegate void DelegatePagoOk(string mensaje);
    public delegate void DelegatePagoOff();


    public partial class ReservaForm : Form
    {
        //ATRIBUTOS
        private MainForm formularioMain;
        private List<Reserva> listaReservas;
        public BuscarPorDniDelegate delegadoBuscarPorDni;
        //CANCELACION HILO SECUNDARIO
        private CancellationTokenSource cancellation;

        //EVENTOS
        public event DelegateVerificacionPago OnVerificacionPago;
        public event DelegatePagoOk OnPagoOk;
        public event DelegatePagoOff OnPagoOff;

        //CONSTRUCTOR
        public ReservaForm(MainForm mainForm)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.formularioMain = mainForm;
            this.delegadoBuscarPorDni = new BuscarPorDniDelegate(this.BuscarPorDni);
        }

        //PROPIEDAD
        public List<Reserva> ListaReservas { get => this.listaReservas; set => this.listaReservas = value; }

        /// <summary>
        /// Evento que se dispara cuando el formulario de reserva se carga.
        /// </summary>
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

        /// <summary>
        /// Maneja el evento de clic en el botón de búsqueda de clientes por DNI.
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = this.txtDniCliente.Text;
                int.TryParse(dni, out int numDni);
                Cliente clienteBuscado = this.delegadoBuscarPorDni(numDni, this.formularioMain.ListaClientes);
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

        /// <summary>
        /// Busca un cliente por su número de DNI en la lista de clientes.
        /// </summary>
        /// <param name="numDni">Número de DNI del cliente a buscar.</param>
        /// <param name="listaClientes">Lista de clientes en la que se realizará la búsqueda.</param>
        /// <returns>El objeto Cliente si se encuentra.</returns>
        /// <exception cref="ElementoNoEncontradoException">Se lanza si no se encuentra ningún cliente con el DNI proporcionado.</exception>
        private Cliente BuscarPorDni(int numDni, List<Cliente> listaClientes)
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

        /// <summary>
        /// Carga la lista de vehículos disponibles para alquilar en el ListBox.
        /// </summary>
        private void CargarListaVehiculosDisp()
        {
            try
            {
                this.lstVehiculosDisp.Items.Clear();
                this.formularioMain.ListaVehiculos = VehiculoDAO.LeerVehiculos();

                // Filtra los vehículos disponibles y ordena por Tipo utilizando LINQ
                IOrderedEnumerable<Vehiculo>? vehiculosDisponiblesOrdenados = this.formularioMain.ListaVehiculos
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

        /// <summary>
        /// Confirma la reserva de un vehículo para un cliente, realiza el proceso de guardado en la base de datos y el pago.
        /// </summary>
        private void btnConfirmarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lstVehiculosDisp.SelectedItem is not null)
                {
                    int.TryParse(this.txtDniCliente.Text, out int numDni);
                    Cliente clienteBuscado = this.BuscarPorDni(numDni, this.formularioMain.ListaClientes);

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

        /// <summary>
        /// Carga la lista de reservas que están vigentes en el ListBox de reservas.
        /// </summary>
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

        /// <summary>
        /// Cancela la reserva seleccionada, marcando la reserva como no vigente y liberando el vehículo asociado.
        /// </summary>
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
                this.formularioMain.ListaVehiculos = VehiculoDAO.LeerVehiculos();
                MessageBox.Show("La reserva se canceló con éxito", "Reserva cancelada", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            this.CargarListaVehiculosDisp();
            this.CargarListaReservas();
        }

        /// <summary>
        /// Exporta las reservas vigentes a un archivo JSON y muestra un mensaje de éxito o advertencia.
        /// </summary>
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

        /// <summary>
        /// Exporta la lista de reservas proporcionada a un archivo JSON en el escritorio del usuario.
        /// </summary>
        /// <param name="listaReservas">Lista de reservas a exportar.</param>
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

        /// <summary>
        /// Maneja el evento de importación de vehículos desde un archivo JSON seleccionado por el usuario si existe una lista de vehículos.
        /// </summary>
        private void btnImportarVehiculos_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files(*.json)|*.json";
            openFileDialog.ShowDialog();
            try
            {
                this.formularioMain.ListaVehiculos = VehiculoDAO.LeerVehiculos();
                this.ImportarConfig(openFileDialog.FileName, this.formularioMain.ListaVehiculos);
            }
            catch (BaseDeDatosException)
            {
                this.ImportarConfigSiListaVacia(openFileDialog.FileName);
            }
            this.CargarListaVehiculosDisp();
        }

        /// <summary>
        /// Importa la configuración de vehículos desde un archivo JSON y guarda aquellos que no existen en la lista proporcionada.
        /// </summary>
        /// <param name="path">Ruta del archivo JSON a importar.</param>
        /// <param name="listaVehiculosDisp">Lista de vehículos disponibles.</param>
        private void ImportarConfig(string path, List<Vehiculo> listaVehiculosDisp)
        {
            try
            {
                string jsonString = File.ReadAllText(path);

                // Deserializa una lista de objetos de tipo Vehiculo a partir de JSON.
                List<Vehiculo> listaVehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(jsonString);

                foreach (Vehiculo vehiculo in listaVehiculos)
                {
                    if (!this.ValidarVehiculoExistente(vehiculo, this.formularioMain.ListaVehiculos))
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

        /// <summary>
        /// Importa la configuración de vehículos desde un archivo JSON si la lista de vehículos está vacía.
        /// </summary>
        /// <param name="path">Ruta del archivo JSON a importar.</param>
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

        /// <summary>
        /// Valida si un vehículo ya existe en la lista de vehículos proporcionada, a través de su patente.
        /// </summary>
        /// <param name="vehiculo">Vehículo a validar.</param>
        /// <param name="listaVehiculos">Lista de vehículos en la que se realiza la búsqueda.</param>
        /// <returns>True si el vehículo ya existe en la lista, False en caso contrario.</returns>
        private bool ValidarVehiculoExistente(Vehiculo vehiculo, List<Vehiculo> listaVehiculos)
        {
            return listaVehiculos.Any(item => item.Patente == vehiculo.Patente);
        }

        /// <summary>
        /// Muestra un mensaje de error con detalles de la excepción proporcionada.
        /// </summary>
        /// <param name="ex">Excepción que se desea mostrar.</param>
        private void MostrarMensajeDeError(Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(ex.Message);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(ex.StackTrace);

            MessageBox.Show(stringBuilder.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Inicia el procesamiento simulado de un pago utilizando eventos de verificación, éxito y finalización del pago.
        /// </summary>
        private void ProcesarPago()
        {
            // Inicia una nueva tarea en un hilo secundario.
            // Utiliza un ciclo do-while para simular la verificación, éxito y finalización del pago.
            this.cancellation = new CancellationTokenSource();
            Task.Run(() =>
            {
                do
                {
                    if (this.OnVerificacionPago is not null && this.OnPagoOk is not null)
                    {
                        // Invoca el evento de verificación del pago y espera simulada.
                        this.OnVerificacionPago.Invoke("Verificando medio de pago...");
                        Thread.Sleep(3500);
                        // Invoca el evento de éxito del pago.
                        this.OnPagoOk.Invoke("El pago se realizó con éxito");
                        Thread.Sleep(2000);
                        // Invoca el evento de finalización del pago y cancela la tarea.
                        this.OnPagoOff.Invoke();
                        this.cancellation.Cancel();
                    }
                } while (!this.cancellation.IsCancellationRequested);
            }, this.cancellation.Token);
        }

        /// <summary>
        /// Método manejador del evento OnVerificacionPago que muestra un mensaje de verificación de pago en el formulario.
        /// </summary>
        /// <param name="mensaje">Mensaje de verificación de pago.</param>
        private void MostrarVerificacionPago(string mensaje)
        {
            // Verifica si es necesario invocar el método en el hilo de la interfaz de usuario.
            if (this.InvokeRequired)
            {
                // Invoca el método en el hilo de la interfaz de usuario utilizando.
                this.BeginInvoke(() => this.MostrarVerificacionPago(mensaje));
            }
            else
            {
                this.lblPago.Visible = true;
                this.lblPago.ForeColor = Color.Red;
                this.lblPago.Text = mensaje;
            }
        }

        /// <summary>
        /// Método manejador del evento OnPagoOK que muestra un mensaje de éxito de pago en el formulario.
        /// </summary>
        /// <param name="mensaje">Mensaje de éxito de pago.</param>
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

        /// <summary>
        /// Método manejador del evento OnPagoOff que finaliza el pago y oculta el mensaje en el formulario.
        /// </summary>
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
