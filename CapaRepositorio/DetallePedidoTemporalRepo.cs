using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;

namespace CapaRepositorio
{
    public class DetallePedidoTemporalRepo
    {
        public IEnumerable<DetallePedidoTemporal> MostrarDetallePedidosTemporal()
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IEnumerable<DetallePedidoTemporal> result = modeloDeDominio.DetallePedidoTemporals.ToList();
                return result;
            }
        }
        public void GuardarDetallePedidoTemporal(DetallePedidoTemporal detallePedidoTemporal)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                modeloDeDominio.Add(detallePedidoTemporal);
                modeloDeDominio.SaveChanges();
            }
        }
        public void BorrarListaDetallePedidoTemporal()
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IQueryable<DetallePedidoTemporal> query = modeloDeDominio.GetAll<DetallePedidoTemporal>();

                foreach (DetallePedidoTemporal dpt in query)
                {
                    modeloDeDominio.Delete(dpt);
                    modeloDeDominio.SaveChanges();
                }
            }
        }
        public void BorrarDetallePedidoTemporal(DetallePedidoTemporal detallePedidoTemporal)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IQueryable<DetallePedidoTemporal> query = modeloDeDominio.GetAll<DetallePedidoTemporal>().Where(c=>c.IdDetallePedidoTemporal==detallePedidoTemporal.IdDetallePedidoTemporal);

                foreach (DetallePedidoTemporal dpt in query)
                {
                    modeloDeDominio.Delete(dpt);
                    modeloDeDominio.SaveChanges();
                }
            }
        }
        public int TraerMenorIndice()
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                int result = modeloDeDominio.DetallePedidoTemporals.First().IdDetallePedidoTemporal;
                return result;
            }
        }
        public DetallePedidoTemporal ObtenerDetallePedidoTemporal(int id)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                DetallePedidoTemporal result = modeloDeDominio.DetallePedidoTemporals.Where(c => c.IdDetallePedidoTemporal == id).FirstOrDefault();
                return result;
            }
        }
        public void ActualizarDetallePedidoTemporal(DetallePedidoTemporal detallePedidoTemporal)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                modeloDeDominio.AttachCopy(detallePedidoTemporal);
                modeloDeDominio.SaveChanges();
            }
        }
        public DetallePedidoTemporal FiltrarDetallePedidoTemporalSegunProducto(int id)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                DetallePedidoTemporal result = modeloDeDominio.DetallePedidoTemporals.Where(c => c.IdProducto == id).FirstOrDefault();
                return result;
            }
        }
    }
}
