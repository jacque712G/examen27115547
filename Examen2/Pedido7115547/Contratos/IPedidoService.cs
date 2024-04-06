using Pedido7115547.DTO;
using Pedido7115547.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.Contratos
{
	public interface IPedidoService
	{
		public Task<bool> InsertarPedido(PedidoDTO pedido);
		public Task<bool> ModificarPedido(PedidoDTO pedido, int id);	
		public Task<List<Pedido>> ListarPedidos();
		public Task<Pedido> ObtenerPedidoById(int id);
		public Task<List<Reporte1DTO>> ListarReporte1();
		public Task<List<Reporte2DTO>> ListarReporte2();
	}
}
