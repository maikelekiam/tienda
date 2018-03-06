using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;

namespace CapaRepositorio
{
    public class DetallePedidoRepo
    {
        public IEnumerable<DetallePedido> MostrarDetallePedidos()
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IEnumerable<DetallePedido> result = modeloDeDominio.DetallePedidos.ToList();
                return result;
            }
        }
        public void GuardarDetallePedido(DetallePedido detallePedido)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                modeloDeDominio.Add(detallePedido);
                modeloDeDominio.SaveChanges();
            }
        }
    }
}
