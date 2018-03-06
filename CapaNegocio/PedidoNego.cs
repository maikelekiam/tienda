using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;
using CapaRepositorio;

namespace CapaNegocio
{
    public class PedidoNego
    {
        PedidoRepo pedidoRepo = new PedidoRepo();

        public IEnumerable<Pedido> MostrarPedidos()
        {
            return pedidoRepo.MostrarPedidos();
        }

        public int GuardarPedido(Pedido pedido)
        {
            return pedidoRepo.GuardarPedido(pedido);
        }
        public Pedido ObtenerPedido(int id)
        {
            return pedidoRepo.ObtenerPedido(id);
        }
    }
}
