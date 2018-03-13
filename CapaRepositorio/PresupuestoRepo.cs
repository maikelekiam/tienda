using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;

namespace CapaRepositorio
{
    public class PresupuestoRepo
    {
        public IEnumerable<PresupuestoTemporal> MostrarPresupuestosTemporal()
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IEnumerable<PresupuestoTemporal> result = modeloDeDominio.PresupuestoTemporals.ToList();
                return result;
            }
        }
        public void GuardarPresupuestoTemporal(PresupuestoTemporal presupuestoTemporal)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                modeloDeDominio.Add(presupuestoTemporal);
                modeloDeDominio.SaveChanges();
            }
        }
        public PresupuestoTemporal FiltrarPresupuestoTemporalSegunProducto(int id, int idUsu)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                PresupuestoTemporal result = modeloDeDominio.PresupuestoTemporals.Where(c => c.IdProducto == id).Where(d => d.IdUsuario == idUsu).FirstOrDefault();
                return result;
            }
        }
        public void ActualizarPresupuestoTemporal(PresupuestoTemporal presupuestoTemporal)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                modeloDeDominio.AttachCopy(presupuestoTemporal);
                modeloDeDominio.SaveChanges();
            }
        }
        public PresupuestoTemporal ObtenerPresupuestoTemporal(int id)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                PresupuestoTemporal result = modeloDeDominio.PresupuestoTemporals.Where(c => c.IdPresupuestoTemporal == id).FirstOrDefault();
                return result;
            }
        }
        public void BorrarPresupuestoTemporal(PresupuestoTemporal presupuestoTemporal)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IQueryable<PresupuestoTemporal> query = modeloDeDominio.GetAll<PresupuestoTemporal>().Where(c => c.IdPresupuestoTemporal == presupuestoTemporal.IdPresupuestoTemporal);

                foreach (PresupuestoTemporal presu in query)
                {
                    modeloDeDominio.Delete(presu);
                    modeloDeDominio.SaveChanges();
                }
            }
        }
        public void BorrarListaPresupuestoTemporal(int idUsu)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IList<PresupuestoTemporal> query = modeloDeDominio.PresupuestoTemporals.Where(c => c.IdUsuario == idUsu).ToList();

                foreach (PresupuestoTemporal dpt in query)
                {
                    modeloDeDominio.Delete(dpt);
                    modeloDeDominio.SaveChanges();
                }
            }
        }
    }
}
