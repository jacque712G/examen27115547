using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.DTO
{
	public class DetalleDTO
	{
		public int Id { get; set; }
		public int IdPedido { get; set; }
		public int IdProducto { get; set; }	
		public int Cantidad { get; set; }		
		public double Precio { get; set; }
		public double Subtotal { get; set; }
	}
}
