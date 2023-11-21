using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.excepciones
{
    /// <summary>
    /// Excepción lanzada cuando ocurre un error relacionado con la base de datos.
    /// </summary>
    public class BaseDeDatosException : Exception
    {
        public BaseDeDatosException(string? message) : base(message)
        {
        }

        public BaseDeDatosException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
