﻿using Entidades.excepciones;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.metodoGenerico
{
    public class MetodoGenerico
    {
        /// <summary>
        /// Busca un elemento en una lista por el valor de una propiedad específica.
        /// </summary>
        /// <typeparam name="T">Tipo de elemento en la lista.</typeparam>
        /// <typeparam name="TPropiedad">Tipo de propiedad a comparar.</typeparam>
        /// <param name="valorPropiedad">Valor de la propiedad a buscar.</param>
        /// <param name="lista">Lista en la que se realizará la búsqueda.</param>
        /// <param name="obtenerValorPropiedad">Función que obtiene el valor de la propiedad del elemento.</param>
        /// <returns>El primer elemento encontrado que coincide con el valor de la propiedad.</returns>
        /// <exception cref="ElementoNoEncontradoException">Se lanza si no se encuentra ningún elemento con el valor de la propiedad
        /// proporcionado.</exception>
        public static T BuscarElementoPorPropiedad<T, TPropiedad>(TPropiedad valorPropiedad, List<T> lista, Func<T, TPropiedad> obtenerValorPropiedad)
            where T:class
        {
            try
            {
                foreach (T elemento in lista)
                {
                    //Verifica si el valor de la propiedad del elemento actual es igual al valor de propiedad proporcionado.
                    //EqualityComparer<TPropiedad>.Default.Equals se utiliza para comparar los valores de manera segura,
                    //incluso si TPropiedad no implementa la interfaz IEqualityComparer.
                    if (EqualityComparer<TPropiedad>.Default.Equals(obtenerValorPropiedad(elemento), valorPropiedad))
                    {
                        return elemento;
                    }
                }

                throw new ElementoNoEncontradoException($"Ningún elemento encontrado para ese valor de propiedad: {valorPropiedad}");
            }
            catch (Exception ex)
            {
                throw new ElementoNoEncontradoException($"Error al buscar elemento de tipo {typeof(T).Name}", ex);
            }
        }
    }
}
