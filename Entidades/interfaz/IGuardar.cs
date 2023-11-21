using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.interfaz
{
    /// <summary>
    /// Define un contrato para las clases que pueden realizar operaciones de guardado en la base de datos.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad que se va a guardar.</typeparam>
    public interface IGuardar<T> where T : class
    {
        /// <summary>
        /// Guarda la entidad en la base de datos.
        /// </summary>
        /// <param name="entidad">La entidad que se va a guardar, de tipo genérica.</param>
        void Guardar(T entidad);
    }
}
