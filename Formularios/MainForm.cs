using Entidades;
using Entidades.excepciones;
using Entidades.sql;
using System.Net;

namespace Formularios
{
    public partial class MainForm : Form
    {
        //ATRIBUTOS
        private ClienteForm formCliente;
        private VehiculoForm formVehiculo;
        private ReservaForm formReserva;
        private List<Cliente> listaClientes;
        private List<Vehiculo> listaVehiculos;
        private string imagen = @"https://money-tourism.gr/wp-content/uploads/2017/03/RENTACAR.jpg";

        //CONSTRUCTOR
        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //PROPIEDADES
        public List<Cliente> ListaClientes { get => this.listaClientes; set => this.listaClientes = value; }
        public List<Vehiculo> ListaVehiculos { get => this.listaVehiculos; set => this.listaVehiculos = value; }


        /// <summary>
        /// Abre el formulario para agregar un nuevo cliente.
        /// </summary>
        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            this.formCliente = new ClienteForm(this);
            this.formCliente.ShowDialog();
        }

        /// <summary>
        /// Abre el formulario para agregar un nuevo vehículo.
        /// </summary>
        private void btnAgregarVehiculo_Click(object sender, EventArgs e)
        {
            this.formVehiculo = new VehiculoForm(this);
            this.formVehiculo.ShowDialog();
        }

        /// <summary>
        /// Abre el formulario para ver las reservas vigentes y realizar una nueva.
        /// </summary>
        private void btnReserva_Click(object sender, EventArgs e)
        {
            try
            {
                this.formReserva = new ReservaForm(this);
                this.formReserva.ShowDialog();
            }
            catch (BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga el formulario principal de la aplicación.
        /// <exception cref="BaseDeDatosException">Se lanza si ocurre un error al interactuar con la base de datos.</exception>
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {        
            this.pictureCar.Load(this.imagen);        
            try
            {
                this.ListaClientes = ClienteDAO.LeerClientes();
                this.ListaVehiculos = VehiculoDAO.LeerVehiculos();
            }
            catch (BaseDeDatosException)
            {
                MessageBox.Show("Error de conexión con la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}