﻿using System;
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
    }
}
