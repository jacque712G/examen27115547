using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.Models
{
	public class Producto
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El campo Nombre Producto es requerido")]
		[StringLength(maximumLength: 100)]
		public string NombreProducto { get; set; } = null!;
		

	}
}
