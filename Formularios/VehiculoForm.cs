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

        /// <summary>
        /// Realiza la validación de los datos ingresados y guarda un nuevo vehiculo en la base de datos.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string marca = this.delegadoValidarAlfanumericos(this.txtMarca.Text);
            string modelo = this.delegadoValidarAlfanumericos(this.txtModelo.Text);
            string anio = this.ValidarAnio(this.txtAnio.Text);
            string tipo = this.cmbTipo.SelectedItem?.ToString();
            string patente = ValidarPatente(this.txtPatente.Text);

            this.LimpiarErrores();

            try
            {
                ValidarDatosVehiculo(marca, modelo, anio, tipo, patente);
            }
            catch (VehiculoExistenteException ex)
            {
                MessageBox.Show(ex.Message, "Alta Vehiculo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }         
        }

        /// <summary>
        /// Valida los datos del vehiculo y guarda la información en la base de datos.
        /// </summary>
        /// <param name="marca">Marca del vehículo.</param>
        /// <param name="modelo">Modelo del vehículo.</param>
        /// <param name="anio">Año del vehículo.</param>
        /// <param name="tipo">Tipo del vehículo.</param>
        /// <param name="patente">Patente del vehículo.</param>
        /// <exception cref="VehiculoExistenteException">Se lanza si el vehículo cargado ya existe en la base de datos.</exception>
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
                try
                {
                    int.TryParse(anio, out int numAnio);
                    Vehiculo nuevoVehiculo = new Vehiculo(marca, modelo, numAnio, tipo, patente, true);

                    //Si todavía no hay vehículos guardados en la base de datos
                    if (this.formMain.ListaVehiculos is null)
                    {
                        this.formMain.ListaVehiculos = new List<Vehiculo>();
                        VehiculoDAO vehiculoDAO = new VehiculoDAO("Vehiculos");
                        vehiculoDAO.Guardar(nuevoVehiculo);
                        this.formMain.ListaVehiculos.Add(nuevoVehiculo);
                        MessageBox.Show("El vehiculo se guardó correctamente", "Alta Vehiculo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                    //Si la lista de vehículos ya fue creada verifica que el vehículo no exista
                    else if (!this.ValidarVehiculoExistente(nuevoVehiculo, this.formMain.ListaVehiculos))
                    {
                        this.formMain.ListaVehiculos = VehiculoDAO.LeerVehiculos();
                        VehiculoDAO vehiculoDAO = new VehiculoDAO("Vehiculos");
                        vehiculoDAO.Guardar(nuevoVehiculo);
                        this.formMain.ListaVehiculos.Add(nuevoVehiculo);
                        MessageBox.Show("El vehiculo se guardó correctamente", "Alta Vehiculo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                    else
                    {
                        throw new VehiculoExistenteException("El vehiculo ya existe en la base de datos");
                    }
                }
                catch (BaseDeDatosException)
                {
                    MessageBox.Show("Error al guardar el vehiculo en una base inexistente", "Alta Vehiculo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Valida el año del vehículo antes de asignarlo, asegurándose de que sea un formato de cuatro dígitos.
        /// </summary>
        /// <param name="anio">Año del vehículo como cadena.</param>
        /// <returns>El año validado o null si el formato no es correcto.</returns>
        private string ValidarAnio(string anio)
        {
            if (!Regex.IsMatch(anio, "^[1-2][0-9]{3}$"))
            {
                return null;
            }
            return anio;
        }

        /// <summary>
        /// Valida la patente del vehículo antes de asignarla, asegurándose de que cumpla con uno de los dos formatos permitidos.
        /// </summary>
        /// <param name="patente">Patente del vehículo como cadena.</param>
        /// <returns>La patente validada o null si el formato no es correcto.</returns>
        private string ValidarPatente(string patente)
        {
            if (!Regex.IsMatch(patente, "^[A-Z]{3}[0-9]{3}$|^[A-Z]{2}[0-9]{3}[A-Z]{2}$"))
            {
                return null;
            }
            return patente;
        }

        /// <summary>
        /// Valida si un vehículo ya existe en la lista de vehículos.
        /// </summary>
        /// <param name="vehiculo">Vehículo a validar.</param>
        /// <param name="listaVehiculos">Lista de vehículos donde realizar la búsqueda.</param>
        /// <returns>True si el vehículo ya existe en la lista; de lo contrario, False.</returns>
        private bool ValidarVehiculoExistente(Vehiculo vehiculo, List<Vehiculo> listaVehiculos)
        {
            return listaVehiculos.Any(item => item.Patente == vehiculo.Patente);
        }

        /// <summary>
        /// Limpia los mensajes de error en el formulario de vehiculos.
        /// </summary>
        private void LimpiarErrores()
        {
            this.lblErrorMarca.Text = "";
            this.lblErrorModelo.Text = "";
            this.lblErrorAnio.Text = "";
            this.lblErrorTipo.Text = "";
            this.lblErrorPatente.Text = "";
        }
    }
}
