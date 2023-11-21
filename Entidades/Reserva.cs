using Entidades.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    
    public class Reserva 
    {
        //ATRIBUTOS
        private Cliente cliente;
        private int dniCliente;
        private Vehiculo vehiculo;
        private string patenteVehiculo;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private static float tarifaAuto;
        private static float tarifaCamioneta;
        private bool vigente;
     
        //CONSTRUCTOR
        public Reserva(Cliente cliente, int dniCliente, Vehiculo vehiculo, string patenteVehiculo, DateTime fechaInicio, DateTime fechaFin, bool vigente)
        {
            this.cliente = cliente;
            this.dniCliente = dniCliente;
            this.vehiculo = vehiculo;
            this.patenteVehiculo = patenteVehiculo;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            Reserva.tarifaAuto = 20000;
            Reserva.tarifaCamioneta = 35000;
            this.vigente = vigente;
        }

        //PROPIEDADES
        public Cliente Cliente { get => this.cliente; set => this.cliente = value; }
        public int DniCliente { get => this.dniCliente; set => this.dniCliente = value; }
        public Vehiculo Vehiculo { get => this.vehiculo; set => this.vehiculo = value; }
        public string PatenteVehiculo { get => this.patenteVehiculo; set => this.patenteVehiculo = value; }
        public DateTime FechaInicio { get => this.fechaInicio; set => this.fechaInicio = value; }
        public DateTime FechaFin { get => this.fechaFin; set => this.fechaFin = value; }
        public static float TarifaAuto { get => Reserva.tarifaAuto; }
        public static float TarifaCamioneta { get => Reserva.tarifaCamioneta; }
        public bool Vigente { get => this.vigente; set => this.vigente = value; }


        //SOBRECARGA PARA MOSTRAR LOS DATOS DE LA RESERVA
        public override string ToString()
        {
            return $@"{this.Cliente} | {this.Vehiculo} | {this.FechaInicio.ToString("dd/MM/yyyy")} al {this.FechaFin.ToString("dd/MM/yyyy")} - Costo Total: ${this.CalcularCostoReserva()}";
        }

        /// <summary>
        /// Calcula el costo total de la reserva en función de la tarifa diaria del vehículo según su tipo y la duración de la reserva en días.
        /// </summary>
        /// <returns>Costo total de la reserva.</returns>
        public float CalcularCostoReserva()
        {
            int diasDiferencia = (int)(this.FechaFin.Date - this.FechaInicio.Date).TotalDays;

            if (this.Vehiculo.Tipo == "Auto")
            {
                return Reserva.TarifaAuto * diasDiferencia;
            }
            else if (this.Vehiculo.Tipo == "Camioneta")
            {
                return Reserva.TarifaCamioneta * diasDiferencia;
            }

            return 0; 
        }
    }
}
