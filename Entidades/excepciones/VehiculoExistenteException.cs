using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.excepciones
{
    /// <summary>
    /// Excepción lanzada cuando un vehiculo ya existe en la base de datos.
    /// </summary>
    public class VehiculoExistenteException : Exception
    {
        public VehiculoExistenteException(string? message) : base(message)
        {
        }

        public VehiculoExistenteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
