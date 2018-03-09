﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaRepositorio;
using CapaDominio;

namespace CapaNegocio
{
    public class UsuarioNego
    {
        UsuarioRepo usuarioRepo = new UsuarioRepo();

        public Usuario ObtenerUsuario(String nombre, String contrasenia)
        {
            return usuarioRepo.ObtenerUsuario(nombre, contrasenia);
        }
        public void ActualizarUsuario(Usuario usuario)
        {
            usuarioRepo.ActualizarUsuario(usuario);
        }
        public void ActualizarMargenUsuario(int margen, int id)
        {
            usuarioRepo.ActualizarMargenUsuario(margen, id);
        }
    }
}
