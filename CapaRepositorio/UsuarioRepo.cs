﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;

namespace CapaRepositorio
{
    public class UsuarioRepo
    {
        public Usuario ObtenerUsuario(String nombre, String contrasenia)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                return modeloDeDominio.Usuarios.Where(c => c.Nombre == nombre && c.Contrasenia == contrasenia).FirstOrDefault();

            }
        }
    }
}
