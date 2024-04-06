using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pedido7115547.DTO
{
	public class Reporte1DTO
	{
	

	   public string NombreCliente { get; set; }=null!;
		public DateTime FechaPedido { get; set; }
		public string NombreProducto { get; set; } = null!;
		public double Subtotal { get; set; }

	}
}
