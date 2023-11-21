using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.metodoExtension
{
    public static class MetodoDeExtensionLista
    {
        /// <summary>
        /// Filtra las reservas vigentes de una lista de reservas.
        /// </summary>
        /// <param name="listaReservas">La lista de reservas a filtrar.</param>
        /// <returns>Una nueva lista que contiene solo las reservas vigentes.</returns>
        public static List<Reserva> FiltrarReservasVigentes(this List<Reserva> listaReservas)
        {
            List<Reserva> listaReservasVigentes = new List<Reserva>();
            foreach (Reserva reserva in listaReservas) 
            {
                if (reserva.Vigente)
                {
                    listaReservasVigentes.Add(reserva);
                }
            }
            return listaReservasVigentes;       
        }
    }
}
    