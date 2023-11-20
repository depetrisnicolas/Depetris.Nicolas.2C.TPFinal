using Entidades.sql;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using Entidades.excepciones;

namespace Formularios
{
    public partial class VehiculoForm : Form
    {
        private MainForm formMain;
        private ValidarCaractAlfanumericosDelegate delegadoValidarAlfanumericos;
        public VehiculoForm(MainForm mainForm)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.formMain = mainForm;
            this.delegadoValidarAlfanumericos = new ValidarCaractAlfanumericosDelegate(cadena =>
                Regex.IsMatch(cadena, "^[a-zA-Z0-9]+$") ? cadena : null);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string marca = this.delegadoValidarAlfanumericos(this.txtMarca.Text);
            string modelo = this.delegadoValidarAlfanumericos(this.txtModelo.Text);
            string anio = this.ValidarAnio(this.txtAnio.Text);
            string tipo = this.cmbTipo.SelectedItem?.ToString();
            string patente = ValidarPatente(this.txtPatente.Text);

            this.lblErrorMarca.Text = "";
            this.lblErrorModelo.Text = "";
            this.lblErrorAnio.Text = "";
            this.lblErrorTipo.Text = "";
            this.lblErrorPatente.Text = "";

            try
            {
                ValidarDatosVehiculo(marca, modelo, anio, tipo, patente);
            }
            catch (VehiculoExistenteException ex)
            {
                MessageBox.Show(ex.Message, "Alta Vehiculo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void ValidarDatosVehiculo(string marca, string modelo, string anio, string tipo, string patente)
        {
            if (string.IsNullOrEmpty(marca))
            {
                this.lblErrorMarca.Text = "Marca inválida";
            }
            else if (string.IsNullOrEmpty(modelo))
            {
                this.lblErrorModelo.Text = "Modelo inválido";
            }
            else if (string.IsNullOrEmpty(anio))
            {
                this.lblErrorAnio.Text = "Año inválido";
            }
            else if (string.IsNullOrEmpty(tipo))
            {
                this.lblErrorTipo.Text = "Tipo inválido";
            }
            else if (string.IsNullOrEmpty(patente))
            {
                this.lblErrorPatente.Text = "Patente inválida";
            }
            else
            {
                int.TryParse(anio, out int numAnio);
                Vehiculo nuevoVehiculo = new Vehiculo(marca, modelo, numAnio, tipo, patente, true);

                if (!this.ValidarVehiculoExistente(nuevoVehiculo, this.formMain.ListaVehiculos))
                {
                    VehiculoDAO vehiculoDAO = new VehiculoDAO("Vehiculos");
                    vehiculoDAO.Guardar(nuevoVehiculo);
                    MessageBox.Show("El vehiculo se guardó correctamente", "Alta Vehiculo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
                }
                else
                {
                    throw new VehiculoExistenteException("El vehiculo ya existe en la base de datos");
                }
            }
        }

        private string ValidarAnio(string anio)
        {
            if (!Regex.IsMatch(anio, "^[1-2][0-9]{3}$"))
            {
                return null;
            }
            return anio;

        }

        private string ValidarPatente(string patente)
        {
            if (!Regex.IsMatch(patente, "^[A-Z]{3}[0-9]{3}$|^[A-Z]{2}[0-9]{3}[A-Z]{2}$"))
            {
                return null;
            }
            return patente;
        }

        private bool ValidarVehiculoExistente(Vehiculo vehiculo, List<Vehiculo> listaVehiculos)
        {
            return listaVehiculos.Any(item => item.Patente == vehiculo.Patente);
        }

        private void VehiculoForm_Load(object sender, EventArgs e)
        {
            this.formMain.ListaVehiculos = VehiculoDAO.LeerVehiculos();
        }
    }
}
