using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;
using CapaRepositorio;

namespace CapaNegocio
{
    public class DetallePedidoNego
    {
        DetallePedidoRepo detallePedidoRepo = new DetallePedidoRepo();

        public IEnumerable<DetallePedido> MostrarDetallePedidos()
        {
            return detallePedidoRepo.MostrarDetallePedidos();
        }

        public void GuardarDetallePedido(DetallePedido detallePedido)
        {
            detallePedidoRepo.GuardarDetallePedido(detallePedido);
        }
    }
}
