using Pedido7115547.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.DTO
{
	public class PedidoDTO
	{
		public int Id { get; set; }
		public int IdCliente { get; set; }	
		public DateTime Fecha { get; set; }	
		public double Total { get; set; }		
		public string Estado { get; set; } = null!;
		public virtual List<DetalleDTO> Detalles { get; set; } = new List<DetalleDTO>();
	}
}
