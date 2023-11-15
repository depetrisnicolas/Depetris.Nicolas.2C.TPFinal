using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Reserva
    {
        private Cliente datosCliente;
        private Vehiculo vehiculo;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private static float tarifaAuto;
        private static float tarifaCamioneta;

        public Reserva(Cliente cliente, Vehiculo vehiculo, DateTime fechaInicio, DateTime fechaFin)
        {
            this.datosCliente = cliente;
            this.vehiculo = vehiculo;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            Reserva.tarifaAuto = 20000;
            Reserva.tarifaCamioneta = 35000;
        }

        public Cliente DatosCliente { get => this.datosCliente; set => this.datosCliente = value; }
        public Vehiculo Vehiculo { get => this.vehiculo; set => this.vehiculo = value; }
        public DateTime FechaInicio { get => this.fechaInicio; set => this.fechaInicio = value; }
        public DateTime FechaFin { get => this.fechaFin; set => this.fechaFin = value; }
        public static float TarifaAuto { get => tarifaAuto; }
        public static float TarifaCamioneta { get => tarifaCamioneta; }

        public override string ToString()
        {
            return $@"{this.DatosCliente.ToString()} | {this.Vehiculo.ToString()} | {this.FechaInicio.ToString("dd/MM/yyyy")} al {this.FechaFin.ToString("dd/MM/yyyy")} | ${this.CalcularCostoReserva()}";
        }

        private float CalcularCostoReserva()
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

            return 0; // Valor por defecto si el tipo de vehículo no coincide con "Auto" ni "Camioneta"
        }
    }
}
