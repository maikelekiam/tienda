using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;
using CapaRepositorio;

namespace CapaNegocio
{
    public class DetallePedidoTemporalNego
    {
        DetallePedidoTemporalRepo detallePedidoTemporalRepo = new DetallePedidoTemporalRepo();

        public IEnumerable<DetallePedidoTemporal> MostrarDetallePedidosTemporal()
        {
            return detallePedidoTemporalRepo.MostrarDetallePedidosTemporal();
        }

        public void GuardarDetallePedidoTemporal(DetallePedidoTemporal detallePedidoTemporal)
        {
            detallePedidoTemporalRepo.GuardarDetallePedidoTemporal(detallePedidoTemporal);
        }
        public void BorrarListaDetallePedidoTemporal(int idUsu)
        {
            detallePedidoTemporalRepo.BorrarListaDetallePedidoTemporal(idUsu);
        }
        public void BorrarDetallePedidoTemporal(DetallePedidoTemporal detallePedidoTemporal)
        {
            detallePedidoTemporalRepo.BorrarDetallePedidoTemporal(detallePedidoTemporal);
        }
        public int TraerMenorIndice()
        {
            return detallePedidoTemporalRepo.TraerMenorIndice();
        }
        public DetallePedidoTemporal ObtenerDetallePedidoTemporal(int id)
        {
            return detallePedidoTemporalRepo.ObtenerDetallePedidoTemporal(id);
        }
        public void ActualizarDetallePedidoTemporal(DetallePedidoTemporal detallePedidoTemporal)
        {
            detallePedidoTemporalRepo.ActualizarDetallePedidoTemporal(detallePedidoTemporal);
        }
        public DetallePedidoTemporal FiltrarDetallePedidoTemporalSegunProducto(string cod, int idUsu)
        {
            return detallePedidoTemporalRepo.FiltrarDetallePedidoTemporalSegunProducto(cod,idUsu);
        }
    }
}
