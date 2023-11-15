using Entidades;
namespace Formularios
{
    public partial class MainForm : Form
    {
        private ClienteForm formCliente;
        private VehiculoForm formVehiculo;
        private ReservaForm formReserva;
        private List<Cliente> listaClientes = new List<Cliente>();

        public MainForm()
        {
            InitializeComponent();
        }

        public List<Cliente> ListaClientes { get => this.listaClientes; set => this.listaClientes = value; }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            this.formCliente = new ClienteForm(this);
            this.formCliente.ShowDialog();
        }

        private void btnAgregarVehiculo_Click(object sender, EventArgs e)
        {
            this.formVehiculo = new VehiculoForm();
            this.formVehiculo.ShowDialog();
        }

        private void btnReserva_Click(object sender, EventArgs e)
        {
            this.formReserva = new ReservaForm(this);
            this.formReserva.ShowDialog();
        }
    }
}