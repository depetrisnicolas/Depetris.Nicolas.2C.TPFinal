using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.interfaz
{
    /// <summary>
    /// Define un contrato para clases que requieren conexión a una base de datos.
    /// </summary>
    public interface IConexionABase
    {
        /// <summary>
        /// Cadena de conexión a la base de datos.
        /// </summary>
        static string StringConnection { get; set; }
    }
}
