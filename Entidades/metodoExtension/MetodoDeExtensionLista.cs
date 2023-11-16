using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.metodoExtension
{
    public static class MetodoDeExtensionLista
    {
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
    