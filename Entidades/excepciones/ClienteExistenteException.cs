using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.excepciones
{
    public class ClienteExistenteException : Exception
    {
        public ClienteExistenteException(string? message) : base(message)
        {
        }

        public ClienteExistenteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
