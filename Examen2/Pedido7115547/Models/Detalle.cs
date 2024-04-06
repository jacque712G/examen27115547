using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.Models
{
	public class Detalle
	{
		[Key]
		public int Id { get; set; }
		public int IdPedido { get; set; }
		public int IdProducto { get; set; }

		[Required(ErrorMessage = "El campo Cantidad es requerido")]
		public int Cantidad { get; set; }

		[Required(ErrorMessage = "El campo Precio es requerido")]
		public double Precio { get; set; }

		[Required(ErrorMessage = "El campo Subtotal es requerido")]
		public double Subtotal { get; set; }		

		[ForeignKey("IdPedido")]		
		public virtual Pedido? Pedido { get; set; }
		[ForeignKey("IdProducto")]
		public virtual Producto? Producto { get; set; }

	}
}
