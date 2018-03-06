using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;

namespace CapaRepositorio
{
    public class PedidoRepo
    {
        public IEnumerable<Pedido> MostrarPedidos()
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IEnumerable<Pedido> result = modeloDeDominio.Pedidos.ToList();
                return result;
            }
        }
        public int GuardarPedido(Pedido pedido)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                modeloDeDominio.Add(pedido);
                modeloDeDominio.SaveChanges();
            }
            return pedido.IdPedido;
        }
        public Pedido ObtenerPedido(int id)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                Pedido pedido = modeloDeDominio.Pedidos.Where(c => c.IdPedido == id).FirstOrDefault();

                return pedido;
            }
        }

    }
}
