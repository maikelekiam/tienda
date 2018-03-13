using System;
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
        public void ActualizarUsuario(Usuario usuario)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                modeloDeDominio.AttachCopy(usuario);
                modeloDeDominio.SaveChanges();
            }
        }
        public void ActualizarMargenUsuario(int margen, int id)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                Usuario usuario = modeloDeDominio.Usuarios.Where(c => c.IdUsuario == id).First();

                usuario.Margen = margen;

                modeloDeDominio.SaveChanges();
            }
        }
        public IEnumerable<Usuario> MostrarUsuarios()
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IEnumerable<Usuario> result = modeloDeDominio.Usuarios.ToList();
                return result;
            }
        }
        public void GuardarUsuario(Usuario usuario)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                modeloDeDominio.Add(usuario);
                modeloDeDominio.SaveChanges();
            }
        }
        public Usuario ObtenerUsuario(int id)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                Usuario usuario = modeloDeDominio.Usuarios.Where(c => c.IdUsuario == id).FirstOrDefault();

                return usuario;
            }
        }
    }
}
