using Entidades.excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.metodoGenerico
{
    public class MetodoGenerico
    {
        public static T LeerElementoPorPropiedad<T, TPropiedad>(TPropiedad valor, List<T> lista, Func<T, TPropiedad> propiedad) where T:class
        {
            try
            {
                foreach (T elemento in lista)
                {
                    if (EqualityComparer<TPropiedad>.Default.Equals(propiedad(elemento), valor))
                    {
                        return elemento;
                    }
                }

                throw new ElementoNoEncontradoException($"Ningún elemento encontrado para ese valor de propiedad: {valor}");
            }
            catch (Exception ex)
            {
                throw new ElementoNoEncontradoException($"Error al buscar elemento de tipo {typeof(T).Name}", ex);
            }
        }
    }
}
