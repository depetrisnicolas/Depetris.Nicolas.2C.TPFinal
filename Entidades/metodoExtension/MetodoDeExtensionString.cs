using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.metodoExtension
{
    public static class MetodoDeExtensionString
    {
        public static string AgregarPrefijo(this string cadena, string prefijo)
        {
            return $"{prefijo}{cadena}";
        }
    }
}
