using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;
using CapaRepositorio;

namespace CapaNegocio
{
    public class PresupuestoNego
    {
        PresupuestoRepo presupuestoRepo = new PresupuestoRepo();

        public IEnumerable<PresupuestoTemporal> MostrarPresupuestosTemporal()
        {
            return presupuestoRepo.MostrarPresupuestosTemporal();
        }

        public void GuardarPresupuestoTemporal(PresupuestoTemporal presupuestoTemporal)
        {
            presupuestoRepo.GuardarPresupuestoTemporal(presupuestoTemporal);
        }
        public PresupuestoTemporal FiltrarPresupuestoTemporalSegunProducto(string cod, int idUsu)
        {
            return presupuestoRepo.FiltrarPresupuestoTemporalSegunProducto(cod, idUsu);
        }
        public void ActualizarPresupuestoTemporal(PresupuestoTemporal presupuestoTemporal)
        {
            presupuestoRepo.ActualizarPresupuestoTemporal(presupuestoTemporal);
        }
        public PresupuestoTemporal ObtenerPresupuestoTemporal(int id)
        {
            return presupuestoRepo.ObtenerPresupuestoTemporal(id);
        }
        public void BorrarPresupuestoTemporal(PresupuestoTemporal presupuestoTemporal)
        {
            presupuestoRepo.BorrarPresupuestoTemporal(presupuestoTemporal);
        }
        public void BorrarListaPresupuestoTemporal(int idUsu)
        {
            presupuestoRepo.BorrarListaPresupuestoTemporal(idUsu);
        }
    }
}
