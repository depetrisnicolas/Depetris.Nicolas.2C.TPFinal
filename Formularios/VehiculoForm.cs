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

namespace Formularios
{
    public partial class VehiculoForm : Form
    {
        public VehiculoForm()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string marca = ValidarCadena(this.txtMarca.Text);
            string modelo = ValidarCadena(this.txtModelo.Text);
            string anio = ValidarAnio(this.txtAnio.Text);
            string tipo = this.cmbTipo.SelectedItem.ToString();
            string patente = ValidarCadena(this.txtPatente.Text);




            if (string.IsNullOrEmpty(marca) || string.IsNullOrEmpty(modelo) || string.IsNullOrEmpty(anio) || string.IsNullOrEmpty(tipo)
                || string.IsNullOrEmpty(patente))
            {
                MessageBox.Show("Datos erróneos o incompletos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int.TryParse(anio, out int numAnio);
                Vehiculo nuevoVehiculo = new Vehiculo(marca, modelo, numAnio, tipo, patente);
                VehiculoDAO.Guardar(nuevoVehiculo);
            }
        }

        private string ValidarAnio(string anio)
        {
            if (!Regex.IsMatch(anio, "^[0-9]{4}$"))
            {
                return null;
            }
            return anio;

        }

        private string ValidarCadena(string patente)
        {
            if (!Regex.IsMatch(patente, "^[a-zA-Z0-9]+$"))
            {
                return null;
            }
            return patente;

        }

        private void txtPatente_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
