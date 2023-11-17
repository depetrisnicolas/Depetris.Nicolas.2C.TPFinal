﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.interfaz
{
    public interface IGuardar<T> where T : class
    {
        void Guardar(T entidad);
    }
}