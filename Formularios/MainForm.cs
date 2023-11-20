using Entidades;
using Entidades.excepciones;
using Entidades.sql;
using System.Net;

namespace Formularios
{
    public partial class MainForm : Form
    {
        private ClienteForm formCliente;
        private VehiculoForm formVehiculo;
        private ReservaForm formReserva;
        private List<Cliente> listaClientes;
        private List<Vehiculo> listaVehiculos;
        private string imagen = @"https://money-tourism.gr/wp-content/uploads/2017/03/RENTACAR.jpg";


        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        public List<Cliente> ListaClientes { get => this.listaClientes; set => this.listaClientes = value; }
        public List<Vehiculo> ListaVehiculos { get => this.listaVehiculos; set => this.listaVehiculos = value; }


        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            this.formCliente = new ClienteForm(this);
            this.formCliente.ShowDialog();
        }

        private void btnAgregarVehiculo_Click(object sender, EventArgs e)
        {
            this.formVehiculo = new VehiculoForm(this);
            this.formVehiculo.ShowDialog();
        }

        private void btnReserva_Click(object sender, EventArgs e)
        {
            try
            {
                this.ListaVehiculos = VehiculoDAO.LeerVehiculos();
                this.formReserva = new ReservaForm(this);
                this.formReserva.ShowDialog();
            }
            catch (ClienteExistenteException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ListaClientes = ClienteDAO.LeerClientes();
            this.pictureCar.Load(this.imagen);
        }
    }
}